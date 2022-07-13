using Fairy_project.Models;
using Fairy_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Fairy_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ServerContext _context;

        public AdminController(ServerContext context)
        {
            _context = context;
        }
        // GET: AdminController
        public IActionResult Master()
        {
            List<MasterViewModels> modellist = new List<MasterViewModels>();
            foreach (var item in _context.exhibitions)
            {
                MasterViewModels model = new MasterViewModels();

                //model.exhibitId = item.exhibitId;
                //model.exhibitName = item.exhibitName;
                //model.exhibit_Pre_img = item.exhibit_Pre_img;
                //model.exhibitStatus = item.exhibitStatus;
                //modellist.Add(model);
            }

            //foreach (var item in modellist)
            //{
            //    item.soldbooth = _context.booths.Where(b => b.e_Id == item.exhibitId).Count();
            //    item.soldticket = _context.booths.Where(t => t.e_Id == item.exhibitId).Count();
            //    item.enteredpeople = 3;
            //}
            return View(modellist);
        }

        public async Task<IActionResult> MasterPreview(int exhibitId)
        {
            int tickets = await _context.tickets.CountAsync(t => t.e_Id == exhibitId);
            if (tickets != 0) 
            {
                ViewBag.ticketscount = tickets;
            }
            ViewBag.test = "test";
            return View("_MasterPreviewPartial");
        }




















        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
