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
            exhibition.exhibitName = model.exhibitName;
            exhibition.exhibitStatus = 1;
            exhibition.datefrom = model.datefrom;
            exhibition.dateto = model.dateto;
            exhibition.ex_Description = model.ex_description;
            exhibition.ex_personTime = model.ex_personTime;
            exhibition.ex_totalImcome = model.ex_totalImcome;
            exhibition.ticket_Price = model.ticket_Peice;
            _context.exhibitions.Add(exhibition);
            int boothnumber = 1;
            for (int i = 0; i < model.setboothslist.Count; i++)
            {
                for (int j = 0; j < model.setboothslist[i].boothsum; j++)
                {
                    Booths booths = new Booths();
                    booths.boothNumber = boothnumber;
                    booths.boothState = 0;
                    if (model.setboothslist[i].boothLv == "大型")
                    {
                        booths.boothLv = 3;
                    }
                    else if (model.setboothslist[i].boothLv == "中型")
                    {
                        booths.boothLv = 2;
                    }
                    else if (model.setboothslist[i].boothLv == "小型")
                    {
                        booths.boothLv = 1;
                    }
                    booths.boothPrice = model.setboothslist[i].boothPrice;
                    _context.boothMaps.Add(booths);
                    boothnumber++;
                }
            }
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
