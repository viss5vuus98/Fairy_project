using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fairy_project.Controllers
{
    public class ManufacturerController : Controller
    {
        //[Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Policy()
        {
            return View();
        }

        public IActionResult ApplyStand()
        {
            return View();
        }
        public IActionResult StandProcess()
        {
            return View();
        }




    }
}
