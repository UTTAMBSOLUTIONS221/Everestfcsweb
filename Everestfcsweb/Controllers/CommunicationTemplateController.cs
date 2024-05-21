using Everestfcsweb.Entities;
using Everestfcsweb.Enum;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    public class CommunicationTemplateController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public CommunicationTemplateController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await bl.Getsystemcommunicationtemplate(SessionUserData.Token);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Addcommunicationtemplate(long? Templateid)
        {
            Communicationtemplate data = new Communicationtemplate();
            if (Templateid > 0)
            {
                data = await bl.Getsystemcommunicationtemplatedatabyid(SessionUserData.Token, Templateid);
            }
            return PartialView(data);
        }
        public async Task<JsonResult> Addcommunicationtemplatedata(Communicationtemplate model)
        {
            var resp = await bl.Addcommunicationtemplatedata(SessionUserData.Token, model);
            return Json(resp);
        }
    }
}
