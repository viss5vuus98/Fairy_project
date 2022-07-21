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
            foreach (var exhibition in _context.exhibitions)
            {
                if (exhibition.exhibitStatus == 2 && DateTime.Compare(DateTime.Now, (DateTime)exhibition.datefrom) == 1)
                {
                    exhibition.exhibitStatus = 3;
                    _context.exhibitions.Update(exhibition);
                }
                else if (exhibition.exhibitStatus == 3 && DateTime.Compare(DateTime.Now, (DateTime)exhibition.dateto) == 1)
                {
                    exhibition.exhibitStatus = 4;
                    _context.exhibitions.Update(exhibition);
                }
            }
            _context.SaveChanges();

            var q = from e in _context.exhibitions where e.exhibitStatus != 4 orderby e.exhibitStatus select e;
            List<Exhibition> el = new List<Exhibition>();
            el = q.ToList();
            el.Reverse();
            return View(el);
        }

        [Route("/Admin/{action}/{exhibitId}")]
        public async Task<IActionResult> ExhibitIdDetail(int exhibitId)
        {
            ExhibitIdDetail_1_ model = new ExhibitIdDetail_1_();
            model.setboothslist = new List<CreatBoothsViewModel>();
            var e = _context.exhibitions.Where(e => e.exhibitId == exhibitId);
            Exhibition exhibition = e.FirstOrDefault();
            List<Booths> booths3 = _context.boothMaps.Where(b => b.e_Id == exhibitId & b.boothLv == 3).ToList();
            List<Booths> booths2 = _context.boothMaps.Where(b => b.e_Id == exhibitId & b.boothLv == 2).ToList();
            List<Booths> booths1 = _context.boothMaps.Where(b => b.e_Id == exhibitId & b.boothLv == 1).ToList();

            model.exhibitId = exhibition.exhibitId;
            model.exhibitName = exhibition.exhibitName;
            model.exhibitStatus = exhibition.exhibitStatus;
            model.exhibit_P_img = exhibition.exhibit_P_img;
            model.exhibit_T_img = exhibition.exhibit_T_img;
            model.exhibit_Pre_img = exhibition.exhibit_Pre_img;
            model.datefrom = exhibition.datefrom;
            model.dateto = exhibition.dateto;
            model.ex_description = exhibition.ex_Description;
            model.ex_personTime = exhibition.ex_personTime;
            model.ex_totalImcome = exhibition.ex_totalImcome;
            model.ticket_Peice = exhibition.ticket_Price;

            if (booths3.Count > 0)
            {
                CreatBoothsViewModel booth3 = new CreatBoothsViewModel();
                booth3.boothLv = "大型";
                booth3.boothPrice = booths3[0].boothPrice;
                booth3.boothsum = booths3.Count;
                model.setboothslist.Add(booth3);
            }
            if (booths2.Count > 0)
            {
                CreatBoothsViewModel booth2 = new CreatBoothsViewModel();
                booth2.boothLv = "中型";
                booth2.boothPrice = booths2[0].boothPrice;
                booth2.boothsum = booths2.Count;
                model.setboothslist.Add(booth2);
            }
            if (booths1.Count > 0)
            {
                CreatBoothsViewModel booth1 = new CreatBoothsViewModel();
                booth1.boothLv = "小型";
                booth1.boothPrice = booths1[0].boothPrice;
                booth1.boothsum = booths1.Count;
                model.setboothslist.Add(booth1);
            }


            //List<CheckApplyViewModel> modellist = new List<CheckApplyViewModel>();

            model.applylist = new List<CheckApplyViewModel>();
            List<Apply> applies = _context.Applies.Where(a => a.e_Id == exhibitId).ToList();
            for (int i = 0; i < applies.Count; i++)
            {
                CheckApplyViewModel checkapply = new CheckApplyViewModel();
                checkapply.applyNum = applies[i].applyNum;
                checkapply.mf_Id = applies[i].mf_Id;
                checkapply.mf_Id = applies[i].mf_Id;
                checkapply.boothNumber = applies[i].boothNumber;
                checkapply.checkState = applies[i].checkState;
                checkapply.mf_logo = applies[i].mf_logo;
                checkapply.mf_P_img = applies[i].mf_P_img;
                checkapply.mf_Description = applies[i].mf_Description;
                var m = _context.manufactures.Where(m => m.manufactureId == applies[i].mf_Id);
                Manufactures manufactures = m.FirstOrDefault();
                checkapply.manufactureId = manufactures.manufactureId;
                checkapply.manufactureAcc = manufactures.manufactureAcc;
                checkapply.manufactureName = manufactures.manufactureName;
                checkapply.mfPerson = manufactures.mfPerson;
                checkapply.mfPhoneNum = manufactures.mfPhoneNum;
                model.applylist.Add(checkapply);
            }



            return View(model);
        }

        [Route("/Admin/{action}/{exhibitId}")]
        [HttpPost]
        public async Task<IActionResult> ExhibitIdDetail(ExhibitIdDetail_1_ model)
        {
            string img_dir = @$"wwwroot/images/";
            Random myRand = new Random();
            var e = _context.exhibitions.Where(e => e.exhibitId == model.exhibitId);
            Exhibition exhibition = e.FirstOrDefault();
            exhibition.exhibitName = model.exhibitName;
            exhibition.datefrom = model.datefrom;
            exhibition.dateto = model.dateto;
            exhibition.ex_Description = model.ex_description;
            exhibition.ex_personTime = model.ex_personTime;
            exhibition.ex_totalImcome = model.ex_totalImcome;
            exhibition.ticket_Price = model.ticket_Peice;

            if (model.fexhibit_T_img != null)
            {
                string Timgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_T_img.FileName);
                exhibition.exhibit_T_img = Timgname;
                using (var stream = System.IO.File.Create(img_dir + Timgname))
                {
                    await model.fexhibit_T_img.CopyToAsync(stream);
                }
            }

            if (model.fexhibit_P_img != null)
            {
                string Pimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_P_img.FileName);
                exhibition.exhibit_P_img = Pimgname;
                using (var stream = System.IO.File.Create(img_dir + Pimgname))
                {
                    await model.fexhibit_P_img.CopyToAsync(stream);
                }
            }

            if (model.fexhibit_Pre_img != null)
            {
                string Preimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_Pre_img.FileName);
                exhibition.exhibit_Pre_img = Preimgname;
                using (var stream = System.IO.File.Create(img_dir + Preimgname))
                {
                    await model.fexhibit_Pre_img.CopyToAsync(stream);
                }

            }
            _context.exhibitions.Update(exhibition);

            foreach (var booth in _context.boothMaps)
            {
                if (booth.e_Id == model.exhibitId)
                {
                    _context.boothMaps.Remove(booth);
                }
            }

            int boothnumber = 1;
            if (model.setboothslist != null)
            {
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
                        await _context.boothMaps.AddAsync(booths);
                        boothnumber++;
                    }
                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }


        [Route("/Admin/{action}/{idnew}")]
        public IActionResult CreatExhibition(int idnew)
        {
            ViewBag.idnew = idnew;
            return View();
        }

        [Route("/Admin/{action}/{idnew}")]
        [HttpPost]
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

            if (model.setboothslist != null)
            {
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

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        public async Task<IActionResult> ChangeExhibitState(int exhibitId)
        {
            var e = _context.exhibitions.Where(e => e.exhibitId == exhibitId);
            Exhibition exhibition = e.FirstOrDefault();
            exhibition.exhibitStatus = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        public async Task<IActionResult> DeleteExhibition(int exhibitId)
        {
            var e = _context.exhibitions.Find(exhibitId);
            _context.exhibitions.Remove(e);
            foreach (var booth in _context.boothMaps)
            {
                if (booth.e_Id == exhibitId)
                {
                    _context.boothMaps.Remove(booth);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        //public async Task<IActionResult> _ApplyPartial(int exhibitId)
        //{
        //    List<CheckApplyViewModel> modellist = new List<CheckApplyViewModel>();
        //    List<Apply> applies = _context.Applies.Where(a => a.e_Id == exhibitId).ToList();
        //    for (int i =0;i<applies.Count;i++)
        //    {
        //        CheckApplyViewModel model = new CheckApplyViewModel();
        //        applies[i].applyNum = model.applyNum;
        //        applies[i].mf_Id = model.mf_Id;
        //        applies[i].boothNumber = model.boothNumber;
        //        applies[i].checkState = model.checkState;
        //        applies[i].mf_logo = model.mf_logo;
        //        applies[i].mf_P_img = model.mf_P_img;
        //        applies[i].mf_Description = model.mf_Description;
        //        var m = _context.manufactures.Where(m => m.manufactureId == applies[i].mf_Id);
        //        Manufactures manufactures = m.FirstOrDefault();
        //        manufactures.manufactureId = model.manufactureId;
        //        manufactures.manufactureAcc = model.manufactureAcc;
        //        manufactures.manufactureName = model.manufactureName;
        //        manufactures.mfPerson = model.mfPerson; 
        //        manufactures.mfPhoneNum = model.mfPhoneNum;
        //        modellist.Add(model);
        //    }
        //    return PartialView(modellist);
        //}





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
