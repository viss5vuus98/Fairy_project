using Fairy_project.Models;
using Fairy_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Drawing;
using System.IO;


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

        public IActionResult ExhibitIdDetail(int exhibitId)
        {
            ExhibitIdDetail_1_ model = new ExhibitIdDetail_1_();
            Exhibition exhibition = (Exhibition)_context.exhibitions.Where(e => e.exhibitId == exhibitId);
            List<Booths> booths = _context.boothMaps.Where(b => b.e_Id == exhibitId).ToList();

            model.exhibitId = exhibition.exhibitId;
            model.exhibitName = exhibition.exhibitName;
            model.exhibitStatus = exhibition.exhibitStatus;
            model.exhibit_P_img = exhibition.exhibit_P_img;
            model.exhibit_T_img = exhibition.exhibit_T_img;
            model.datefrom = exhibition.datefrom;
            model.dateto = exhibition.dateto;
            model.ex_description = exhibition.ex_Description;
            model.ex_personTime = exhibition.ex_personTime;
            model.ex_totalImcome = exhibition.ex_totalImcome;
            model.ticket_Peice = exhibition.ticket_Price;
            for(int i =0;i< booths.Count;i++)
            {
            }

            return View();
        }

        [Route("/Admin/{action}/{idnew}")]
        public IActionResult CreatExhibition(int idnew)
        {
            ViewBag.idnew = idnew;
            return View();
        }

        [Route("/Admin/{action}/{idnew}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatExhibition(CreatExhibitionViewModel model)
        {
            string img_dir = @$"wwwroot/images/";
            Random myRand = new Random();
            Exhibition exhibition = new Exhibition();
            exhibition.exhibitName = model.exhibitName;
            exhibition.exhibitStatus = 1;
            exhibition.datefrom = model.datefrom;
            exhibition.dateto = model.dateto;
            exhibition.ex_Description = model.ex_description;
            exhibition.ex_personTime = model.ex_personTime;
            exhibition.ex_totalImcome = model.ex_totalImcome;
            exhibition.ticket_Price = model.ticket_Peice;

            string Pimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_P_img.FileName);
            exhibition.exhibit_P_img = Pimgname;
            using (var stream = System.IO.File.Create(img_dir + Pimgname))
            {
                await model.exhibit_P_img.CopyToAsync(stream);
            }

            string Timgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_T_img.FileName);
            exhibition.exhibit_T_img = Timgname;
            using (var stream = System.IO.File.Create(img_dir + Timgname))
            {
                await model.exhibit_T_img.CopyToAsync(stream);
            }

            string Preimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_Pre_img.FileName);
            exhibition.exhibit_Pre_img = Preimgname;
            using (var stream = System.IO.File.Create(img_dir + Preimgname))
            {
                await model.exhibit_Pre_img.CopyToAsync(stream);
            }
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
                    booths.e_Id = model.exhibitId;
                    _context.boothMaps.Add(booths);
                    boothnumber++;
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
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
