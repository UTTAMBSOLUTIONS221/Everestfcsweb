using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class StationManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public StationManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        public async Task<IActionResult> Index(int Offset, int Count)
        {
            IEnumerable<SystemStationData> data = new List<SystemStationData>();
            if (SessionUserData.Usermodel.Stations.Count() > 1)
            {
                data = await bl.GetSystemstationsData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, 0, Offset, Count);
            }
            else
            {
                data = await bl.GetSystemstationsData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, Offset, Count);
            }
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> AddSystemStation(long? StationId)
        {
            ViewData["Systemphonecodelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            SystemStations data = new SystemStations();
            if (StationId > 0)
            {
                data = await bl.GetSystemStationDetailDataById(SessionUserData.Token, StationId);
            }
            return PartialView(data);
        }
        public async Task<JsonResult> Addsystemstationdata(SystemStations model)
        {
            var resp = await bl.Addsystemstationdata(SessionUserData.Token, model);
            return Json(resp);
        }

        #region Station Details
        [HttpGet]
        public async Task<IActionResult> Details(long StationId)
        {
            ViewData["SystemTankslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemTank, StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationstafflists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, ListModelType.SystemStationStaff, StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            Systemstationdetailmodel model = new Systemstationdetailmodel();
            model.StationId = StationId;
            model = await bl.GetSystemStationallDetailDataById(SessionUserData.Token, StationId);
            return View(model);
        }
        #endregion

        #region Automate Station 
        [HttpGet]
        public async Task<IActionResult> AutomateSystemStation(long StationId)
        {
            AutomatedStationData data = new AutomatedStationData();
            data.StationId = StationId;
            return PartialView(data);
        }
        public async Task<JsonResult> Automatesystemstationdata(AutomatedStationData model)
        {
            var resp = await bl.Automatesystemstationdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Station Tanks

        [HttpGet]
        public async Task<IActionResult> AddSystemStationTank(long StationId, long? TankId)
        {
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemWetProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            StationTankModel model = new StationTankModel();
            model.StationId = StationId;
            if (TankId > 0)
            {
                model = await bl.GetSystemStationTankDetailDataById(SessionUserData.Token, TankId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> AddSystemStationTankdata(StationTankModel model)
        {
            var resp = await bl.AddSystemStationTankdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Station Pumps

        [HttpGet]
        public async Task<IActionResult> AddSystemStationPump(long StationId, long? PumpId)
        {
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemTankslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemTank, StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            Stationpumps model = new Stationpumps();
            model.Stationid = StationId;
            if (PumpId > 0)
            {
                model = await bl.GetSystemStationPumpDetailDataById(SessionUserData.Token, PumpId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemstationpumpdata(Stationpumps model)
        {
            var resp = await bl.AddSystemStationpumpdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Station Dips
        [HttpGet]
        public async Task<IActionResult> Addsystemstationdips(long StationId)
        {
            ViewData["SystemTankslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemTank, StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            StationDailyDip model = new StationDailyDip();
            model.StationId = StationId;
            return PartialView(model);
        }
        public async Task<JsonResult> GetsystemstationtankdetailbyId(long TankId)
        {
            var resp = await bl.GetsystemstationtankdetailbyId(SessionUserData.Token, TankId);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationtankdipsdata(StationDailyDip model)
        {
            var resp = await bl.Addsystemstationtankdipsdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Station Purchase
        [HttpGet]
        public async Task<IActionResult> Addsystemstationpurchases(long StationId, long? PurchaseId)
        {
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemSupplierslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemSuppliers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            StationPurchase model = new StationPurchase();
            model.StationId = StationId;
            if (PurchaseId > 0)
            {
                model = await bl.Getsystemstationpurchasesdetailbyid(SessionUserData.Token, PurchaseId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemstationpurchasesdata(StationPurchase model)
        {
            var resp = await bl.Addsystemstationpurchasesdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion


        #region Station Shifts
        [HttpGet]
        public async Task<IActionResult> Shiftlistings()
        {
            IEnumerable<SystemStationShift> model = new List<SystemStationShift>();

            if (SessionUserData.Usermodel.Stations.Count() == 1)
            {
                model = await bl.Getsystemstationshiftlistdata(SessionUserData.Token, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId);
            }
            else
            {
                model = await bl.Getsystemstationshiftlistdata(SessionUserData.Token, 0);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemstationshift(long? ShiftId)
        {
            ViewData["Systemstationstafflists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemDryProductList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemDryProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemWetProductList"] = bl.GetSystemDropDownDataByIdAndSearch(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, "Fuel(Petrol, Diesel,Kerosine)").Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCustomersList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCreditCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPaymentmodes"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemBankPaymentmodes"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.BankPaymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemSupplierslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemSuppliers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemTankslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemTank, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["CustomerequipmentList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomerEquipments, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            SingleStationShiftData model = new SingleStationShiftData();
            if (ShiftId > 0)
            {
                model = await bl.Getsystemstationsingleshiftdata(SessionUserData.Token, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, ShiftId);
            }
            else
            {
                model = await bl.Getsystemstationsingleshiftdata(SessionUserData.Token, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, 0);
            }
            return View(model);
        }
        public async Task<JsonResult> Addsystemstationshiftdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshiftpumpdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftpumpdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshifttankdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshifttankdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshiftlubedata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftlubedata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshiftlpgdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftlpgdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshiftsparepartdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftsparepartdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationshiftcarwashdata(SingleStationShiftData model)
        {
            var resp = await bl.Addsystemstationshiftcarwashdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #region Shift Credit Invoices
        [HttpGet]
        public async Task<IActionResult> GetSystemStationShiftCreditInvoiceData(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftcreditinvoicedata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftcreditinvoicedata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftCreditInvoice>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response); 
        }

        public async Task<JsonResult> Addsystemstationshiftcreditinvoicedata(ShiftCreditInvoice model)
        {
            var resp = await bl.Addsystemstationshiftcreditinvoicedata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Wetstock Purchase
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshiftwetstockpurchasedata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftwetstockpurchasedata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftwetstockpurchasedata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftWetStockPurchases>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.ShiftWetStockPurchases
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftwetpurchasedata(ShiftWetStockPurchase model)
        {
            var resp = await bl.Addsystemstationshiftwetpurchasedata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Drystock Purchase
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshiftdrystockpurchasedata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftdrystockpurchasedata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftdrystockpurchasedata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftDryStockPurchases>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.ShiftDryStockPurchases
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftdrypurchasedata(ShiftDryStockPurchase model)
        {
            var resp = await bl.Addsystemstationshiftdrypurchasedata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Expense Data
        [HttpGet]
        public async Task<IActionResult> GetSystemStationShiftExpenseData(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftexpensedata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftexpensedata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftExpenses>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftexpensedata(ShiftExpenses model)
        {
            var resp = await bl.Addsystemstationshiftexpensedata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Mpesa Collection
        [HttpGet]
        public async Task<IActionResult> GetSystemStationShiftMpesaCollectionData(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftmpesacollectiondata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftmpesacollectiondata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftMpesaCollection>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftmpesadata(ShiftMpesaCollection model)
        {
            var resp = await bl.Addsystemstationshiftmpesadata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Fuel Card Collection
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshiftfuelcarddata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftfuelcarddata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftfuelcarddata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftFuelCardCollection>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftfuelcarddata(ShiftFuelCardCollection model)
        {
            var resp = await bl.Addsystemstationshiftfuelcarddata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion


        #region Shift Merchant Collection
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshiftmerchantdata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftmerchantdata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftmerchantdata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftMerchantCollection>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftmerchantdata(ShiftMerchantCollection model)
        {
            var resp = await bl.Addsystemstationshiftmerchantdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Topup
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshifttopupdata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshifttopupdata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshifttopupdata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftTopup>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshifttopupdata(ShiftTopup model)
        {
            var resp = await bl.Addsystemstationshifttopupdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Shift Payments
        [HttpGet]
        public async Task<IActionResult> Getsystemstationshiftpaymentdata(JQueryDataTableParamModel param)
        {
            // Retrieve parameters
            int draw = int.Parse(param.draw);
            int start = param.start;
            int length = param.length;
            string searchValue = param.searchValue ?? string.Empty;
            // Filtering
            var filteredData = string.IsNullOrEmpty(searchValue) ?
                bl.Getsystemstationshiftpaymentdata(SessionUserData.Token, param.ShiftId, start, length, searchValue) : bl.Getsystemstationshiftpaymentdata(SessionUserData.Token, param.ShiftId, start, length, searchValue);
            if (filteredData == null || filteredData.Result == null)
            {
                // Handle the case where the response is null
                // You can return an error message or an empty response based on your requirement
                return Json(new { draw = draw, recordsTotal = 0, recordsFiltered = 0, data = new List<ShiftPayment>() });
            }
            // Response
            var response = new
            {
                draw = draw,
                recordsTotal = filteredData.Result.RecordsTotal,
                recordsFiltered = filteredData.Result.RecordsTotal,
                data = filteredData.Result.DataTableData
            };

            return Json(response);
        }
        public async Task<JsonResult> Addsystemstationshiftpaymentdata(ShiftPayment model)
        {
            var resp = await bl.Addsystemstationshiftpaymentdata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion
        public async Task<JsonResult> Closesystemstationshiftdata(SingleStationShiftData model)
        {
            var resp = await bl.Closesystemstationshiftdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Supervisorclosesystemstationshiftdata(SingleStationShiftData model)
        {
            var resp = await bl.Supervisorclosesystemstationshiftdata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> ShiftDetails(long ShiftId)
        {
            ViewData["Systemstationstafflists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            var data = await bl.Getsystemstationsingleshiftdata(SessionUserData.Token, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, ShiftId);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> ShiftDataDetails(long ShiftId)
        {
            var data = await bl.Getsystemstationshiftdetaildata(SessionUserData.Token, ShiftId);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Addsystemstationlube(long ShiftId, long AttendantId, long? ShiftLubeLpgId, string ProductCategory)
        {
            ViewData["SystemProduct"] = bl.GetSystemDropDownDataByIdAndSearch(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, ProductCategory).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ShiftLubesandLpg model = new ShiftLubesandLpg();
            model.ShiftId = ShiftId;
            model.AttendantId = AttendantId;
            model.Productcategory = ProductCategory;
            if (ShiftLubeLpgId > 0)
            {
                model = await bl.GetSystemStationLubeandLpgById(SessionUserData.Token, ShiftLubeLpgId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> GetsystemdryproductpricebyId(long ProductId, long CustomerId)
        {
            var resp = await bl.GetsystemdryproductpricebyId(SessionUserData.Token, ProductId, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, CustomerId);
            return Json(resp);
        }
        public async Task<JsonResult> GetsystemproductpricevatbyId(long SupplierId, long ProductVariationId)
        {
            var resp = await bl.GetsystemproductpricevatbyId(SessionUserData.Token, SupplierId, ProductVariationId);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemstationlubedata(ShiftLubesandLpg model)
        {
            var resp = await bl.Addsystemstationlubedata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Addsystemstationvoucher(long ShiftId, long AttendantId, long? ShiftVoucherId, string CashCollection)
        {
            ViewData["SystemPaymentmodes"] = bl.GetSystemDropDownDataByIdAndSearch(SessionUserData.Token, ListModelType.Paymentmodes, 0, CashCollection).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ShiftVoucher model = new ShiftVoucher();
            model.ShiftId = ShiftId;
            model.AttendantId = AttendantId;
            model.CashCollection = CashCollection;
            if (ShiftVoucherId > 0)
            {
                model = await bl.Getsystemstationvoucherdatabyid(SessionUserData.Token, ShiftVoucherId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Addsystemstationshiftvoucherdata(ShiftVoucher model)
        {
            var resp = await bl.Addsystemstationshiftvoucherdata(SessionUserData.Token, model);
            return Json(resp);
        }


        //[HttpGet]
        //public async Task<IActionResult> Addsystemcreditinvoicesale(long ShiftId, long AttendantId, long? ShiftCreditInvoiceId)
        //{
        //    ViewData["SystemProductList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
        //    ViewData["SystemCustomersList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCreditCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
        //    ShiftCreditInvoice model = new ShiftCreditInvoice();
        //    model.ShiftId = ShiftId;
        //    model.AttendantId = AttendantId;
        //    if (ShiftCreditInvoiceId > 0)
        //    {
        //        model = await bl.Getsystemcreditinvoicesaledatabyid(SessionUserData.Token, ShiftCreditInvoiceId);
        //        ViewData["CustomerequipmentList"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomerEquipments, model.CustomerId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
        //    }
        //    return PartialView(model);
        //}
        //public async Task<JsonResult> Addsystemcreditinvoicesaledata(ShiftCreditInvoice model)
        //{
        //    var resp = await bl.Addsystemcreditinvoicesaledata(SessionUserData.Token, model);
        //    return Json(resp);
        //}
        #endregion


        #region Station Summary
        [HttpGet]
        public async Task<IActionResult> SystemStationSummary()
        {
            ViewData["Systemstationslists"] = (await bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid)).Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();

            var StationId = SessionUserData.Usermodel.Stations.Count() > 1 ? 0 : SessionUserData.Usermodel.Stations.FirstOrDefault().StationId;
            ViewData["Systemstationshiftslists"] = (await bl.GetSystemDropDownDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, ListModelType.Systemstationshifts, StationId)).Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> SystemStationSummaryPost(DateTime startdate, DateTime enddate,long stationid, long shiftid)
        {
            var data = await bl.Getsystemstationsalesummary(SessionUserData.Token, startdate, enddate, stationid, shiftid);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Stationsalesummaryreport");
                var currentRow = 1;

                var mergedCell = worksheet.Range(currentRow, 1, currentRow, 10).Merge();
                mergedCell.Value = "STATION SALE SUMMARY REPORT";
                mergedCell.Style.Font.Bold = true;
                mergedCell.Style.Fill.BackgroundColor = XLColor.White;
                mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                mergedCell.Style.Font.FontColor = XLColor.Black;
                currentRow++;

                worksheet.Range(currentRow, 1, currentRow, 10).Merge();
                worksheet.Range(currentRow, 1, currentRow, 10).Value = "PUMP SUMMARIES";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.Black;
                currentRow++;

                // Add header for sales details
                worksheet.Cell(currentRow, 1).Value = "NOZZLE";
                worksheet.Cell(currentRow, 2).Value = "OPEN READ";
                worksheet.Cell(currentRow, 3).Value = "CLOSE READ";
                worksheet.Cell(currentRow, 4).Value = "GROSS SALES";
                worksheet.Cell(currentRow, 5).Value = "PRICE PER LITER";
                worksheet.Cell(currentRow, 6).Value = "CASH GROSS SALE";
                worksheet.Cell(currentRow, 7).Value = "PUMP TEST/RTT";
                worksheet.Cell(currentRow, 8).Value = "CASH TEST/RTT";
                worksheet.Cell(currentRow, 9).Value = "NET SALES";
                worksheet.Cell(currentRow, 10).Value = "CASH NET SALES";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;
                currentRow++;
                if (data != null)
                {
                    if (data.ManualPumpSummary != null)
                    {
                        var groupedReports = data.ManualPumpSummary
                            .GroupBy(x => x.ProductVariationName)
                            .Select(group => new
                            {
                                ProductVariationName = group.Key,
                                Reports = group.ToList(), // To calculate totals
                                Nozzle = group.Select(report => report.Nozzle).FirstOrDefault(),
                                OpeningReading = group.Sum(report => report.OpeningReading),
                                ClosingReading = group.Sum(report => report.ClosingReading),
                                GrossSale = group.Sum(report => report.GrossSale),
                                PricePerLiter = group.Select(report => report.PricePerLiter).FirstOrDefault(),
                                EquiCashGross = group.Sum(report => report.EquiCashGross),
                                PumpTestRTT = group.Sum(report => report.PumpTestRTT),
                                EquiCashTransferRttandGen = group.Sum(report => report.EquiCashTransferRttandGen),
                                NetSale = group.Sum(report => report.NetSale),
                                EquiCashNetSale = group.Sum(report => report.EquiCashNetSale)
                            });

                        foreach (var group in groupedReports)
                        {
                            foreach (var report in group.Reports)
                            {
                                worksheet.Cell(currentRow, 1).Value = report.Nozzle;
                                worksheet.Cell(currentRow, 2).Value = report.OpeningReading;
                                worksheet.Cell(currentRow, 3).Value = report.ClosingReading;
                                worksheet.Cell(currentRow, 4).Value = report.GrossSale;
                                worksheet.Cell(currentRow, 5).Value = report.PricePerLiter;
                                worksheet.Cell(currentRow, 6).Value = report.EquiCashGross;
                                worksheet.Cell(currentRow, 7).Value = report.PumpTestRTT;
                                worksheet.Cell(currentRow, 8).Value = report.EquiCashTransferRttandGen;
                                worksheet.Cell(currentRow, 9).Value = report.NetSale;
                                worksheet.Cell(currentRow, 10).Value = report.EquiCashNetSale;
                                currentRow++;
                            }
                            worksheet.Cell(currentRow, 1).Value = group.ProductVariationName + " Total";
                            worksheet.Range(currentRow, 1, currentRow, 3).Merge().Style.Font.Bold = true;
                            worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                            worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;

                            worksheet.Cell(currentRow, 4).Value = group.Reports.Sum(report => report.GrossSale);
                            worksheet.Cell(currentRow, 5).Value = "";
                            worksheet.Cell(currentRow, 6).Value = group.Reports.Sum(report => report.EquiCashGross);
                            worksheet.Cell(currentRow, 7).Value = group.Reports.Sum(report => report.PumpTestRTT);
                            worksheet.Cell(currentRow, 8).Value = group.Reports.Sum(report => report.EquiCashTransferRttandGen);
                            worksheet.Cell(currentRow, 9).Value = group.Reports.Sum(report => report.NetSale);
                            worksheet.Cell(currentRow, 10).Value = group.Reports.Sum(report => report.EquiCashNetSale);

                            currentRow++;
                        }

                        // Calculate grand totals
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = "Grand Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 3).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 4).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.GrossSale));
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 6).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.EquiCashGross));
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 7).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.PumpTestRTT));
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;


                        worksheet.Cell(currentRow, 8).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.EquiCashTransferRttandGen));
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 9).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.NetSale));
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 10).Value = groupedReports.Sum(group => group.Reports.Sum(report => report.EquiCashNetSale));
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                }



                worksheet.Range(currentRow, 1, currentRow, 10).Merge();
                worksheet.Range(currentRow, 1, currentRow, 10).Value = "TANK SUMMARIES";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.Black;
                currentRow++;

                //worksheet.Range(currentRow, 1, currentRow, 13).Merge();
                //worksheet.Range(currentRow, 1, currentRow, 13).Value = "MANUAL DIPS WET STOCK SUMMARIES";
                //worksheet.Range(currentRow, 1, currentRow, 13).Style.Font.Bold = true;
                //worksheet.Range(currentRow, 1, currentRow, 13).Style.Fill.BackgroundColor = XLColor.White;
                //worksheet.Range(currentRow, 1, currentRow, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //worksheet.Range(currentRow, 1, currentRow, 13).Style.Font.FontColor = XLColor.Black;
                //currentRow++;
                // Add header for sales details
                worksheet.Cell(currentRow, 1).Value = "TANK";
                worksheet.Cell(currentRow, 2).Value = "OPEN STOCK";
                worksheet.Cell(currentRow, 3).Value = "PURCHASES";
                worksheet.Cell(currentRow, 4).Value = "TOTAL STOCK";
                worksheet.Cell(currentRow, 5).Value = "SALE BY PUMP";
                worksheet.Cell(currentRow, 6).Value = "EXPECTED CLOSING STOCK";
                worksheet.Cell(currentRow, 7).Value = "ACTUAL CLOSING STOCK";
                worksheet.Cell(currentRow, 8).Value = "SALES BY TANK";
                worksheet.Cell(currentRow, 9).Value = "GAIN/LOSS";
                worksheet.Cell(currentRow, 10).Value = "% GAIN/LOSS";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;
                currentRow++;
                if (data != null)
                {
                    if (data.ManualTankSummary != null)
                    {
                        var groupedReports = data.ManualTankSummary
                            .GroupBy(x => x.Tank)
                            .Select(group => new
                            {
                                ProductVariationName = group.Key,
                                Reports = group.ToList(), // To calculate totals
                                Tank = group.Select(report => report.Tank).FirstOrDefault(),
                                OpeningStock = group.Sum(report => report.OpeningStock),
                                Purchases = group.Sum(report => report.Purchases),
                                TotalStock = group.Sum(report => report.TotalStock),
                                SalebyPump = group.Select(report => report.SalebyPump),
                                ExpectedClosingStock = group.Sum(report => report.ExpectedClosingStock),
                                ActualClosingStock = group.Sum(report => report.ActualClosingStock),
                                SaleByTank = group.Sum(report => report.SaleByTank),
                                Gain = group.Sum(report => report.Gain),
                                PercentageGain = group.Sum(report => report.PercentageGain),
                            });

                        foreach (var group in groupedReports)
                        {
                            foreach (var report in group.Reports)
                            {
                               
                                worksheet.Cell(currentRow, 1).Value = report.Tank;
                                worksheet.Cell(currentRow, 2).Value = report.OpeningStock;
                                worksheet.Cell(currentRow, 3).Value = report.Purchases;
                                worksheet.Cell(currentRow, 4).Value = report.TotalStock;
                                worksheet.Cell(currentRow, 5).Value = report.SalebyPump;
                                worksheet.Cell(currentRow, 6).Value = report.ExpectedClosingStock;
                                worksheet.Cell(currentRow, 7).Value = report.ActualClosingStock;
                                worksheet.Cell(currentRow, 8).Value = report.SaleByTank;
                                worksheet.Cell(currentRow, 9).Value = report.Gain;
                                worksheet.Cell(currentRow, 10).Value = report.PercentageGain;
                                 currentRow++;
                            }
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Stationsalessummaryreport.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> SystemPurchasesSummary()
        {
            ViewData["Systemstationslists"] = (await bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid)).Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> SystemPurchasesSummaryPost(DateTime startdate, DateTime enddate, long stationid)
        {
            var data = await bl.Getsystemstationpurchasessummary(SessionUserData.Token, startdate, enddate, stationid);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Stationpurchasessummaryreport");
                var currentRow = 1;

                var mergedCell = worksheet.Range(currentRow, 1, currentRow, 10).Merge();
                mergedCell.Value = "STATION PURCHASE SUMMARY REPORT";
                mergedCell.Style.Font.Bold = true;
                mergedCell.Style.Fill.BackgroundColor = XLColor.White;
                mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                mergedCell.Style.Font.FontColor = XLColor.Black;
                currentRow++;

                // Add header for sales details
                worksheet.Cell(currentRow, 1).Value = "DATE";
                worksheet.Cell(currentRow, 2).Value = "STATION";
                worksheet.Cell(currentRow, 3).Value = "ATTENDANT";
                worksheet.Cell(currentRow, 4).Value = "INVOICE #";
                worksheet.Cell(currentRow, 5).Value = "TRUCK";
                worksheet.Cell(currentRow, 6).Value = "DRIVER";
                worksheet.Cell(currentRow, 7).Value = "PRODUCT";
                worksheet.Cell(currentRow, 8).Value = "QUANTITY";
                worksheet.Cell(currentRow, 9).Value = "PRCE";
                worksheet.Cell(currentRow, 10).Value = "TOTAL";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;
                if (data != null)
                {
                    foreach (var report in data)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = report.InvoiceDate;
                        worksheet.Cell(currentRow, 2).Value = report.StationName;
                        worksheet.Cell(currentRow, 3).Value = report.CreatedBy;
                        worksheet.Cell(currentRow, 4).Value = report.InvoiceNumber;
                        worksheet.Cell(currentRow, 5).Value = report.TruckNumber;
                        worksheet.Cell(currentRow, 6).Value = report.DriverName;
                        worksheet.Cell(currentRow, 7).Value = report.ProductVariationName;
                        worksheet.Cell(currentRow, 8).Value = report.PurchaseQuantity;
                        worksheet.Cell(currentRow, 9).Value = report.PurchasePrice;
                        worksheet.Cell(currentRow, 10).Value = report.PurchaseTotal;
                    }
                    // Calculate grand totals
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = "Total: ";
                    worksheet.Range(currentRow, 1, currentRow, 9).Merge().Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;

                    worksheet.Cell(currentRow, 10).Value = data.Sum(report => report.PurchaseTotal);
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Stationpurchasessummaryreport.xlsx");
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> Systemshiftstatement()
        {
            ViewData["Systemstationslists"] = (await bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid)).Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> SystemshiftstatementPost(DateTime startdate, DateTime enddate, long stationid,long shiftId)
        {
            var data = await bl.Getsystemshiftstatementsummary(SessionUserData.Token, startdate, enddate, stationid, shiftId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Stationstatementreport");
                var currentRow = 1;

                var mergedCell = worksheet.Range(currentRow, 1, currentRow, 10).Merge();
                mergedCell.Value = "STATION STATEMENT REPORT";
                mergedCell.Style.Font.Bold = true;
                mergedCell.Style.Fill.BackgroundColor = XLColor.White;
                mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                mergedCell.Style.Font.FontColor = XLColor.Black;
                currentRow++;

                // Add header for sales details
                worksheet.Cell(currentRow, 1).Value = "DATE";
                worksheet.Cell(currentRow, 2).Value = "SHIFT CODE";
                worksheet.Cell(currentRow, 3).Value = "SHIFT CATEGORY";
                worksheet.Cell(currentRow, 4).Value = "ATTENDANT";
                worksheet.Cell(currentRow, 5).Value = "STATUS";
                worksheet.Cell(currentRow, 6).Value = "GAIN OR LOSS";
                worksheet.Cell(currentRow, 7).Value = "% GAIN OR LOSS";
                worksheet.Cell(currentRow, 8).Value = "OPEN READ";
                worksheet.Cell(currentRow, 9).Value = "CLOSE READ";
                worksheet.Cell(currentRow, 10).Value = "SALES";
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.Bold = true;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;
                if (data != null)
                {
                    if (data.Stationshiftstatements != null)
                    {
                        foreach (var report in data.Stationshiftstatements)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.ShiftDateTime;
                            worksheet.Cell(currentRow, 2).Value = report.ShiftCode;
                            worksheet.Cell(currentRow, 3).Value = report.ShiftCategory;
                            worksheet.Cell(currentRow, 4).Value = report.Attendant;
                            worksheet.Cell(currentRow, 5).Value = report.ShiftStatus;
                            worksheet.Cell(currentRow, 6).Value = report.ShiftStatusGain;
                            worksheet.Cell(currentRow, 7).Value = report.PercentShiftStatusGain;
                            worksheet.Cell(currentRow, 8).Value = report.TotalOpeningRead;
                            worksheet.Cell(currentRow, 9).Value = report.TotalClosingRead;
                            worksheet.Cell(currentRow, 10).Value = report.TotalSales;
                        }
                        // Calculate grand totals
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 7).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 10).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 8).Value = data.Stationshiftstatements.Sum(report => report.TotalOpeningRead);
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 9).Value = data.Stationshiftstatements.Sum(report => report.TotalClosingRead);
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        worksheet.Cell(currentRow, 10).Value = data.Stationshiftstatements.Sum(report => report.TotalSales);
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Stationpurchasesstatementreport.xlsx");
                }
            }
        }

        #endregion
        public async Task<JsonResult> GetsystemstationshiftdetailbyId(long Id)
        {
            var resp = await bl.GetSystemDropDownDataById(SessionUserData.Token,ListModelType.Systemstationshifts, Id);
            return Json(resp);
        }
        public async Task<JsonResult> Getsystemstationtankshiftpurchasedata(long ShiftId,long TankId)
        {
            var resp = await bl.Getsystemstationtankshiftpurchasedata(SessionUserData.Token, ShiftId, TankId);
            return Json(resp);
        }
        public async Task<JsonResult> Getsystemstationdryproductshiftpurchasedata(long ShiftId, long ProductId)
        {
            var resp = await bl.Getsystemstationdryproductshiftpurchasedata(SessionUserData.Token, ShiftId, ProductId);
            return Json(resp);
        }
    }
}
