
using Everestfcsweb.Models;
using Everestfcsweb.Models.Dashboards;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : BaseController
    {
        private readonly Dashboardservices bl;
        public HomeController(IConfiguration config)
        {
            bl = new Dashboardservices(config);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<JsonResult> Getsystemdashboarddata(string date)
        {
            SystemDashboardData Data = new SystemDashboardData();
            if (SessionUserData.Usermodel.Stations.Count()>1)
            {
                Data = await bl.Getsystemdashboarddata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid,0, date);
            }
            else
            {
                Data = await bl.Getsystemdashboarddata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid,SessionUserData.Usermodel.Stations.FirstOrDefault().StationId, date);
            }
            return Json(Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}