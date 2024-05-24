using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security;
using System.Security.Claims;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public AccountController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        #region User Signin
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Signinuser(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signinuser(Userloginmodel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var resp = await bl.Validateuser(model);
            if (resp.RespStatus == 200)
            {
                if (resp.Usermodel.Loginstatus == (int)UserLoginStatus.ChangePassword)
                {
                    return RedirectToAction("Resetuserpassword", new Staffresetpassword() { Code = Guid.NewGuid(), Userid = resp.Usermodel.Userid });
                }
                else
                {
                    SetUserLoggedIn(resp, false);
                }

                //if (resp.LoginStatus == (int)UserLoginStatus.VerifyAccount)
                //{
                //    return RedirectToAction("VerifyAccount", "Account", new { Usercode = resp.Userid, Phonenumber = resp.Phone });
                //}
                return RedirectToLocal(returnUrl);
            }
            else if (resp.RespStatus == 401)
            {
                Warning(resp.RespMessage, true);
            }
            else
            {
                ModelState.AddModelError(string.Empty, resp.RespMessage);
            }
            return View();

        }
        #endregion

        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(AccountController.Signinuser), "Account");
        }
        #endregion

        #region User Reset Password
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Resetuserpassword(Staffresetpassword model)
        {
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Resetuserpasswordpost(Staffresetpassword model)
        {
            var resp = await bl.Resetuserpasswordpost(model);
            if (resp.RespStatus == 0)
            {
                return RedirectToAction("Signinuser");
            }
            else
            {
                ModelState.AddModelError(string.Empty, resp.RespMessage);
            }
            return RedirectToAction("Resetuserpassword", model);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Forgotpassword(StaffForgotPassword model)
        {
            var resp = await bl.Forgotpasswordpost(model);
            if (resp.RespStatus == 0)
            {
                return RedirectToAction("Signinuser");
            }
            else
            {
                ModelState.AddModelError(string.Empty, resp.RespMessage);
            }
            return View(model);

        }
        #endregion


        #region User Profile
        [HttpGet]
        public IActionResult Myprofile()
        {
            return View();
        }
        #endregion

        #region Other Methods

        private async Task SetUserLoggedIn(UsermodelResponce user, bool rememberMe)
        {
            string userData = JsonConvert.SerializeObject(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Usermodel.Userid.ToString()),
                new Claim(ClaimTypes.Name, user.Usermodel.Fullname),
                new Claim("Token", user.Token),
                new Claim("FullNames", user.Usermodel.Fullname ?? string.Empty),
                new Claim("Userid", user.Usermodel.Userid.ToString()),
                new Claim("Tenantname", user.Usermodel.Tenantname ?? string.Empty),
                new Claim("Tenantsubdomain", user.Usermodel.Tenantsubdomain ?? string.Empty),
                new Claim("TenantLogo", user.Usermodel.TenantLogo ?? string.Empty),
                new Claim("Currencyname", user.Usermodel.Currencyname ?? string.Empty),
                new Claim("Utcname", user.Usermodel.Utcname ?? string.Empty),
                new Claim("Phonenumber", user.Usermodel.Phonenumber ?? string.Empty),
                new Claim("Username", user.Usermodel.Username ?? string.Empty),
                new Claim("Emailaddress", user.Usermodel.Emailaddress ?? string.Empty),
                new Claim("Roleid", user.Usermodel.Roleid.ToString()),
                new Claim("Rolename", user.Usermodel.Rolename ?? string.Empty),
                new Claim("Passharsh", user.Usermodel.Passharsh ?? string.Empty),
                new Claim("Passwords", user.Usermodel.Passwords ?? string.Empty),
                new Claim("LimitTypeId", user.Usermodel.LimitTypeId.ToString()),
                new Claim("LimitTypeValue", user.Usermodel.LimitTypeValue.ToString("F2")),
                new Claim("Isactive", user.Usermodel.Isactive.ToString()),
                new Claim("Isdeleted", user.Usermodel.Isdeleted.ToString()),
                new Claim("Loginstatus", user.Usermodel.Loginstatus.ToString()),
                new Claim("Passwordresetdate", user.Usermodel.Passwordresetdate.ToString("o")),
                new Claim("Createdby", user.Usermodel.Createdby.ToString()),
                new Claim("Modifiedby", user.Usermodel.Modifiedby.ToString()),
                new Claim("Lastlogin", user.Usermodel.Lastlogin.ToString("o")),
                new Claim("Datemodified", user.Usermodel.Datemodified.ToString("o")),
                new Claim("Datecreated", user.Usermodel.Datecreated.ToString("o")),
                new Claim("userData", userData),
            };

            if (user.Usermodel.Stations != null)
            {
                string stationsData = JsonConvert.SerializeObject(user.Usermodel.Stations);
                claims.Add(new Claim("Stations", stationsData));
            }

            if (user.Usermodel.Permission != null)
            {
                string permissionsData = JsonConvert.SerializeObject(user.Usermodel.Permission);
                claims.Add(new Claim("Permissions", permissionsData));
            }


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity[] { claimsIdentity });
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = new DateTimeOffset?(DateTime.Now.AddMinutes(30))
            });
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                return RedirectToAction(nameof(HomeController.Dashboard), "Home", new { area = "" });
            }
            else if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Dashboard), "Home", new { area = "" });
            }
        }
        #endregion
    }
}
