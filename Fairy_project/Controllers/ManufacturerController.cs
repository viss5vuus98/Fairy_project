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
        public async Task<IActionResult> Index(GetBoothsViewModel model)
        {
            Exhibition exhibition =new Exhibition();
            Booths booths  = new Booths();
            
            return View(/*await model.*/);
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
