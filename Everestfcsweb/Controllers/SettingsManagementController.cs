
using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SettingsManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public SettingsManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }

        #region System Permissions
        [HttpGet]
        public async Task<IActionResult> Systempermissionlist()
        {
            var data = await bl.Getsystempermissiondata(SessionUserData.Token);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystempermissions(long Permissionid)
        {
            Systempermissions model = new Systempermissions();
            if (Permissionid > 0)
            {
                model = await bl.Getsystempermissiondatabyid(SessionUserData.Token, Permissionid);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystempermissiondata(Systempermissions model)
        {
            var Resp = await bl.Registersystempermissiondata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Tenant Account Settings
        [HttpGet]
        public async Task<IActionResult> Systemtenantlist()
        {
            var data = await bl.Getsystemtenantaccountdata(SessionUserData.Token);
            return View(data);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registersystemtenant()
        {
            ViewData["Systemphonecodelists"] = bl.UnauthGetSystemDropDownData(ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCountrylists"] = bl.UnauthGetSystemDropDownData(ListModelType.Country).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
           return  View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Unauthregistertenantaccountdata(SystemTenantAccount model)
        {
            var resp = await bl.Unauthregistertenantaccountdata(model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemtenant(long? TenantId)
        {
            ViewData["Systemphonecodelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCountrylists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Country).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            SystemTenantAccount data = new SystemTenantAccount();
            if (TenantId > 0)
            {
                data = await bl.Getsystemtenantaccountbytenantid(SessionUserData.Token, TenantId);
            }
            return PartialView(data);
        }
        public async Task<JsonResult> Registertenantaccountdata(SystemTenantAccount model)
        {
            var resp = await bl.Registertenantaccountdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region  Tenant System Suppliers
        [HttpGet]
        public async Task<IActionResult> Systemsupplierslist()
        {
            var data = await bl.Getsystemsupplierslistdata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemsuplier(long? SupplierId)
        {
            ViewData["Systemphonecodelists"] = bl.UnauthGetSystemDropDownData(ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            SystemSupplier model = new SystemSupplier();
            if (SupplierId > 0)
            {
                model = await bl.Getsystemsupplierdetailbyid(SessionUserData.Token, SupplierId);
            }
            else
            {
                model = await bl.Getsystemsupplierdetailbyid(SessionUserData.Token, 0);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemsupplierdata(SystemSupplier model)
        {
            var Resp = await bl.Registersystemsuppliersdata(SessionUserData.Token, model);
            return Json(Resp);
        }
       
        #endregion

        #region Loyalty Settings

        [HttpGet]
        public async Task<IActionResult> Loyaltysettinglist()
        {
            var data = await bl.Getsystemloyaltysettingsdata(SessionUserData.Token);
            return View(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Addsystemloyaltysetting(long? LoyaltysettId)
        {
            ViewData["Systemloyaltyawardtypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemLoyaltyAwardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemtenantaccountslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemTenantAccounts).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            SystemLoyaltySetings data = new SystemLoyaltySetings();
            if (LoyaltysettId > 0)
            {
                data = await bl.GetLoyaltySettingsById(SessionUserData.Token, LoyaltysettId);
            }
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> LoyaltySettings()
        {
            ViewData["Systemloyaltyawardtypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemLoyaltyAwardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            var data = await bl.GetLoyaltySettings(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        public async Task<JsonResult> AddsystemloyaltysettingData(SystemLoyaltySetings model)
        {
            var resp = await bl.PostLoyaltySettingsData(SessionUserData.Token, model);
            return Json(resp);
        }
       
        #endregion
    }
}
