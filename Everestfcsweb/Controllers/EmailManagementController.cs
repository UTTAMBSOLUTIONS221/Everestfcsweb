
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class EmailManagementController : Controller
    {
        private readonly Fuelprodataservices bl;
        public IActionResult Index()
        {
            return View();
        }
    }
}
