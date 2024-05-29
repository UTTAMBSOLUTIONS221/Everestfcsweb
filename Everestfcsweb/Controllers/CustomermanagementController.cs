
using DocumentFormat.OpenXml.Drawing;
using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Helpers;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CustomerManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public CustomerManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }

        #region User Signin
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Signincustomer(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signincustomer(Userloginmodel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var resp = await bl.Validatecustomeruser(model);
            if (resp.RespStatus == 200)
            {
                SetCustomerLoggedIn(resp, false);
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
            return RedirectToAction("Signinuser", "Account");

        }
        #endregion

        public async Task<IActionResult> Dashboard()
        {
            var data = await bl.GetSystemCustomerDetailData(SessionCustomerData.Token, SessionCustomerData.CustomerModel.CustomerId);
            return View(data);
        }
        public async Task<IActionResult> Index(int Offset = 0, int Count = 10)
        {
            var data = await bl.Getsystemtenantcustomers(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, Offset, Count);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Addsystemindcustomer()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Addsystemcorcustomer()
        {
            ViewData["Systemphonecodelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCountrylists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Country).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Addsystemcustomer(long? CustomerId)
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemphonecodelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemPhoneCodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (CustomerId > 0)
            {
                var data = await bl.GetSystemCustomerData(SessionUserData.Token, CustomerId);
                if (data.Designation == "Corporate")
                {
                    return PartialView("Editcorporatecustomer", data);
                }
                else
                {
                    return PartialView("Editindividualcustomer", data);
                }
            }
            return PartialView();
        }
        public async Task<JsonResult> Addsystemcustomer(CompanyCustomer model)
        {
            var resp = await bl.Addsystemcorcustomer(SessionUserData.Token, model);
            return Json(resp);
        }

        #region Customer Details
        [HttpGet]
        public async Task<IActionResult> Details(long CustomerId)
        {
            var data = await bl.GetSystemCustomerDetailData(SessionUserData.Token, CustomerId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerAgreementPriceList(long PriceListId)
        {
            var data = await bl.GetSystemCustomerAgreementPriceListData(SessionUserData.Token, PriceListId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerAgreementDiscountList(long DiscountListId)
        {
            var data = await bl.GetSystemCustomerAgreementDiscountListData(SessionUserData.Token, DiscountListId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerAgreementTopupTransferList(long AccountId)
        {
            var data = await bl.GetSystemCustomerAgreementTopupTransferListData(SessionUserData.Token, AccountId);
            return PartialView(data);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerAgreementPaymentList(long AgreementId)
        {
            var data = await bl.GetSystemCustomerAgreementPaymentListData(SessionUserData.Token, AgreementId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> Customeraccountdetail(long AccountId)
        {
            var data = await bl.GetCustomerAccountDetailData(SessionUserData.Token, AccountId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Customerportalaccountdetail(long AccountId)
        {
            var data = await bl.GetCustomerAccountDetailData(SessionCustomerData.Token, AccountId);
            return View(data);
        }
        #endregion

        #region Customer Agreements
        [HttpGet]
        public IActionResult Addcustomeragreement(long CustomerId)
        {
            CustomerAgreementData model = new CustomerAgreementData();
            model.CustomerId = CustomerId;
            ViewData["SystemLoyaltyGroupinglists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Loyaltygrouping).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPricelists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Pricelist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemDiscountlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Discountlist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemConsumptionTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.ConsumptionType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }

        public async Task<JsonResult> Addsystemcustomerprepaidagreementdata(CustomerPrepaidAgreement model)
        {
            var resp = await bl.Addsystemcustomerprepaidagreement(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Editcustomerprepaidagreement(long CustomerAgreementId)
        {
            ViewData["SystemLoyaltyGroupinglists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Loyaltygrouping).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPricelists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Pricelist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemDiscountlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Discountlist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            var data = await bl.Getcustomerprepaidagreementdatabyid(SessionUserData.Token, CustomerAgreementId);
            return PartialView(data);
        }

        public async Task<JsonResult> Addsystemcustomerpostpaidrecurrentagreementdata(PostpaidRecurentAgreement model)
        {
            var resp = await bl.Addsystemcustomerpostpaidrecurrentagreementdata(SessionUserData.Token, model);
            return Json(resp);
        }

        public async Task<JsonResult> Addsystemcustomerpostpaidoneoffagreementdata(PostpaidOneOffAgreement model)
        {
            var resp = await bl.Addsystemcustomerpostpaidoneoffagreementdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Addsystemcustomerpostpaidcreditagreementdata(CustomerCreditAgreement model)
        {
            var resp = await bl.Addsystemcustomerpostpaidcreditagreementdata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> Editpostpaidcreditagreement(long CustomerAgreementId)
        {
            ViewData["SystemLoyaltyGroupinglists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Loyaltygrouping).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPricelists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Pricelist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemDiscountlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Discountlist, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            var data = await bl.Getcustomerpostpaidcreditagreementdatabyid(SessionUserData.Token, CustomerAgreementId);
            return PartialView(data);
        }
        #endregion

        #region Customer Accounts
        [HttpGet]
        public async Task<IActionResult> AddcustomeragreementAccount(long CustomerAgreementId)
        {
            CustomerAgreementAccountData model = new CustomerAgreementAccountData();
            model.AgreementId = CustomerAgreementId;
            ViewData["SystemEquimentMakeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemEquipmentMakes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemConsumptionTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.ConsumptionType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCardTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.CardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddcustomerportalagreementAccount(long CustomerAgreementId)
        {
            CustomerAgreementAccountData model = new CustomerAgreementAccountData();
            model.AgreementId = CustomerAgreementId;
            ViewData["SystemEquimentMakeslists"] = bl.GetSystemDropDownData(SessionCustomerData.Token, ListModelType.SystemEquipmentMakes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemConsumptionTypelists"] = bl.GetSystemDropDownData(SessionCustomerData.Token, ListModelType.ConsumptionType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemCardTypelists"] = bl.GetSystemDropDownData(SessionCustomerData.Token, ListModelType.CardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionCustomerData.Token, ListModelType.Stations, SessionCustomerData.CustomerModel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionCustomerData.Token, ListModelType.SystemProduct, SessionCustomerData.CustomerModel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        
        public async Task<JsonResult> AddcustomeragreementAccountdata(CustomerAgreementAccountData model)
        {
            var resp = await bl.AddcustomeragreementAccount(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Viewcustomeragreementaccountdetail(long AccountId)
        {
            var data = await bl.GetCustomerAccountDetailData(SessionUserData.Token, AccountId);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Viewcustomeraccountdetail(long AccountId)
        {
            var data = await bl.GetCustomerAccountDetailData(SessionUserData.Token, AccountId);
            return PartialView(data);
        }


        #region Replace Customer Account Card
        [HttpGet]
        public async Task<IActionResult> Replacecustomeraccountcard(long AccountId, long CardId, string Masknumber)
        {
            AccountCardReplaceDetails model = new AccountCardReplaceDetails();
            model.AccountId = AccountId;
            model.MaskSno = Masknumber;
            model.CardId = CardId;
            ViewData["SystemCardTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.CardType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Replacecustomeraccountcarddata(AccountCardReplaceDetails model)
        {
            var resp = await bl.Replacecustomeraccountcarddata(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion



        #region Customer Account Policies

        [HttpGet]
        public async Task<IActionResult> Customeraccountpoliciesdetail(long AccountId, string Masknumber)
        {
            CustomerAccountDetails model = new CustomerAccountDetails();
            model.AccountId = AccountId;
            model.MaskNumber = Masknumber;
            model = await bl.GetSystemCustomerAccountPolicyDetailData(SessionUserData.Token, AccountId);
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> Customerportalaccountpoliciesdetail(long AccountId, string Masknumber)
        {
            CustomerAccountDetails model = new CustomerAccountDetails();
            model.AccountId = AccountId;
            model.MaskNumber = Masknumber;
            model = await bl.GetSystemCustomerAccountPolicyDetailData(SessionCustomerData.Token, AccountId);
            return PartialView(model);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountproductpolicy(long AccountId, long? AccountProductId, string Masknumber)
        {
            AccountProductpolicy model = new AccountProductpolicy();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (AccountProductId > 0)
            {
                model = await bl.GetcustomeraccountproductpolicyData(SessionUserData.Token, AccountProductId);
            }
            model.AccountId = AccountId;
            model.Masknumber = Masknumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountproductpolicydata(AccountProductpolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountProductPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountstationpolicy(long AccountId, long? AccountStationId, string Masknumber)
        {
            AccountStationspolicy model = new AccountStationspolicy();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (AccountStationId > 0)
            {
                model = await bl.Getcustomeraccountstationpolicydata(SessionUserData.Token, AccountStationId);
            }
            model.AccountId = AccountId;
            model.Masknumber = Masknumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountstationpolicydata(AccountStationspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountStationPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountweekdaypolicy(long AccountId, long? AccountWeekDaysId, string Masknumber)
        {
            AccountWeekDayspolicy model = new AccountWeekDayspolicy();
            if (AccountWeekDaysId > 0)
            {
                model = await bl.Getcustomeraccountweekdaypolicydata(SessionUserData.Token, AccountWeekDaysId);
                model.SelectWeekDays = model.WeekDays.Split(',').ToList();
            }
            model.AccountId = AccountId;
            model.Masknumber = Masknumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountweekdayypolicydata(AccountWeekDayspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountWeekdayPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountfrequencypolicy(long AccountId, long? AccountFrequencyId, string Masknumber)
        {
            AccountTransactionFrequencypolicy model = new AccountTransactionFrequencypolicy();
            if (AccountFrequencyId > 0)
            {
                model = await bl.Getcustomeraccountfrequencypolicydata(SessionUserData.Token, AccountFrequencyId);
            }
            model.AccountId = AccountId;
            model.Masknumber = Masknumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountfrequencypolicydata(AccountTransactionFrequencypolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountFrequencyPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion
        public IActionResult AddcustomeragreementAccountlevelTopup(long AccountId)
        {
            CustomerAccountTopup model = new CustomerAccountTopup();
            model.AccountId = AccountId;
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemStationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public IActionResult AddcustomeragreementAccountTopup(long AccountId)
        {
            CustomerAccountTopup model = new CustomerAccountTopup();
            model.AccountId = AccountId;
            ViewData["SystemStationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> AddcustomeragreementAccounttopupdata(CustomerAccountTopup model)
        {
            model.TransactionDescription = "Topup";
            var resp = await bl.AddcustomeragreementAccounttopupdata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public IActionResult AddcustomeragreementTransfer(long AccountId)
        {
            CustomerAccountTransfer model = new CustomerAccountTransfer();
            model.FromAccountId = AccountId;
            ViewData["Systemagreementaccountlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemAccountNumbers, AccountId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        [HttpGet]
        public IActionResult AddcustomerportalagreementTransfer(long AccountId)
        {
            CustomerAccountTransfer model = new CustomerAccountTransfer();
            model.FromAccountId = AccountId;
            ViewData["Systemagreementaccountlists"] = bl.GetSystemDropDownDataById(SessionCustomerData.Token, ListModelType.SystemAccountNumbers, AccountId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult AddcustomeragreementAccountTransfer(long AccountId)
        {
            CustomerAccountTransfer model = new CustomerAccountTransfer();
            model.FromAccountId = AccountId;
            ViewData["Systemagreementaccountlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemAgreementAccountNumbers, AccountId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> AddcustomeragreementAccountTransferdata(CustomerAccountTransfer model)
        {
            model.TransactionDescription = "TopupTransfer";
            var resp = await bl.AddcustomeragreementAccountTransferdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public IActionResult Addcustomeragreementpayment(long AgreementId)
        {
            CustomerAgreementPayment model = new CustomerAgreementPayment();
            model.AgreementId = AgreementId;
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeragreementpaymentdata(CustomerAgreementPayment model)
        {
            var resp = await bl.Addcustomeragreementpaymentdata(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Reversetopup(ReverseSaleRequestData model)
        {
            var resp = await bl.PostReversetopupData(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> Reversepayment(ReverseSaleRequestData model)
        {
            var resp = await bl.PostReversepaymentData(SessionUserData.Token, model);
            return Json(resp);
        }
        public IActionResult AgreementAccountOfflineSale(long AccountId, string Accountmask)
        {
            CustomerAccountTopup model = new CustomerAccountTopup();
            model.AccountId = AccountId;
            ViewData["SystemPaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }

        #endregion

        #region Customer Account Employees
        [HttpGet]
        public async Task<IActionResult> AddCustomerAccountEmployee(long AccountId, long? EmployeeId)
        {
            CustomerAccountEmployees model = new CustomerAccountEmployees();
            if (EmployeeId > 0)
            {
                model = await bl.GetCustomerAccountEmployeeById(SessionUserData.Token, EmployeeId);
            }
            model.AccountId = AccountId;
            return PartialView(model);
        }
        public async Task<JsonResult> AddCustomerAccountEmployeedata(CustomerAccountEmployees model)
        {
            var resp = await bl.AddCustomerAccountEmployeedata(SessionUserData.Token, model);
            return Json(resp);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerAccountEmployeePolicies(long EmployeeId)
        {
            var data = await bl.GetCustomerAccountEmployeePoliciesdata(SessionUserData.Token, EmployeeId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerPortalAccountEmployeePolicies(long EmployeeId)
        {
            var data = await bl.GetCustomerAccountEmployeePoliciesdata(SessionCustomerData.Token, EmployeeId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountemployeeproductpolicy(long EmployeeId, long? EmployeeProductId, string EmployeeName)
        {
            AccountEmployeeProductpolicy model = new AccountEmployeeProductpolicy();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (EmployeeProductId > 0)
            {
                model = await bl.Getcustomeraccountemployeeproductpolicydata(SessionUserData.Token, EmployeeProductId);
            }
            model.EmployeeId = EmployeeId;
            model.EmployeeName = EmployeeName;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountemployeeproductpolicydata(AccountEmployeeProductpolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEployeeProductPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountemployeestationpolicy(long EmployeeId, string EmployeeName)
        {
            AccountEmployeeStationspolicy model = new AccountEmployeeStationspolicy();
            model.EmployeeId = EmployeeId;
            model.EmployeeName = EmployeeName;

            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountemployeestationpolicydata(AccountEmployeeStationspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEmployeeStationPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountemployeeweekdaypolicy(long EmployeeId, long? EmployeeWeekDaysId, string EmployeeName)
        {
            AccountEmployeeWeekDayspolicy model = new AccountEmployeeWeekDayspolicy();
            if (EmployeeWeekDaysId > 0)
            {
                model = await bl.Getcustomeraccountemployeeweekdaypolicydata(SessionUserData.Token, EmployeeWeekDaysId);
                model.SelectWeekDays = model.WeekDays.Split(',').ToList();
            }
            model.EmployeeId = EmployeeId;
            model.EmployeeName = EmployeeName;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountemployeeweekdaypolicydata(AccountEmployeeWeekDayspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEmployeeWeekdayPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountemployeefrequencypolicy(long EmployeeId, long? EmployeeFrequencyId, string EmployeeName)
        {
            AccountEmployeeTransactionFrequencypolicy model = new AccountEmployeeTransactionFrequencypolicy();
            if (EmployeeFrequencyId > 0)
            {
                model = await bl.Getcustomeraccountemployeefrequencypolicydata(SessionUserData.Token, EmployeeFrequencyId);
            }
            model.EmployeeId = EmployeeId;
            model.EmployeeName = EmployeeName;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountemployeefrequencypolicydata(AccountEmployeeTransactionFrequencypolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEmployeeFrequencyPolicypData(SessionUserData.Token, model);
            return Json(resp);
        }

        #endregion

        #region Customer Account Equipment
        [HttpGet]
        public async Task<IActionResult> AddCustomerAccountEquipment(long AccountId, long? EquipmentId)
        {
            CustomerAccountEquipments model = new CustomerAccountEquipments();
            model.AccountId = AccountId;
            ViewData["SystemEquimentMakeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.SystemEquipmentMakes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemConsumptionTypelists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.ConsumptionType).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            if (EquipmentId > 0)
            {
                model = await bl.GetCustomerAccountEquipmentdata(SessionUserData.Token, EquipmentId);
            }
            return PartialView(model);
        }
        public async Task<JsonResult> AddCustomerAccountEquipmentdata(CustomerAccountEquipments model)
        {
            var resp = await bl.AddCustomerAccountEquipmentdata(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerAccountEquipmentPolicies(long EquipmentId)
        {
            var data = await bl.GetCustomerAccountEquipmentPoliciesdata(SessionUserData.Token, EquipmentId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerPortalAccountEquipmentPolicies(long EquipmentId)
        {
            var data = await bl.GetCustomerAccountEquipmentPoliciesdata(SessionCustomerData.Token, EquipmentId);
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountequipmentproductpolicy(long EquipmentId, long? EquipmentProductId, string EquipmentNumber)
        {
            AccountEquipmentProductpolicy model = new AccountEquipmentProductpolicy();
            if (EquipmentProductId > 0)
            {
                model = await bl.Getcustomeraccountequipmentproductpolicydata(SessionUserData.Token, EquipmentProductId);
            }
            model.EquipmentId = EquipmentId;
            model.EquipmentNumber = EquipmentNumber;
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountequipmentproductpolicydata(AccountEquipmentProductpolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEquipmentProductPolicyData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountequipmentstationpolicy(long EquipmentId, string EquipmentNumber)
        {
            AccountEquipmentStationspolicy model = new AccountEquipmentStationspolicy();
            model.EquipmentId = EquipmentId;
            model.EquipmentNumber = EquipmentNumber;

            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountequipmentstationpolicydata(AccountEquipmentStationspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEquipmentStationPolicyData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountequipmentweekdaypolicy(long EquipmentId, long? EquipmentWeekDaysId, string EquipmentNumber)
        {
            AccountEquipmentWeekDayspolicy model = new AccountEquipmentWeekDayspolicy();
            if (EquipmentWeekDaysId > 0)
            {
                model = await bl.Getcustomeraccountequipmentweekdaypolicydata(SessionUserData.Token, EquipmentWeekDaysId);
                model.SelectWeekDays = model.WeekDays.Split(',').ToList();
            }
            model.EquipmentId = EquipmentId;
            model.EquipmentNumber = EquipmentNumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountequipmentweekdaypolicydata(AccountEquipmentWeekDayspolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEquipmentWeekdayPolicyData(SessionUserData.Token, model);
            return Json(resp);
        }
        [HttpGet]
        public async Task<IActionResult> Addcustomeraccountequipmentfrequencypolicy(long EquipmentId, long? EquipmentFrequencyId, string EquipmentNumber)
        {
            AccountEquipmentTransactionFrequencypolicy model = new AccountEquipmentTransactionFrequencypolicy();
            if (EquipmentFrequencyId > 0)
            {
                model = await bl.Getcustomeraccountequipmentfrequencypolicydata(SessionUserData.Token, EquipmentFrequencyId);
            }
            model.EquipmentId = EquipmentId;
            model.EquipmentNumber = EquipmentNumber;
            return PartialView(model);
        }
        public async Task<JsonResult> Addcustomeraccountequipmentfrequencypolicydata(AccountEquipmentTransactionFrequencypolicy model)
        {
            var resp = await bl.RegisterCustomerAgreementAccountEquipmentFrequencyPolicyData(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Delete,Deactivate Actions
        public async Task<JsonResult> AllDeleteDeativateActions(ActivateDeactivateActions model)
        {
            var resp = await bl.DeactivateorDeleteTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> AllRemoveDeativateActions(ActivateDeactivateActions model)
        {
            var resp = await bl.RemoveTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        public async Task<JsonResult> AllSingleDefaultActions(ActivateDeactivateActions model)
        {
            var resp = await bl.DefaultThisTableColumnData(SessionUserData.Token, model);
            return Json(resp);
        }
        #endregion

        #region Offline sales
        [HttpGet]
        public IActionResult AgreementAccountOfflineSale(long CustomerId, long AccountId)
        {
            AgreementAccountOfflineSale model = new AgreementAccountOfflineSale();
            model.CustomerId = CustomerId;
            model.AccountId = AccountId;
            ViewData["Systemstationslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Stations).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView(model);
        }

        public JsonResult AddProductdatatocart(ProductModel model)
        {
            String[] str = new String[] { "", "" };
            List<CartItem> cart = new List<CartItem>();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                cart.Add(new CartItem { Product = model, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(model.ProductVariationId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = model, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            str[0] = GetSaleItem().ProductList;

            return Json(str[0]);
        }
        //get items in cart
        public SaleDetail GetSaleItem()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            SaleDetail sDetail = new SaleDetail();
            string ProductList = "";
            decimal SubTotal = 0, TotalItem = 0;
            string? Currency = "UGX";

            if (cart.Count() > 0)
            {
                ProductList += "<table class=\"table table-condensed table-bordered\" style=\"font-size:10px;\"><thead><tr><th></th><th width=\"35%\" class=\"name\" >Product</th><th width=\"20%\">Price</th><th width=\"15%\">Qty</th><th width=\"20%\">Amount</th><th width=\"10%\">Remove</th></tr></thead><tbody>";
                foreach (var t in cart)
                {
                    var price = t.Product.ProductPrice;
                    string SellRate = price.ToString();
                    string Total = (Convert.ToDecimal(SellRate) * t.Quantity).ToString();
                    ProductList += string.Format("<tr><td>{2}</td><td class=\"name\">{0}</td><td>{5}</td><td><input style=\"height:unset;padding:2px;\" type=\"number\" class=\"form-control qty\" onchange=\"UpdateSaleItem('{2}',this.value)\" value=\"{1}\"></td><td><input style=\"height:unset;padding:2px;\" type=\"number\" class=\"form-control\" onchange=\"UpdateAmountItem('{2}',this.value)\"  value=\"{4}\"></td><td style=\"text-align: center;\"><a onclick=\"DeleteSaleItem('{2}')\" class=\"delete\"><i class=\"fa fa-remove\"></i></a></td></tr>", t.Product.ProductName, Math.Round(t.Quantity, 2), t.Product.ProductVariationId, t.Product.ProductCode, Math.Round(t.Quantity * t.Product.ProductPrice, 2), t.Product.ProductPrice);
                    SubTotal += Convert.ToDecimal(Total);
                    TotalItem += t.Quantity;

                }
                ProductList += string.Format("</tbody>");
                ProductList += "</tbody></table>";
            }
            if (ProductList == "")
            {
                ProductList += string.Format("<table class=\"table table-condensed table-bordered\"><thead><tr><th width=\"55%\" class=\"name\" >Product</th><th width=\"30%\">Qty</th><th width=\"15%\">Remove</th></tr></thead><tbody><tr><td></td><td><input id=\"dtyNot\" type=\"number\" class=\"form-control qty\" value=\"0\" readonly ></td><td></td></tr></tbody></table>");
            }
            sDetail.ProductList = ProductList;
            return sDetail;


        }
        //Delete Sale Item
        public JsonResult DeleteSaleItem(long ProductVariationId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(ProductVariationId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(GetSaleItem().ProductList);
        }
        //Update Sale Item
        public JsonResult UpdateSaleItem(long ProductVariationId, decimal Qty)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(ProductVariationId);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(GetSaleItem().ProductList);
        }
        private int isExist(long id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductVariationId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion

        #region drop down data
        [HttpGet]
        public JsonResult GetSystemDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSEquipmentModelDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemEquipmentModel, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetsystemcardDropDownDatawiththistype(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCardModel, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSystemStationStaffDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult Getsystemcustomerequipmentdata(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomerEquipments, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        #endregion

        #region Other Methods

        private async void SetCustomerLoggedIn(CustomermodelResponce customer, bool rememberMe)
        {
            string customerData = JsonConvert.SerializeObject(customer);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerModel.CustomerId.ToString()),
                new Claim(ClaimTypes.Name, customer.CustomerModel.Companyname),
                new Claim("FullNames", customer.CustomerModel.Companyname),
                new Claim("CustomerId", customer.CustomerModel.CustomerId.ToString()),
                new Claim("Token", customer.Token),
                new Claim("customerData", customerData),
            };

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
                return RedirectToAction(nameof(CustomerManagementController.Dashboard), "CustomerManagement", new { area = "" });
            }
            else if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(CustomerManagementController.Dashboard), "CustomerManagement", new { area = "" });
            }
        }
        #endregion

        [HttpGet, HttpPost]
        public async Task<IActionResult> Resendcustomerpassword(long CustomerId)
        {
            var Resp = await bl.Resendcustomerpassword(SessionUserData.Token, CustomerId);
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
