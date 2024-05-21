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
    public class HardwareManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public HardwareManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        #region POS Management
        public async Task<IActionResult> Gadgetlists(int Offset, int Count)
        {
            var data = await bl.GetSystemGadgetsData(SessionUserData.Token, Offset, Count);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> AddTenantGadget(long? GadgetId)
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Stations).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();

            Systemgadgets data = new Systemgadgets();
            if (GadgetId > 0)
            {
                data = await bl.GetSystemGadgetsDataById(SessionUserData.Token, GadgetId);
            }
            return PartialView(data);
        }
        public async Task<JsonResult> AddTenantGadgetdata(Systemgadgets model)
        {
            var resp = await bl.AddTenantGadgetdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion


        #region Cards Management
        public async Task<IActionResult> Cardlists(int Offset, int Count)
        {
            var data = await bl.GetSystemCardsData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, Offset, Count);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> AddTenantCard(long? CardId)
        {
            ViewData["SystemCardTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.CardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).Where(x=>x.Text!= "V-Card").ToList();
            SystemTenantCard data = new SystemTenantCard();
            if (CardId > 0)
            {
                data = await bl.GetSystemTenantCardDataById(SessionUserData.Token, CardId);
            }
            return PartialView(data);
        }
        public async Task<JsonResult> AddTenantCarddata(SystemTenantCard model)
        {
            var resp = await bl.AddTenantCarddata(SessionUserData.Token, model);
            return Json(resp);
        }
        
        [HttpGet,HttpPost]
        public async Task<IActionResult> Resendcardpindata(long Tenantcardid)
        {
            var Resp = await bl.Resendcardpindata(SessionUserData.Token, Tenantcardid);
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
            return RedirectToAction("Cardlists", new { Offset=0, Count=1000 });
        }
        #endregion
    }
}
