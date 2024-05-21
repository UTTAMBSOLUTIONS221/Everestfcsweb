using Everestfcsweb.Enum;
using Everestfcsweb.Entities;
using Everestfcsweb.Models;
using Everestfcsweb.Models.Getendpoitmodels;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Collections.Generic;

namespace Everestfcsweb.Services
{
    public class Fuelprodataservices
    {
        string BaseUrl = "";
        public Fuelprodataservices(IConfiguration config)
        {
            BaseUrl = Util.Currenttenantbaseurlstring(config);
        }
        #region Login User
        public async Task<UsermodelResponce> Validateuser(Userloginmodel obj)
        {
            UsermodelResponce userModel = new UsermodelResponce { };
            var resp = await POSTTOAPILOGIN("/api/Everestfcsauthentication/Authenticate", obj);
            if (resp.RespStatus == 200)
            {
                userModel = new UsermodelResponce
                {
                    Token = resp.Token,
                    Usermodel = resp.Usermodel,
                    RespStatus = resp.RespStatus,
                    RespMessage = resp.RespMessage,
                };
            }
            else
            {
                userModel.RespStatus = 401;
                userModel.RespMessage = "Incorrect Password!";
            }

            return userModel;
        }
        #endregion

        #region  Reset Password Data
        public async Task<Genericmodel> Resetuserpasswordpost(Staffresetpassword obj)
        {
            Genericmodel resp = new Genericmodel();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/StaffManagemet/Resetuserpasswordpost", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Genericmodel>(outPut);
                }
            }
            return resp;
        }
        public async Task<Genericmodel> Forgotpasswordpost(StaffForgotPassword obj)
        {
            Genericmodel model = new Genericmodel();
            UsermodelResponce resp = new UsermodelResponce();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + "/api/StaffManagemet/Forgotuserpasswordpost", content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<UsermodelResponce>(outPut);
                }
            }
            if (resp.Usermodel != null)
            {
                model = await UNAUTHPOSTTOAPI("/api/StaffManagemet/Resetuserpasswordpost", new Emailsendingdata { Fullname = resp.Usermodel.Fullname, Username = resp.Usermodel.Username, Emailaddress = resp.Usermodel.Emailaddress, Password = resp.Usermodel.Passwords, Passharsh = resp.Usermodel.Passharsh });
            }
            else
            {
                model.RespStatus = 1;
                model.RespMessage = "Email not sent";
            }
            return model;
        }
        #endregion

        #region System Permissions
        public async Task<IEnumerable<Systempermissions>> Getsystempermissiondata(string Tokenbearer)
        {
            IEnumerable<Systempermissions> Data = new List<Systempermissions>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/Getsystempermissiondata"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<Systempermissions>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Registersystempermissiondata(string Tokenbearer, Systempermissions obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SettingsManagement/Registersystempermissiondata", obj);
            return resp;
        }

        public async Task<Systempermissions> Getsystempermissiondatabyid(string Tokenbearer, long Permissionid)
        {
            Systempermissions Data = new Systempermissions();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/Getsystempermissiondatabyid/" + Permissionid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<Systempermissions>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Communication Templates
        public async Task<IEnumerable<Communicationtemplatedata>> Getsystemcommunicationtemplate(string Tokenbearer)
        {
            List<Communicationtemplatedata> obj = new List<Communicationtemplatedata>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/Communicationtemplate/Getsystemcommunicationtemplatedata"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<List<Communicationtemplatedata>>(apiResponse);
                }
            }
            return obj;
        }
        public async Task<Communicationtemplate> Getsystemcommunicationtemplatedatabyid(string Tokenbearer, long? TemplateId)
        {
            Communicationtemplate obj = new Communicationtemplate();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/Communicationtemplate/Getsystemcommunicationtemplatedatabyid/" + TemplateId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Communicationtemplate>(apiResponse);
                }
            }
            return obj;
        }
        public async Task<Genericmodel> Addcommunicationtemplatedata(string Tokenbearer, Communicationtemplate obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/Communicationtemplate/Registersystemcommunicationtemplatedata", obj);
            return resp;
        }
        #endregion

        #region Settings
        public async Task<IEnumerable<SystemTenantAccountData>> Getsystemtenantaccountdata(string Tokenbearer)
        {
            IEnumerable<SystemTenantAccountData> Data = new List<SystemTenantAccountData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/Getsystemtenantaccountdata"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemTenantAccountData>>(apiResponse);
                }
            }
            return Data;
        }

        public async Task<Genericmodel> Unauthregistertenantaccountdata(SystemTenantAccount obj)
        {
            var resp = await UNAUTHPOSTTOAPI("/api/SettingsManagement/Unauthregistertenantaccountdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Registertenantaccountdata(string Tokenbearer, SystemTenantAccount obj)
        {
            var Json = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/SettingsManagement/Registertenantaccountdata", obj);
            return resp;
        }

        public async Task<SystemTenantAccount> Getsystemtenantaccountbytenantid(string Tokenbearer, long? TenantId)
        {
            SystemTenantAccount Data = new SystemTenantAccount();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/Getsystemtenantaccountbytenantid/" + TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemTenantAccount>(apiResponse);
                }
            }
            return Data;
        }

        public async Task<IEnumerable<LoyaltySettingsModelData>> Getsystemloyaltysettingsdata(string Tokenbearer)
        {
            IEnumerable<LoyaltySettingsModelData> Data = new List<LoyaltySettingsModelData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/Getsystemloyaltysettingsdata"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<LoyaltySettingsModelData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<SystemLoyaltySetings> GetLoyaltySettingsById(string Tokenbearer, long? LoyaltysettId)
        {
            SystemLoyaltySetings Data = new SystemLoyaltySetings();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/GetSystemLoyaltySettingsById/" + LoyaltysettId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemLoyaltySetings>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<SystemLoyaltySetings> GetLoyaltySettings(string Tokenbearer, long? TenantId)
        {
            SystemLoyaltySetings Data = new SystemLoyaltySetings();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SettingsManagement/GetSystemLoyaltySettings/" + TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemLoyaltySetings>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> PostLoyaltySettingsData(string Tokenbearer, SystemLoyaltySetings obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SettingsManagement/RegisterLoyaltySetings", obj);
            return resp;
        }
        #endregion

        #region Tenant System Suppliers
        public async Task<IEnumerable<SupplierDetailData>> Getsystemsupplierslistdata(string Tokenbearer, long Tenantid)
        {
            IEnumerable<SupplierDetailData> Data = new List<SupplierDetailData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemSuppliers/Getsystemsupplierslistdata/" + Tenantid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SupplierDetailData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Registersystemsuppliersdata(string Tokenbearer, SystemSupplier obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/SystemSuppliers/Registersystemsuppliersdata", obj);
            return resp;
        }
        public async Task<SystemSupplier> Getsystemsupplierdetailbyid(string Tokenbearer, long? SupplierId)
        {
            SystemSupplier data = new SystemSupplier();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemSuppliers/Getsystemsupplierdetailbyid/" + SupplierId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<SystemSupplier>(apiResponse);
                }
            }
            return data;
        }
        #endregion

        #region System Roles
        public async Task<IEnumerable<SystemUserRoles>> GetSystemUserRolesData(string Tokenbearer, long Tenantid, int Offset, int Count)
        {
            IEnumerable<SystemUserRoles> Data = new List<SystemUserRoles>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StaffManagemet/GetSystemRoles/" + Tenantid + "/" + Offset + "/" + Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemUserRoles>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addsystemroledata(string Tokenbearer, SystemUserRoles obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StaffManagemet/RegisterSystemStaffRole", obj);
            return resp;
        }
        public async Task<SystemUserRoles> GetSystemRoleDetailData(string Tokenbearer, long? RoleId)
        {
            SystemUserRoles Role = new SystemUserRoles();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StaffManagemet/GetSystemRoleDetailData/" + RoleId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Role = JsonConvert.DeserializeObject<SystemUserRoles>(apiResponse);
                }
            }
            return Role;
        }
        #endregion

        #region StaffUserPermissions
        public static async Task<List<SystemPermissions>> GetTenantStaffPermissions(string Tokenbearer, long Userid)
        {
            List<SystemPermissions> Permissions = new List<SystemPermissions>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Tokenbearer + "/api/StaffManagemet/GetSystemUserPermissions/" + Userid + "/" + true))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Permissions = JsonConvert.DeserializeObject<List<SystemPermissions>>(apiResponse);
                }
            }
            return Permissions;
        }
        #endregion

        #region System Staffs
        public async Task<IEnumerable<SystemStaffsData>> GetSystemStaffsData(string Tokenbearer, long Tenantid, int Offset, int Count)
        {
            IEnumerable<SystemStaffsData> Data = new List<SystemStaffsData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StaffManagemet/GetSystemStaffsData/" + Tenantid + "/" + Offset + "/" + Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemStaffsData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addsystemstaffdata(string Tokenbearer, SystemStaffs obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StaffManagemet/RegisterSystemStaff", obj);
            if (resp.RespStatus == 0)
            {
                await POSTTOAPI(Tokenbearer, "/api/SetupManagement/Registersystemstaffdata", new Genericmodel { Data1 = resp.Data1, Data2 = resp.Data2, Data3 = resp.Data3, Data4 = resp.Data4, Data5 = resp.Data5, Data6 = resp.Data6, Data7 = resp.Data7, Data8 = resp.Data8, Data9 = resp.Data9 });
            }
            return resp;
        }
        public async Task<SystemStaffs> Getsystemstaffdetailtdata(string Tokenbearer, long? Userid)
        {
            SystemStaffs Data = new SystemStaffs();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StaffManagemet/GetSystemStaffById/" + Userid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemStaffs>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Resendstaffpassword(string Tokenbearer, long? Userid)
        {
            Genericmodel model = new Genericmodel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StaffManagemet/Resendsystemstaffpassword/" + Userid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Genericmodel>(apiResponse);
                }
            }

            return model;
        }
        #endregion

        #region System Stationss
        public async Task<IEnumerable<SystemStationData>> GetSystemstationsData(string Tokenbearer, long Tenantid, long StationId, int Offset, int Count)
        {
            IEnumerable<SystemStationData> Data = new List<SystemStationData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/GetSystemstationsData/" + Tenantid + "/" + StationId + "/" + Offset + "/" + Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemStationData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addsystemstationdata(string Tokenbearer, SystemStations obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/RegisterSystemStation", obj);
            return resp;
        }
        public async Task<Genericmodel> Automatesystemstationdata(string Tokenbearer, AutomatedStationData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Automatesystemstationdata", obj);
            return resp;
        }
        public async Task<SystemStations> GetSystemStationDetailDataById(string Tokenbearer, long? StationId)
        {
            SystemStations Data = new SystemStations();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/GetSystemStationDetailDataById/" + StationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemStations>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Systemstationdetailmodel> GetSystemStationallDetailDataById(string Tokenbearer, long? StationId)
        {
            Systemstationdetailmodel data = new Systemstationdetailmodel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemStationallDetailDataById/" + StationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Systemstationdetailmodel>(apiResponse);
                }
            }
            return data;
        }
        #endregion

        #region System Station Tanks
        public async Task<StationTankModel> GetSystemStationTankDetailDataById(string Tokenbearer, long? TankId)
        {
            StationTankModel tanks = new StationTankModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemStationTankDetailDataById/" + TankId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tanks = JsonConvert.DeserializeObject<StationTankModel>(apiResponse);
                }
            }
            return tanks;
        }
        public async Task<Genericmodel> AddSystemStationTankdata(string Tokenbearer, StationTankModel obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SetupManagement/RegisterSystemStationTank", obj);
            return resp;
        }
        #endregion

        #region System Station Pump
        public async Task<Stationpumps> GetSystemStationPumpDetailDataById(string Tokenbearer, long? PumpId)
        {
            Stationpumps tanks = new Stationpumps();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemStationPumpDetailDataById/" + PumpId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tanks = JsonConvert.DeserializeObject<Stationpumps>(apiResponse);
                }
            }
            return tanks;
        }
        public async Task<Genericmodel> AddSystemStationpumpdata(string Tokenbearer, Stationpumps obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/SetupManagement/RegisterSystemStationPump", obj);
            return resp;
        }
        #endregion

        #region System Station Tank Dips
        public async Task<Genericmodel> Addsystemstationtankdipsdata(string Tokenbearer, StationDailyDip obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationtankdipsdata", obj);
            return resp;
        }
        public async Task<StationDailyDip> GetsystemstationtankdetailbyId(string Tokenbearer, long TankId)
        {
            StationDailyDip data = new StationDailyDip();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/GetsystemstationtankdetailbyId/" + TankId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<StationDailyDip>(apiResponse);
                }
            }
            return data;
        }
        #endregion

        #region System Station Purchases
        public async Task<Genericmodel> Addsystemstationpurchasesdata(string Tokenbearer, StationPurchase obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationpurchasesdata", obj);
            return resp;
        }
        public async Task<StationPurchase> Getsystemstationpurchasesdetailbyid(string Tokenbearer, long? PurchaseId)
        {
            StationPurchase data = new StationPurchase();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationpurchasesdetailbyid/" + PurchaseId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<StationPurchase>(apiResponse);
                }
            }
            return data;
        }
        #endregion

        #region Station Shift

        public async Task<StationShiftDetailData> Getsystemstationshiftdetaildata(string Tokenbearer)
        {
            StationShiftDetailData data = new StationShiftDetailData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftdetaildata"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<StationShiftDetailData>(apiResponse);
                }
            }
            return data;
        }

        public async Task<SingleStationShiftData> Getsystemstationsingleshiftdata(string Tokenbearer, long StationId, long? ShiftId)
        {
            SingleStationShiftData data = new SingleStationShiftData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationsingleshiftdata/" + StationId + "/" + ShiftId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<SingleStationShiftData>(apiResponse);
                }
            }
            return data;
        }

        public async Task<ShiftDetailDataModel> Getsystemstationshiftdetaildata(string Tokenbearer, long ShiftId)
        {
            ShiftDetailDataModel data = new ShiftDetailDataModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftdetaildata/" + ShiftId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftDetailDataModel>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshiftpumpdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftpumpdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshifttankdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshifttankdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshiftlubedata(string Tokenbearer, SingleStationShiftData obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftlubedata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshiftlpgdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftlpgdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshiftsparepartdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftsparepartdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemstationshiftcarwashdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftcarwashdata", obj);
            return resp;
        }
        #region Shift Credit Invoices
        public async Task<ShiftCreditInvoiceData> Getsystemstationshiftcreditinvoicedata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftCreditInvoiceData data = new ShiftCreditInvoiceData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftcreditinvoicedata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftCreditInvoiceData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftcreditinvoicedata(string Tokenbearer, ShiftCreditInvoice obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftcreditinvoicedata", obj);
            return resp;
        }
        #endregion

        #region Shift Wetstock Purchase
        public async Task<ShiftWetStockPurchaseData> Getsystemstationshiftwetstockpurchasedata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftWetStockPurchaseData data = new ShiftWetStockPurchaseData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftwetstockpurchasedata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftWetStockPurchaseData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftwetpurchasedata(string Tokenbearer, ShiftWetStockPurchase obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftwetstockpurchasedata", obj);
            return resp;
        }
        #endregion

        #region Shift Drystock Purchase
        public async Task<ShiftDryStockPurchaseData> Getsystemstationshiftdrystockpurchasedata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftDryStockPurchaseData data = new ShiftDryStockPurchaseData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftdrystockpurchasedata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftDryStockPurchaseData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftdrypurchasedata(string Tokenbearer, ShiftDryStockPurchase obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftdrystockpurchasedata", obj);
            return resp;
        }
        #endregion

        #region Station Shift Expenses 
        public async Task<ShiftExpenseData> Getsystemstationshiftexpensedata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftExpenseData data = new ShiftExpenseData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftexpensedata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftExpenseData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftexpensedata(string Tokenbearer, ShiftExpenses obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftexpensedata", obj);
            return resp;
        }
        #endregion

        #region Shift Mpesa Collection
        public async Task<ShiftMpesaCollectionData> Getsystemstationshiftmpesacollectiondata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftMpesaCollectionData data = new ShiftMpesaCollectionData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftmpesadata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftMpesaCollectionData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftmpesadata(string Tokenbearer, ShiftMpesaCollection obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftmpesadata", obj);
            return resp;
        }
        #endregion

        #region Shift Fuel Card Collections
        public async Task<ShiftFuelCardCollectionData> Getsystemstationshiftfuelcarddata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftFuelCardCollectionData data = new ShiftFuelCardCollectionData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftfuelcarddata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftFuelCardCollectionData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftfuelcarddata(string Tokenbearer, ShiftFuelCardCollection obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftfuelcarddata", obj);
            return resp;
        }
        #endregion

        #region Shift Merchant Collection
        public async Task<ShiftMerchantCollectionData> Getsystemstationshiftmerchantdata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftMerchantCollectionData data = new ShiftMerchantCollectionData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftmerchantdata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftMerchantCollectionData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftmerchantdata(string Tokenbearer, ShiftMerchantCollection obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftmerchantdata", obj);
            return resp;
        }
        #endregion

        #region Shift Topups
        public async Task<ShiftTopupData> Getsystemstationshifttopupdata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftTopupData data = new ShiftTopupData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshifttopupdata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftTopupData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshifttopupdata(string Tokenbearer, ShiftTopup obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshifttopupdata", obj);
            return resp;
        }
        #endregion

        #region Shift Payment 
        public async Task<ShiftPaymentData> Getsystemstationshiftpaymentdata(string Tokenbearer, long ShiftId, int start, int length, string searchParam)
        {
            ShiftPaymentData data = new ShiftPaymentData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftpaymentdata?ShiftId=" + ShiftId + "&start=" + start + "&length=" + length + "&searchParam=" + searchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftPaymentData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftpaymentdata(string Tokenbearer, ShiftPayment obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftpaymentdata", obj);
            return resp;
        }
        #endregion
        public async Task<Genericmodel> Closesystemstationshiftdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Closesystemstationshiftdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Supervisorclosesystemstationshiftdata(string Tokenbearer, SingleStationShiftData obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Supervisorclosesystemstationshiftdata", obj);
            return resp;
        }
        
        public async Task<decimal> Getsystemstationtankshiftpurchasedata(string Tokenbearer, long ShiftId,long TankId)
        {
            decimal data=0;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationtankshiftpurchasedata/" + ShiftId+"/"+TankId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<decimal>(apiResponse);
                }
            }
            return data;
        }
        public async Task<decimal> Getsystemstationdryproductshiftpurchasedata(string Tokenbearer, long ShiftId, long ProductId)
        {
            decimal data = 0;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationdryproductshiftpurchasedata/" + ShiftId + "/" + ProductId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<decimal>(apiResponse);
                }
            }
            return data;
        }
        public async Task<IEnumerable<SystemStationShift>> Getsystemstationshiftlistdata(string Tokenbearer, long StationId)
        {
            IEnumerable<SystemStationShift> data = new List<SystemStationShift>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationshiftlistdata/" + StationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<IEnumerable<SystemStationShift>>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationshiftvoucherdata(string Tokenbearer, ShiftVoucher obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationshiftvoucherdata", obj);
            return resp;
        }
        public async Task<ShiftVoucher> Getsystemstationvoucherdatabyid(string Tokenbearer, long? ShiftVoucherId)
        {
            ShiftVoucher data = new ShiftVoucher();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationvoucherbyid/" + ShiftVoucherId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftVoucher>(apiResponse);
                }
            }
            return data;
        }
        public async Task<ProductPriceData> GetsystemdryproductpricebyId(string Tokenbearer, long ProductVariationId, long StationId,long CustomerId)
        {
            ProductPriceData data = new ProductPriceData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/GetsystemdryproductpricebyId/" + ProductVariationId+"/"+StationId + "/" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ProductPriceData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<ProductVatPriceData> GetsystemproductpricevatbyId(string Tokenbearer, long SupplierId, long ProductId)
        {
            ProductVatPriceData data = new ProductVatPriceData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/GetsystemproductpricevatbyId/" + SupplierId + "/" + ProductId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ProductVatPriceData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<Genericmodel> Addsystemstationlubedata(string Tokenbearer, ShiftLubesandLpg obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationlubedata", obj);
            return resp;
        }
        public async Task<ShiftLubesandLpg> GetSystemStationLubeandLpgById(string Tokenbearer, long? ShiftLubeLpgId)
        {
            ShiftLubesandLpg data = new ShiftLubesandLpg();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationlubeandlpgbyid/" + ShiftLubeLpgId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftLubesandLpg>(apiResponse);
                }
            }
            return data;
        }
        //public async Task<Genericmodel> Addsystemcreditinvoicesaledata(string Tokenbearer, ShiftCreditInvoice obj)
        //{
        //    var resp = await POSTTOAPI(Tokenbearer, "/api/StationManagement/Registersystemstationcreditinvoicedata", obj);
        //    return resp;
        //}
        //public async Task<ShiftCreditInvoice> Getsystemcreditinvoicesaledatabyid(string Tokenbearer, long? ShiftCreditInvoiceId)
        //{
        //    ShiftCreditInvoice data = new ShiftCreditInvoice();
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
        //        using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemcreditinvoicesalebyid/" + ShiftCreditInvoiceId))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            data = JsonConvert.DeserializeObject<ShiftCreditInvoice>(apiResponse);
        //        }
        //    }
        //    return data;
        //}
        #endregion

        #region Station Summary Data
        public async Task<ShiftSummaryDetailData> Getsystemstationsalesummary(string Tokenbearer,DateTime StartDate,DateTime EndDate,long StationId,long ShiftId)
        {
            ShiftSummaryDetailData data = new ShiftSummaryDetailData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationsalesummary?StationId=" + StationId + "&ShiftId=" + ShiftId + "&StartDate=" + StartDate + "&EndTime=" + EndDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ShiftSummaryDetailData>(apiResponse);
                }
            }
            return data;
        }
        public async Task<IEnumerable<StationPurchaseItemModel>> Getsystemstationpurchasessummary(string Tokenbearer, DateTime StartDate, DateTime EndDate, long StationId)
        {
            IEnumerable<StationPurchaseItemModel> data = new List<StationPurchaseItemModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemstationpurchaseummary?StationId=" + StationId + "&StartDate=" + StartDate + "&EndTime=" + EndDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<IEnumerable<StationPurchaseItemModel>>(apiResponse);
                }
            }
            return data;
        }
        public async Task<StationStatementData> Getsystemshiftstatementsummary(string Tokenbearer, DateTime StartDate, DateTime EndDate, long StationId,long ShiftId)
        {
            StationStatementData data = new StationStatementData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/StationManagement/Getsystemshiftstatementsummary?StationId=" + StationId + "&ShiftId=" + ShiftId + "&StartDate=" + StartDate + "&EndTime=" + EndDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<StationStatementData>(apiResponse);
                }
            }
            return data;
        }
        #endregion

        #region Staff And Stations Data
        public static async Task<SystemStaffs> GetTenantStaffData(string Tokenbearer, long Userid)
        {
            SystemStaffs Staff = new SystemStaffs();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Tokenbearer + "/api/StaffManagemet/GetSystemStaffById/" + Userid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Staff = JsonConvert.DeserializeObject<SystemStaffs>(apiResponse);
                }
            }
            return Staff;
        }
        #endregion

        #region System Customer Data
        public async Task<IEnumerable<SystemCustomerModel>> Getsystemtenantcustomers(string Tokenbearer,long TenantId, int Offset,int Count)
        {
            IEnumerable<SystemCustomerModel> Customers = new List<SystemCustomerModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerData/" + TenantId +"/" + Offset +"/" + Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<IEnumerable<SystemCustomerModel>>(apiResponse);
                }
            }
            return Customers;
        }

        public async Task<Genericmodel> Addsystemcorcustomer(string Tokenbearer, CompanyCustomer obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerData", obj);
            return resp;
        }

        public async Task<CompanyCustomer> GetSystemCustomerData(string Tokenbearer, long? CustomerId)
        {
            CompanyCustomer Customer = new CompanyCustomer();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerData/" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<CompanyCustomer>(apiResponse);
                }
            }
            return Customer;
        }

        public async Task<SystemCustomerDetails> GetSystemCustomerDetailData(string Tokenbearer, long? CustomerId)
        {
            SystemCustomerDetails Customer = new SystemCustomerDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerDetailData/" + CustomerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<SystemCustomerDetails>(apiResponse);
                }
            }
            return Customer;
        }
        public async Task<PriceListData> GetSystemCustomerAgreementPriceListData(string Tokenbearer, long? PriceListId)
        {
            PriceListData Customer = new PriceListData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/GetSystemCustomerAgreementPriceListData/" + PriceListId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<PriceListData>(apiResponse);
                }
            }
            return Customer;
        }
        public async Task<DiscountListData> GetSystemCustomerAgreementDiscountListData(string Tokenbearer, long? DiscountListId)
        {
            DiscountListData Customer = new DiscountListData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/GetSystemCustomerAgreementDiscountListData/" + DiscountListId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<DiscountListData>(apiResponse);
                }
            }
            return Customer;
        }
        #endregion

        #region Customer Agreement Data
        public async Task<Genericmodel> Addsystemcustomerprepaidagreement(string Tokenbearer, CustomerPrepaidAgreement obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerPrepaidAgreementData", obj);
            return resp;
        }
        public async Task<CustomerPrepaidAgreement> Getcustomerprepaidagreementdatabyid(string Tokenbearer, long Id)
        {
            CustomerPrepaidAgreement Data = new CustomerPrepaidAgreement();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomerprepaidagreementdatabyid/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerPrepaidAgreement>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addsystemcustomerpostpaidrecurrentagreementdata(string Tokenbearer, PostpaidRecurentAgreement obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerPostpaidRecurrentAgreementData", obj);
            return resp;
        }

        public async Task<Genericmodel> Addsystemcustomerpostpaidoneoffagreementdata(string Tokenbearer, PostpaidOneOffAgreement obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerPostpaidOneoffAgreementData", obj);
            return resp;
        }
        public async Task<Genericmodel> Addsystemcustomerpostpaidcreditagreementdata(string Tokenbearer, CustomerCreditAgreement obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerPostpaidCreditAgreementData", obj);
            return resp;
        }
        public async Task<CustomerCreditAgreement> Getcustomerpostpaidcreditagreementdatabyid(string Tokenbearer, long Id)
        {
            CustomerCreditAgreement Data = new CustomerCreditAgreement();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomerpostpaidcreditagreementdatabyid/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerCreditAgreement>(apiResponse);
                }
            }
            return Data;
        }


        public async Task<IEnumerable<CustomerAccountTopups>> GetSystemCustomerAgreementTopupTransferListData(string Tokenbearer, long Id)
        {
            IEnumerable<CustomerAccountTopups> Data = new List<CustomerAccountTopups>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAgreementtopuptransferData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<CustomerAccountTopups>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<IEnumerable<CustomerAgreementPayments>> GetSystemCustomerAgreementPaymentListData(string Tokenbearer, long Id)
        {
            IEnumerable<CustomerAgreementPayments> Data = new List<CustomerAgreementPayments>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAgreementPaymentListData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<CustomerAgreementPayments>>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Agreement Account Data
        public async Task<Genericmodel> AddcustomeragreementAccount(string Tokenbearer, CustomerAgreementAccountData obj)
        {
            var dara =JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountData", obj);
            return resp;
        }
        public async Task<Genericmodel> AddcustomeragreementAccounttopupdata(string Tokenbearer, CustomerAccountTopup obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountTopupData", obj);
            return resp;
        }
        public async Task<Genericmodel> AddcustomeragreementAccountTransferdata(string Tokenbearer, CustomerAccountTransfer obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountTransferData", obj);
            return resp;
        }
        public async Task<Genericmodel> Addcustomeragreementpaymentdata(string Tokenbearer, CustomerAgreementPayment obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementPaymentData", obj);
            return resp;
        }

        public async Task<Genericmodel> PostReversetopupData(string Tokenbearer, ReverseSaleRequestData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/PostReverseTopupTransactionData", obj);
            return resp;
        }
        public async Task<Genericmodel> PostReversepaymentData(string Tokenbearer, ReverseSaleRequestData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/PostReversePaymentTransactionData", obj);
            return resp;
        }

        public async Task<SystemAccountDetailData> GetCustomerAgreementAccountsData(string Tokenbearer, long Id)
        {
            SystemAccountDetailData Data = new SystemAccountDetailData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountDetailData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemAccountDetailData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<SystemAccountDetailData> GetCustomerAccountDetailData(string Tokenbearer, long Id)
        {
            SystemAccountDetailData Data = new SystemAccountDetailData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountDetailData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemAccountDetailData>(apiResponse);
                }
            }
            return Data;
        }

        #endregion

        #region Replace Customer Account Card
        public async Task<Genericmodel> Replacecustomeraccountcarddata(string Tokenbearer, AccountCardReplaceDetails obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/Replacecustomeraccountcarddata", obj);
            return resp;
        }
        #endregion

        #region Customer account Policies
        public async Task<CustomerAccountDetails> GetSystemCustomerAccountPolicyDetailData(string Tokenbearer, long Id)
        {
            CustomerAccountDetails Data = new CustomerAccountDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountPolicyDetailData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerAccountDetails>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountProductPolicypData(string Tokenbearer, AccountProductpolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountProductPolicyData", obj);
            return resp;
        }
        public async Task<AccountProductpolicy> GetcustomeraccountproductpolicyData(string Tokenbearer, long? Id)
        {
            AccountProductpolicy Data = new AccountProductpolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetcustomeraccountproductpolicyData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountProductpolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountStationPolicypData(string Tokenbearer, AccountStationspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountStationPolicyData", obj);
            return resp;
        }
        public async Task<AccountStationspolicy> Getcustomeraccountstationpolicydata(string Tokenbearer, long? Id)
        {
            AccountStationspolicy Data = new AccountStationspolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountstationpolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountStationspolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountWeekdayPolicypData(string Tokenbearer, AccountWeekDayspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountWeekdayPolicyData", obj);
            return resp;
        }
        public async Task<AccountWeekDayspolicy> Getcustomeraccountweekdaypolicydata(string Tokenbearer, long? Id)
        {
            AccountWeekDayspolicy Data = new AccountWeekDayspolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountweekdaypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountWeekDayspolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountFrequencyPolicypData(string Tokenbearer, AccountTransactionFrequencypolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountFrequencyPolicyData", obj);
            return resp;
        }
        public async Task<AccountTransactionFrequencypolicy> Getcustomeraccountfrequencypolicydata(string Tokenbearer, long? Id)
        {
            AccountTransactionFrequencypolicy Data = new AccountTransactionFrequencypolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountfrequencypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountTransactionFrequencypolicy>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region System Employee Data
        public async Task<Genericmodel> AddCustomerAccountEmployeedata(string Tokenbearer, CustomerAccountEmployees obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAccountEmployeeData", obj);
            return resp;
        }
        public async Task<CustomerAccountEmployees> GetCustomerAccountEmployeeById(string Tokenbearer, long? Id)
        {
            CustomerAccountEmployees Data = new CustomerAccountEmployees();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetCustomerAccountEmployeeById/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerAccountEmployees>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<CustomerAccountEmployeePolicyDetails> GetCustomerAccountEmployeePoliciesdata(string Tokenbearer, long Id)
        {
            CustomerAccountEmployeePolicyDetails Data = new CustomerAccountEmployeePolicyDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountEmployeePolicyDetailData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerAccountEmployeePolicyDetails>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEployeeProductPolicypData(string Tokenbearer, AccountEmployeeProductpolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEmployeeProductPolicyData", obj);
            return resp;
        }
        public async Task<AccountEmployeeProductpolicy> Getcustomeraccountemployeeproductpolicydata(string Tokenbearer, long? Id)
        {
            AccountEmployeeProductpolicy Data = new AccountEmployeeProductpolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountemployeeproductpolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEmployeeProductpolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEmployeeStationPolicypData(string Tokenbearer, AccountEmployeeStationspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEmployeeStationPolicyData", obj);
            return resp;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEmployeeWeekdayPolicypData(string Tokenbearer, AccountEmployeeWeekDayspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEmployeeWeekdayPolicyData", obj);
            return resp;
        }
        public async Task<AccountEmployeeWeekDayspolicy> Getcustomeraccountemployeeweekdaypolicydata(string Tokenbearer, long? Id)
        {
            AccountEmployeeWeekDayspolicy Data = new AccountEmployeeWeekDayspolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountemployeeweekdaypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEmployeeWeekDayspolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEmployeeFrequencyPolicypData(string Tokenbearer, AccountEmployeeTransactionFrequencypolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEmployeeFrequencyPolicyData", obj);
            return resp;
        }
        public async Task<AccountEmployeeTransactionFrequencypolicy> Getcustomeraccountemployeefrequencypolicydata(string Tokenbearer, long? Id)
        {
            AccountEmployeeTransactionFrequencypolicy Data = new AccountEmployeeTransactionFrequencypolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountemployeefrequencypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEmployeeTransactionFrequencypolicy>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region System Equipment Data
        public async Task<Genericmodel> AddCustomerAccountEquipmentdata(string Tokenbearer, CustomerAccountEquipments obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAccountEquipmentData", obj);
            return resp;
        }
        public async Task<CustomerAccountEquipments> GetCustomerAccountEquipmentdata(string Tokenbearer, long? EquipmentId)
        {
            CustomerAccountEquipments Data = new CustomerAccountEquipments();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountEquipmentData/" + EquipmentId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerAccountEquipments>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<CustomerAccountEquipmentPolicyDetails> GetCustomerAccountEquipmentPoliciesdata(string Tokenbearer, long Id)
        {
            CustomerAccountEquipmentPolicyDetails Data = new CustomerAccountEquipmentPolicyDetails();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/GetSystemCustomerAccountEquipmentPolicyDetailData/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<CustomerAccountEquipmentPolicyDetails>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEquipmentProductPolicyData(string Tokenbearer, AccountEquipmentProductpolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEquipmentProductPolicyData", obj);
            return resp;
        }
        public async Task<AccountEquipmentProductpolicy> Getcustomeraccountequipmentproductpolicydata(string Tokenbearer, long? Id)
        {
            AccountEquipmentProductpolicy Data = new AccountEquipmentProductpolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountequipmentproductpolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEquipmentProductpolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEquipmentStationPolicyData(string Tokenbearer, AccountEquipmentStationspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEquipmentStationPolicyData", obj);
            return resp;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEquipmentWeekdayPolicyData(string Tokenbearer, AccountEquipmentWeekDayspolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEquipmentWeekdayPolicyData", obj);
            return resp;
        }
        public async Task<AccountEquipmentWeekDayspolicy> Getcustomeraccountequipmentweekdaypolicydata(string Tokenbearer, long? Id)
        {
            AccountEquipmentWeekDayspolicy Data = new AccountEquipmentWeekDayspolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountequipmentweekdaypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEquipmentWeekDayspolicy>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> RegisterCustomerAgreementAccountEquipmentFrequencyPolicyData(string Tokenbearer, AccountEquipmentTransactionFrequencypolicy obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RegisterCustomerAgreementAccountEquipmentFrequencyPolicyData", obj);
            return resp;
        }
        public async Task<AccountEquipmentTransactionFrequencypolicy> Getcustomeraccountequipmentfrequencypolicydata(string Tokenbearer, long? Id)
        {
            AccountEquipmentTransactionFrequencypolicy Data = new AccountEquipmentTransactionFrequencypolicy();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/CustomerManagement/Getcustomeraccountequipmentfrequencypolicydata/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<AccountEquipmentTransactionFrequencypolicy>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Customer Loyalty Data
        public async Task<LoyaltyFormulaandFormulaRules> GetSystemLoyaltyFormulaandFormulaRulesData(string Tokenbearer,long TenantId)
        {
            LoyaltyFormulaandFormulaRules Data = new LoyaltyFormulaandFormulaRules();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltyFormulaandFormulaRulesData/"+TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<LoyaltyFormulaandFormulaRules>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addstemawardingformuladata(string Tokenbearer, LoyaltyFormulaandRules obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/Registerformulaandrules", obj);
            return resp;
        }
        public async Task<SystemFormulaData> Getsystemformulabyid(string Tokenbearer,long FormulaId)
        {
            SystemFormulaData Data = new SystemFormulaData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltyFormulaDataById/"+FormulaId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemFormulaData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystemformuladatadata(string Tokenbearer, SystemFormulaData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/Registerformulaeditdata", obj);
            return resp;
        }
        public async Task<SystemFormulaRuleData> Getsystemformularuledatabyid(string Tokenbearer, long FormulaRuleId)
        {
            SystemFormulaRuleData Data = new SystemFormulaRuleData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltyFormularuleDataById/"+FormulaRuleId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemFormulaRuleData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystemformularuleeditdata(string Tokenbearer, SystemFormulaRuleData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/RegisterformulaRuleeditdata", obj);
            return resp;
        }
        public async Task<LoyaltySchemeandSchemeRules> GetSystemLoyaltySchemeandSchemeRulesData(string Tokenbearer,long TenantId)
        {
            LoyaltySchemeandSchemeRules Data = new LoyaltySchemeandSchemeRules();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltySchemeandSchemeRulesData/"+ TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<LoyaltySchemeandSchemeRules>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addstemawardingmschemedata(string Tokenbearer, LoyaltySchemesandRules obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/Registerschemeandrules", obj);
            return resp;
        }

        public async Task<SystemLoyaltyScheme> Getsystemschemedatabyid(string Tokenbearer, long LSchemeId)
        {
            SystemLoyaltyScheme Data = new SystemLoyaltyScheme();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltyschemeDataById/"+LSchemeId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemLoyaltyScheme>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystemschemedata(string Tokenbearer, SystemLoyaltyScheme obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/Registerschemeeditdata", obj);
            return resp;
        }
        public async Task<SystemSchemeRuleResultData> Getsystemschemeruledatabyid(string Tokenbearer, long LSchemeRuleId)
        {
            SystemSchemeRuleResultData Data = new SystemSchemeRuleResultData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/LoyaltyManagement/GetSystemLoyaltyschemeRuleDataById/"+LSchemeRuleId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemSchemeRuleResultData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystemschemeruledata(string Tokenbearer, SchemeRule obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/LoyaltyManagement/Registerschemeruleeditdata", obj);
            return resp;
        }
        #endregion

        #region System Products Data
        public async Task<IEnumerable<SystemProductModelData>> Getsystemproductvariationdata(string Tokenbearer,long TenantId)
        {
            IEnumerable<SystemProductModelData> Data = new List<SystemProductModelData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/ProductManagement/GetSystemProductvariationData/"+ TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemProductModelData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<IEnumerable<SystemProductModelData>> GetSystemStationProductDataById(string Tokenbearer,long TenantId, long Id)
        {
            IEnumerable<SystemProductModelData> Data = new List<SystemProductModelData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/ProductManagement/GetSystemStationProductData?TenantId="+ TenantId + "&StationId="+ Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemProductModelData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addsystemproductdata(string Tokenbearer, SystemProducts obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/ProductManagement/RegisterSystemProduct", obj);
            return resp;
        }
        public async Task<SystemProductVariation> GetSystemProductDataById(string Tokenbearer, long? Id)
        {
            SystemProductVariation Data = new SystemProductVariation();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/ProductManagement/GetSystemProductDetailDataById/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemProductVariation>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Updatesystemproductdata(string Tokenbearer, SystemProductVariation obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/ProductManagement/UpdateSystemProduct", obj);
            return resp;
        }
        #endregion

        #region Main Store Listing
        public async Task<IEnumerable<DryStockMainStoreModelData>> Getsystemproductmainstoredata(string Tokenbearer, long TenantId, long StationId)
        {
            IEnumerable<DryStockMainStoreModelData> Data = new List<DryStockMainStoreModelData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/ProductManagement/Getsystemproductmainstoredata?TenantId=" + TenantId + "&StationId=" + StationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<DryStockMainStoreModelData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Savetransfertoaccessoriesdata(string Tokenbearer, DryStockMovement obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/ProductManagement/Savetransfertoaccessoriesdata", obj);
            return resp;
        }
        #endregion

        #region System POS List Data
        public async Task<IEnumerable<SystemGadgetsData>> GetSystemGadgetsData(string Tokenbearer, int Offset, int Count)
        {
            IEnumerable<SystemGadgetsData> Data = new List<SystemGadgetsData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/HardwareManagement/GetSystemGadgetsData/" + Offset + "/" + Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemGadgetsData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> AddTenantGadgetdata(string Tokenbearer, Systemgadgets obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/HardwareManagement/RegisterSystemGadgets", obj);
            return resp;
        }

        public async Task<Systemgadgets> GetSystemGadgetsDataById(string Tokenbearer, long? GadgetId)
        {
            Systemgadgets Data = new Systemgadgets();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/HardwareManagement/GetSystemGadgetsDataById/"+GadgetId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<Systemgadgets>(apiResponse);
                }
            }
            return Data;
        }

        #endregion

        #region System Cards List Data
        public async Task<IEnumerable<SystemTenantsCardData>> GetSystemCardsData(string Tokenbearer,long TenantId, int Offset, int Count)
        {
            IEnumerable<SystemTenantsCardData> Data = new List<SystemTenantsCardData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemTenantCardData/" + TenantId + "/" + Offset+"/"+ Count))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<SystemTenantsCardData>>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> AddTenantCarddata(string Tokenbearer, SystemTenantCard obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SetupManagement/RegisterSystemTenantCards", obj);
            return resp;
        }

        public async Task<SystemTenantCard> GetSystemTenantCardDataById(string Tokenbearer,long? CardId)
        {
            SystemTenantCard Data = new SystemTenantCard();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemTenantCardDataById/"+CardId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemTenantCard>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Resendcardpindata(string Tokenbearer, long? Tenantcardid)
        {
            Genericmodel model = new Genericmodel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SetupManagement/GetSystemTenantCardById/" + Tenantcardid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Genericmodel>(apiResponse);
                }
            }
            return model;
        }
        #endregion

        #region System Price List Data
        public async Task<SystemPriceListData> GetSystemPriceListData(string Tokenbearer,long TenantId)
        {
            SystemPriceListData Data = new SystemPriceListData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/GetSystemPriceListData/"+TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemPriceListData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Addpricelistdata(string Tokenbearer, StationPriceLists obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Registerpricelistdata", obj);
            return resp;
        }
        public async Task<PriceListInfoData> GetSystemPriceListDataById(string Tokenbearer, long? PricelistId)
        {
            PriceListInfoData Data = new PriceListInfoData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/Getsystempricelistdatabyid/" + PricelistId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<PriceListInfoData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystempricelistdata(string Tokenbearer, PriceListInfoData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Editsystempricelistdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Addpricelistpricenewdata(string Tokenbearer, PriceListPriceDataModel obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Addpricelistpricenewdata", obj);
            return resp;
        }
        #endregion

        #region System Discount List Data
        public async Task<SystemDiscountListData> GetSystemDiscountListData(string Tokenbearer,long TenantId)
        {
            SystemDiscountListData Data = new SystemDiscountListData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/GetSystemDiscountListData/"+ TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemDiscountListData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Adddiscountlistdata(string Tokenbearer, StationDiscountLists obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Registerdiscountlistdata", obj);
            return resp;
        }

        public async Task<DiscountListModelData> GetSystemDiscountListDataById(string Tokenbearer, long? DiscountlistId)
        {
            DiscountListModelData Data = new DiscountListModelData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/Getsystemdiscountlistdatabyid/" + DiscountlistId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<DiscountListModelData>(apiResponse);
                }
            }
            return Data;
        }
        public async Task<Genericmodel> Editsystemdiscountlistdata(string Tokenbearer, DiscountListModelData obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Editsystemdiscountlistdata", obj);
            return resp;
        }
        public async Task<Genericmodel> Adddicountlistvaluenewdata(string Tokenbearer, LnkDiscountProductModel obj)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/PriceManagement/Adddicountlistvaluenewdata", obj);
            return resp;
        }
        public async Task<IEnumerable<DiscountlistDiscountData>> Getdiscountlistdiscountsdata(string Tokenbearer, long DiscountlistId)
        {
            IEnumerable<DiscountlistDiscountData> Data = new List<DiscountlistDiscountData>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/PriceManagement/Getdiscountlistdiscountdata/" + DiscountlistId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<DiscountlistDiscountData>>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Post Offline Sale
        public async Task<CustomerCardDetailsData> GetCustomerCardDetails(string Tokenbearer, PostcardDetails obj)
        {
            var resp = await POSTTOAPICARDDETAIL(Tokenbearer, "/api/CustomerManagement/GetSystemCustomerAccountCardDetailData", obj);
            return resp;
        }
        public async Task<SingleFinanceTransactions> PostSaleTransactionData(string Tokenbearer, SalesTransactionRequest obj)
        {
            var resp = await SALEPOSTTOAPI(Tokenbearer, "/api/SaleTransaction/PostSaleTransactionData", obj);
            return resp;
        }
        public async Task<Genericmodel> PostReverseSaleTransactionData(string Tokenbearer, ReverseSaleRequestData obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var resp = await POSTTOAPI(Tokenbearer, "/api/SaleTransaction/PostReverseSaleTransactionData", obj);
            return resp;
        }
        public async Task<IEnumerable<FinanceTransactions>> Getallofflinesalesdata(string Tokenbearer,long TenantId)
        {
            IEnumerable<FinanceTransactions> Data = new List<FinanceTransactions>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SaleTransaction/Getallofflinesalesdata/"+ TenantId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<IEnumerable<FinanceTransactions>>(apiResponse);
                }
            }
            return Data;
        }
        #endregion

        #region Delete,Deactivate Actions
        public async Task<Genericmodel> DeactivateorDeleteTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/DeactivateorDeleteTableColumnData", model);
            return resp;
        }
        public async Task<Genericmodel> RemoveTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/RemoveTableColumnData", model);
            return resp;
        } 
        public async Task<Genericmodel> DefaultThisTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/CustomerManagement/DefaultThisTableColumnData", model);
            return resp;
        }
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
        public async Task<List<ListModel>> UnauthGetSystemDropDownData(ListModelType listType)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemDropdown/UnauthSystemdropdowns?listType=" + (int)listType))
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
        public async Task<List<ListModel>> GetSystemDropDownDataByIdAndSearch(string Tokenbearer, ListModelType listType, long Id,string SearchParam)
        {
            List<ListModel> list = new List<ListModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/SystemDropdown/Systemdropdownbyidandsearch?listType=" + (int)listType + "&Id=" + Id + "&SearchParam=" + SearchParam))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<ListModel>>(apiResponse);
                }
            }
            return list;
        }
        public async Task<List<ListModel>> GetSystemDropDownDataById(string Tokenbearer,long TenantId, ListModelType listType, long Id)
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

        #region Tenant Delete,Deactivate Actions
        public async Task<Genericmodel> DeactivateorDeleteTenantTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SystemTenantDropdown/DeactivateorDeleteTableColumnData", model);
            return resp;
        }
        public async Task<Genericmodel> RemoveTenantTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SystemTenantDropdown/RemoveTableColumnData", model);
            return resp;
        }
        public async Task<Genericmodel> DefaultThisTenantTableColumnData(string Tokenbearer, ActivateDeactivateActions model)
        {
            var resp = await POSTTOAPI(Tokenbearer, "/api/SystemTenantDropdown/DefaultThisTableColumnData", model);
            return resp;
        }
        #endregion

        #region Other Methods
        public async Task<Fuelproapienpointmodel> GETDATAFROMAPI(string Tokenbearer, string uri)
        {
            Fuelproapienpointmodel list = new Fuelproapienpointmodel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<Fuelproapienpointmodel>(apiResponse);
                }
            }
            return list;
        }
        public async Task<UsermodelResponce> AUTHPOSTTOAPILOGIN(string endpoint, dynamic obj)
        {
            UsermodelResponce resp = new UsermodelResponce();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<UsermodelResponce>(outPut);
                }
            }
            return resp;
        }
        public async Task<CustomerCardDetailsData> POSTTOAPICARDDETAIL(string Tokenbearer, string endpoint, dynamic obj)
        {
            CustomerCardDetailsData resp = new CustomerCardDetailsData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<CustomerCardDetailsData>(outPut);
                }
            }
            return resp;
        }
        public async Task<UsermodelResponce> POSTTOAPILOGIN(string endpoint, dynamic obj)
        {
            UsermodelResponce resp = new UsermodelResponce();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<UsermodelResponce>(outPut);
                }
            }
            return resp;
        }
        public async Task<Genericmodel> POSTTOAPI(string Tokenbearer, string endpoint, dynamic obj)
        {
            Genericmodel resp = new Genericmodel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Genericmodel>(outPut);
                }
            }
            return resp;
        }
        public async Task<SingleFinanceTransactions> SALEPOSTTOAPI(string Tokenbearer, string endpoint, dynamic obj)
        {
            SingleFinanceTransactions resp = new SingleFinanceTransactions();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<SingleFinanceTransactions>(outPut);
                }
            }
            return resp;
        }
        public async Task<Genericmodel> UNAUTHPOSTTOAPI(string endpoint, dynamic obj)
        {
            Genericmodel resp = new Genericmodel();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(BaseUrl + endpoint, content))
                {
                    string outPut = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Genericmodel>(outPut);
                }
            }
            return resp;
        }
        #endregion
    }
}
