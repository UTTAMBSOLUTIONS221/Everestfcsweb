using Everestfcsweb.Entities;
using Everestfcsweb.Entities.Reports;
using Everestfcsweb.Enum;
using Everestfcsweb.Models;
using Everestfcsweb.Models.Reports;
using Everestfcsweb.Models.Reports.ShiftSummary;
using Newtonsoft.Json;
using System.Text;

namespace Everestfcsweb.Services
{
    public class FuelproReportDataServices
    {
        string BaseUrl = "";
        public FuelproReportDataServices(IConfiguration config)
        {
            BaseUrl = Util.Currenttenantbaseurlstring(config);
        }

        #region Sales Reciept
        public async Task<SingleFinanceTransactions> Getsingleofflinesalesdata(string Tokenbearer, long FinanceTransactionId, long AccountId)
        {
            SingleFinanceTransactions Data = new SingleFinanceTransactions();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SaleTransaction/Getsingleofflinesalesdata/" + FinanceTransactionId +"/" +AccountId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SingleFinanceTransactions>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Sales Transaction Data
        public async Task<SalesTransactionsDetailsData> Generatesalestransactiondata(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            SalesTransactionsDetailsData Data = new SalesTransactionsDetailsData();
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    TenantId = TenantId,
                    Customer = customer,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetSalesTransactionsReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<SalesTransactionsDetailsData>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Postpaid Statement Data
        public async Task<CustomerPostpaidStatementDataReport> Generatecustomerpostpaidstatementdata(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerPostpaidStatementDataReport Data = new CustomerPostpaidStatementDataReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId = TenantId,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");

                var data= JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId = TenantId,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                });
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPostpaidStatementReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPostpaidStatementDataReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Payment Data
        public async Task<CustomerPaymentDataReport> GetCustomerPaymentReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerPaymentDataReport Data = new CustomerPaymentDataReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId = TenantId,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPaymentReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPaymentDataReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Prepaid Statement Data
        public async Task<CustomerPrepaidStatementReportData> GetCustomerPrepaidStatementReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerPrepaidStatementReportData Data = new CustomerPrepaidStatementReportData();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId = TenantId,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPrepaidStatementReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPrepaidStatementReportData>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Topup Data
        public async Task<CustomerTopupDataReport> GetCustomerTopupReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerTopupDataReport Data = new CustomerTopupDataReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId = TenantId,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerTopupReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerTopupDataReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Point Cumulative Data
        public async Task<CustomerCumulativeDataReport> Getcustomercumulativereportdata(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerCumulativeDataReport Data = new CustomerCumulativeDataReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    TenantId= TenantId,
                    Customer = customer,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerCumulativeReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerCumulativeDataReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Point Statement Data
        public async Task<CustomerPointStatementReport> GetCustomerPointStatementReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerPointStatementReport Data = new CustomerPointStatementReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    TenantId = TenantId,
                    Customer = customer,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPointStatementReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPointStatementReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Point Award Data
        public async Task<CustomerPointAwardReport> GetCustomerPointAwardReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long agreement, long group, long station, long paymode, long account, long attendant, long product)
        {
            CustomerPointAwardReport Data = new CustomerPointAwardReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    TenantId = TenantId,
                    Customer = customer,
                    Agreement = agreement,
                    Group = group,
                    Station = station,
                    Paymode = paymode,
                    Account = account,
                    Attendant = attendant,
                    Product = product
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPointAwardReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPointAwardReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Point Redeeme Data
        public async Task<CustomerPointRedeemReport> GetCustomerPointRedeemReportData(string Tokenbearer,long TenantId, DateTime startdate, DateTime enddate, long customer, long station, long account, long attendant)
        {
            CustomerPointRedeemReport Data = new CustomerPointRedeemReport();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(new ReportParameters
                {
                    Startdate = startdate,
                    Enddate = enddate,
                    Customer = customer,
                    TenantId=TenantId,
                    Station = station,
                    Account = account,
                    Attendant = attendant,
                }), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/GetCustomerPointRedeemReportData", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<CustomerPointRedeemReport>(outPut);
                }
            }
            return Data;
        }
        #endregion

