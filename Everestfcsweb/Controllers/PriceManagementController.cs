
using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PriceManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public PriceManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }

        #region Price List and Prices
        public async Task<IActionResult> Pricelists()
        {
            var data = await bl.GetSystemPriceListData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        public IActionResult Addpricelist()
        {
            StationPriceLists data = new StationPriceLists();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations,SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Addpricelistdata(StationPriceLists model)
        {
            var resp = await bl.Addpricelistdata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editpricelist(long PricelistId)
        {
            var data= await bl.GetSystemPriceListDataById(SessionUserData.Token, PricelistId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystempricelistdata(PriceListInfoData model)
        {
            var resp = await bl.Editsystempricelistdata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public IActionResult Addpricelistprice(long PricelistId)
        {
            PriceListPriceDataModel model = new PriceListPriceDataModel();
            model.PriceListId=PricelistId;
             ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations,SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Addpricelistpricenewdata(PriceListPriceDataModel model)
        {
            var resp = await bl.Addpricelistpricenewdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Discount List and Prices
        public async Task<IActionResult> Discountlists()
        {
            var data = await bl.GetSystemDiscountListData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        public IActionResult Addsystemdiscountlist()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Adddiscountlistdata(StationDiscountLists model)
        {
            var resp = await bl.Adddiscountlistdata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Editdiscountlist(long DiscountlistId)
        {
            var data = await bl.GetSystemDiscountListDataById(SessionUserData.Token, DiscountlistId);
            return PartialView(data);
        }
        public async Task<JsonResult> Editsystemdiscountlistdata(DiscountListModelData model)
        {
            var resp = await bl.Editsystemdiscountlistdata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Adddiscountlistvalue(long DiscountlistId)
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            LnkDiscountProductModel model = new LnkDiscountProductModel();
            model.DiscountlistId=DiscountlistId;
            return PartialView(model);
        }

        public async Task<JsonResult> Adddicountlistvaluenewdata(LnkDiscountProductModel model)
        {
            var resp = await bl.Adddicountlistvaluenewdata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Discountlistdiscountsdata(long DiscountlistId)
        {
            var data = await bl.Getdiscountlistdiscountsdata(SessionUserData.Token, DiscountlistId);
            return PartialView(data);
        }
        #endregion
    }
}
