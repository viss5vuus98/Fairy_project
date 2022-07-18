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
            return View(_context.exhibitions);
        }

        public IActionResult MasterDetail(int exhibitId)
        {
            return View();
        }

        public IActionResult CreatExhibition(int idnew)
        {
            ViewBag.idnew = idnew;
            return View();
        }

        [HttpPost]
        public ActionResult CreatExhibition(CreatExhibitionViewModel model)
        {
            Exhibition exhibition = new Exhibition();
            Booths booths = new Booths();
            //exhibition.exhibitName = model.exhibitName;
            Console.Write(model.exhibitName + "22222222222222222222222");
            exhibition.exhibitStatus = 1;
            exhibition.datefrom = model.datefrom;
            exhibition.dateto = model.dateto;
            //exhibition.ex_description = model.ex_description;
            exhibition.ex_personTime = model.ex_personTime;
            exhibition.ex_totalImcome = model.ex_totalImcome;
            //exhibition.ticket_Peice = model.ticket_Peice;
            _context.exhibitions.Add(exhibition);
            //for (int i=0;i<model.setboothslist.Count;i++)
            //{
            //    for (int j = 0; j < model.setboothslist[i].boothsum;j++)
            //    {

            //    }
            //}
            _context.SaveChanges();
            return View();
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
