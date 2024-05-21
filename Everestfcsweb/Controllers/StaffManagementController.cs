
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
    public class StaffManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public StaffManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        #region System Roles
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var data = await bl.GetSystemUserRolesData(SessionUserData.Token,SessionUserData.Usermodel.Tenantid, 0, 10);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemrole(long? RoleId)
        {
            SystemUserRoles model = new SystemUserRoles();  
            ViewData["Systempermissionlists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Systempermission).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (RoleId>0)
            {
                model = await bl.GetSystemRoleDetailData(SessionUserData.Token, RoleId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemroledata(SystemUserRoles model)
        {
            var Resp = await bl.Addsystemroledata(SessionUserData.Token,model);
            return  Json(Resp);
        }
        [HttpGet]
        public async Task<IActionResult> Systemroledetail(long? RoleId)
        {
            var model = await bl.GetSystemRoleDetailData(SessionUserData.Token, RoleId);
            return PartialView(model);
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await bl.GetSystemStaffsData(SessionUserData.Token,SessionUserData.Usermodel.Tenantid, 0, 10);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemstaff(long? Userid)
        {
            SystemStaffs model = new SystemStaffs();
           
            ViewData["Systemphonecodelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstaffrolelists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Roles, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemConsumptionlimitTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.ConsumptionType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemStationlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (Userid>0)
            {
                model = await bl.Getsystemstaffdetailtdata(SessionUserData.Token,Userid);
            }
            model.Tenantsubdomain = SessionUserData.Usermodel.Tenantsubdomain;
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemstaffdata(SystemStaffs model)
        {
            model.Tenantid =SessionUserData.Usermodel.Tenantid;    
            var Resp = await bl.Addsystemstaffdata(SessionUserData.Token, model);
            return Json(Resp);
        }

        #region Delete,Deactivate Actions
        public async Task<JsonResult> AllDeleteDeativateActions(ActivateDeactivateActions model)
        {
            var resp = await bl.DeactivateorDeleteTenantTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> AllRemoveDeativateActions(ActivateDeactivateActions model)
        {
            var resp = await bl.RemoveTenantTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> AllSingleDefaultActions(ActivateDeactivateActions model)
        {
            var resp = await bl.DefaultThisTenantTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        [HttpGet, HttpPost]
        public async Task<IActionResult> Resendstaffpassword(long Tenantstaffid)
        {
            var Resp = await bl.Resendstaffpassword(SessionUserData.Token, Tenantstaffid);
            if (Resp.RespStatus == 0)
            {
                Success(Resp.RespMessage, true);
            }
            else if (Resp.RespStatus == 1)
            {
                Warning(Resp.RespMessage, true);
            }
            else
            {
                Danger(Resp.RespMessage, true);
            }
            return RedirectToAction("Index");
        }
    }
}
