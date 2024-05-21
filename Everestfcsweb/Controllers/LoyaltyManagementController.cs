
using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class LoyaltyManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public LoyaltyManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }

        #region Loyalty Formula
        [HttpGet]
        public async Task<IActionResult> Formulalist()
        {
            var data = await bl.GetSystemLoyaltyFormulaandFormulaRulesData(SessionUserData.Token,SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        [HttpGet]
        public IActionResult Addsystemformula()
        {
            return PartialView();
        }
        public async Task<JsonResult> Addstemawardingformuladata(LoyaltyFormulaandRules model)
        {
            var resp = await bl.Addstemawardingformuladata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editsystemformula(long FormulaId)
        {
            var data = await bl.Getsystemformulabyid(SessionUserData.Token, FormulaId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystemformuladata(SystemFormulaData model)
        {
            var resp = await bl.Editsystemformuladatadata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editsystemformularule(long FormulaRuleId)
        {
            var data = await bl.Getsystemformularuledatabyid(SessionUserData.Token, FormulaRuleId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystemformularuledata(SystemFormulaRuleData model)
        {
            var resp = await bl.Editsystemformularuleeditdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Loyalty Schemes
        [HttpGet]
        public async Task<IActionResult> Schemelist()
        {
            var data = await bl.GetSystemLoyaltySchemeandSchemeRulesData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemscheme()
        {
            ViewData["Systemstationslists"] =  bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations,SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemLoyaltyGroupinglists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Loyaltygrouping).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemFormulaslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemFormulas, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemRewardslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemRewards).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();

            return PartialView();
        }
        public async Task<JsonResult> Addstemawardingmschemedata(LoyaltySchemesandRules model)
        {
            var resp = await bl.Addstemawardingmschemedata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editsystemscheme(long LSchemeId)
        {
            var data = await bl.Getsystemschemedatabyid(SessionUserData.Token, LSchemeId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystemschemedata(SystemLoyaltyScheme model)
        {
            var resp = await bl.Editsystemschemedata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editsystemschemerule(long LSchemeRuleId)
        {
            ViewData["Systemstationslists"] =  bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Stations).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemLoyaltyGroupinglists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Loyaltygrouping).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemFormulaslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemFormulas, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemRewardslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemRewards).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();

            var data = await bl.Getsystemschemeruledatabyid(SessionUserData.Token, LSchemeRuleId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystemschemeruledata(SchemeRule model)
        {
            var resp = await bl.Editsystemschemeruledata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion
    }
}
