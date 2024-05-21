
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Mvc;

namespace Everestfcsweb.Controllers
{
    public class GeneralManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public IActionResult Index()
        {
            return View();
        }
    }
}
