using Everestfcsweb.Models.Dashboards;
using Newtonsoft.Json;

namespace Everestfcsweb.Services
{
    public class Dashboardservices
    {
        string BaseUrl = "";
        public Dashboardservices(IConfiguration config)
        {
            BaseUrl = Util.Currenttenantbaseurlstring(config);
        }
        #region System Dashboard Top Summary Data
        public async Task<SystemDashboardData> Getsystemdashboarddata(string Tokenbearer,long TenantId,long StationId,string TodayDate)
        {
            SystemDashboardData Data = new SystemDashboardData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Tokenbearer);
                using (var response = await httpClient.GetAsync(BaseUrl + "/api/DashboardManagement/Getsystemdashboarddata/"+ TenantId + "/"+ StationId + "/" + TodayDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<SystemDashboardData>(apiResponse);
                }
            }
            return Data;
        }
        #endregion
    }
}
