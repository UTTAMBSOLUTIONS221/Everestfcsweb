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
    public class ProductManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public ProductManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        public async Task<IActionResult> Index()
        {
            var data = await bl.Getsystemproductvariationdata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemproduct(long? ProductvariationId)
        {
            
            ViewData["Systemproductcategorylists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Systemproductcategory).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemproductuomlists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Systemproductuom).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> Editsystemproduct(long? ProductvariationId)
        {
            SystemProductVariation model = new SystemProductVariation();
            ViewData["Systemproductcategorylists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Systemproductcategory).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemproductuomlists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Systemproductuom).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (ProductvariationId > 0)
            {
                model = await bl.GetSystemProductDataById(SessionUserData.Token, ProductvariationId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Updatesystemproductdata(SystemProductVariation model)
        {
            var resp = await bl.Updatesystemproductdata(SessionUserData.Token, model);
            return Json(resp);
        }

        #region Station System Product data
        [HttpGet]
        public JsonResult GetSystemStationProductDataById(long Id, string character)
        {
            var Resp = bl.GetSystemStationProductDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, Id).Result;
            return Json(Resp);
        }
        public async Task<JsonResult> Addsystemproductdata(SystemProducts model)
        {
            var resp = await bl.Addsystemproductdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Main Store Listing
        public async Task<IActionResult> Mainstore()
        {
            var data = await bl.Getsystemproductmainstoredata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId);
            return View(data);
        }
        public async Task<IActionResult> Transfertoaccessories(long DryStockStoreId,long ProductVariationId)
        {
            DryStockMovement model = new DryStockMovement();
            model.DryStockStoreId = DryStockStoreId;
            model.ProductVariationId = ProductVariationId;  
           return PartialView(model);
        }
        public async Task<IActionResult> Transferfromaccessories(long DryStockStoreId, long ProductVariationId)
        {
            DryStockMovement model = new DryStockMovement();
            model.DryStockStoreId = DryStockStoreId;
            model.ProductVariationId = ProductVariationId;
            return PartialView(model);
        }
        public async Task<JsonResult> Savetransfertoaccessoriesdata(DryStockMovement model)
        {
            var resp = await bl.Savetransfertoaccessoriesdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion
    }
}
