using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;
using Fairy_project.ViewModels;

namespace Fairy_project.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ServerContext _context;

        public ManufacturerController(ServerContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin,Manufacturer")]
        [HttpGet]
        public ActionResult showName()
        {
            GetShowViewModels m =new GetShowViewModels();
            var a = _context.boothMaps.FirstOrDefault(m => m.boothLv == 1);
            //m.bList = _context.boothMaps.FirstOrDefault(m=>m.boothLv==1);
            //m.elist = _context.exhibitions;

            return Json(a);
        }
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