        #region Station Shift  Summary
        #region Shift Pump Reading
        public async Task<ShiftPumpReadingReport> Generateshiftpumpreadingdata(string Tokenbearer,dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftPumpReadingReport resp = new ShiftPumpReadingReport();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftpumpreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftPumpReadingReport>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Tank Reading
        public async Task<ShiftTankReadingDetails> Generateshifttankreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftTankReadingDetails resp = new ShiftTankReadingDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshifttankreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftTankReadingDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Lube Lpg Reading
        public async Task<ShiftLpgLubeReadingDetails> Generateshiftlubelpgreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftLpgLubeReadingDetails resp = new ShiftLpgLubeReadingDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftlubelpgreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftLpgLubeReadingDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Expenses Reading
        public async Task<ShiftExpensesDetails> Generateshiftexpensesreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftExpensesDetails resp = new ShiftExpensesDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftexpensesreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftExpensesDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Credit Sale Reading
        public async Task<ShiftCreditSalesDetails> Generateshiftcreditsalereadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftCreditSalesDetails resp = new ShiftCreditSalesDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftcreditsalereadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftCreditSalesDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Cash Drop Reading
        public async Task<ShiftCashDropsDetails> Generateshiftcashdropreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftCashDropsDetails resp = new ShiftCashDropsDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftcashdropreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftCashDropsDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Shift Purchases Reading
        public async Task<ShiftpurchasesReadingDetails> Generateshiftpurchasesreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftpurchasesReadingDetails resp = new ShiftpurchasesReadingDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generateshiftpurchasereadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftpurchasesReadingDetails>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Station Shift Reading
        public async Task<StationShiftDetailsData> Generatestationshiftreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            StationShiftDetailsData resp = new StationShiftDetailsData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generatestationshiftreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<StationShiftDetailsData>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Station Shift Summary Reading
        public async Task<StationShiftSummaryDetailsData> Generatestationshiftsummaryreadingdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            StationShiftSummaryDetailsData resp = new StationShiftSummaryDetailsData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generatestationshiftsummaryreadingreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<StationShiftSummaryDetailsData>(outPut);
                }
            }
            return resp;
        }
        #endregion

        #region Station Customer Statement Reading
        public async Task<ShiftCustomerStatementData> Generatestationshiftcustomerstatementreportdata(string Tokenbearer, dynamic obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            ShiftCustomerStatementData resp = new ShiftCustomerStatementData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/ReportManagement/Generatestationshiftcustomerstatementreportdata", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ShiftCustomerStatementData>(outPut);
                }
            }
            return resp;
        }
        #endregion
        #endregion

        #region System Dropdown Data
        public async Task<List<ListModel>> GetSystemDropDownData(string Tokenbearer, ListModelType listType)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemDropdown/Systemdropdowns?listType=" + (int)listType))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        public async Task<List<ListModel>> GetSystemDropDownDataById(string Tokenbearer, ListModelType listType, long Id)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemDropdown/SystemdropdownbyId?listType=" + (int)listType + "&Id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        public async Task<List<ListModel>> GetSystemDropDownDataById(string Tokenbearer, long TenantId, ListModelType listType, long Id)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemDropdown/SystemdropdownbyIdandTenantId?listType=" + (int)listType + "&TenantId=" + TenantId + "&Id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        #endregion


        #region System Tenant Dropdown Data
        public async Task<List<ListModel>> GetSystemTenantDropDownData(string Tokenbearer, ListModelType listType)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemTenantDropdown/Systemdropdowns?listType=" + (int)listType))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        public async Task<List<ListModel>> GetSystemTenantDropDownDataById(string Tokenbearer, ListModelType listType, long Id)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemTenantDropdown/SystemdropdownbyId?listType=" + (int)listType + "&Id=" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        #endregion

    }
}
