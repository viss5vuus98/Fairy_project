using Fairy_project.Models;
using Fairy_project.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Fairy_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly woowoContext _context;

        public AdminController(woowoContext context)
        {
            _context = context;
        }
        // GET: AdminController
        public IActionResult Master()
        {
            foreach (var exhibition in _context.Exhibitionsses)
            {
                if (exhibition.ExhibitStatus == 2 && DateTime.Compare(DateTime.Now, (DateTime)exhibition.Datefrom) == 1)
                {
                    exhibition.ExhibitStatus = 3;
                    _context.Exhibitionsses.Update(exhibition);
                }
                else if (exhibition.ExhibitStatus == 3 && DateTime.Compare(DateTime.Now, (DateTime)exhibition.Dateto) == 1)
                {
                    exhibition.ExhibitStatus = 4;
                    _context.Exhibitionsses.Update(exhibition);
                }
            }
            _context.SaveChanges();

            var q = from e in _context.Exhibitionsses where e.ExhibitStatus != 4 orderby e.ExhibitStatus select e;
            List<Exhibitionss> el = new List<Exhibitionss>();
            el = q.ToList();
            el.Reverse();
            return View(el);
        }

        [Route("/Admin/{action}/{exhibitId}")]
        public async Task<IActionResult> ExhibitIdDetail(int exhibitId)
        {
            ExhibitIdDetail_1_ model = new ExhibitIdDetail_1_();
            model.setboothslist = new List<CreatBoothsViewModel>();
            var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId);
            Exhibitionss exhibition = e.FirstOrDefault();
            List<BoothMapss> booths3 = _context.BoothMapsses.Where(b => b.EId == exhibitId & b.BoothLv == 3).ToList();
            List<BoothMapss> booths2 = _context.BoothMapsses.Where(b => b.EId == exhibitId & b.BoothLv == 2).ToList();
            List<BoothMapss> booths1 = _context.BoothMapsses.Where(b => b.EId == exhibitId & b.BoothLv == 1).ToList();

            model.exhibitId = exhibition.ExhibitId;
            model.exhibitName = exhibition.ExhibitName;
            model.exhibitStatus = exhibition.ExhibitStatus;
            model.exhibit_P_img = exhibition.ExhibitPImg;
            model.exhibit_T_img = exhibition.ExhibitTImg;
            model.exhibit_Pre_img = exhibition.ExhibitPreImg;
            model.datefrom = exhibition.Datefrom;
            model.dateto = exhibition.Dateto;
            model.ex_description = exhibition.ExDescription;
            model.ex_personTime = exhibition.ExPersonTime;
            model.ex_totalImcome = exhibition.ExTotalImcome;
            model.ticket_Peice = exhibition.TicketPrice;
            model.applysumprice = 0;
            model.applysum = _context.Appliesses.Where(a => a.EId == model.exhibitId && a.CheckState == 2).Count();
            var a = _context.Appliesses.Where(a => a.EId == model.exhibitId && a.CheckState == 2);
            var b = _context.BoothMapsses.Where(b => b.EId == model.exhibitId);
            List<Appliess> Appliess = a.ToList();
            List<BoothMapss> boothMapsses = b.ToList();
            foreach (Appliess Apply in Appliess)
            {
                foreach (BoothMapss booth in boothMapsses)
                {
                    if (Apply.BoothNumber == booth.BoothNumber)
                    {
                        model.applysumprice += booth.BoothPrice;
                    }
                }
            }

            if (exhibition.AreaNum == 1)
            {
                model.areaNumstring = "A";
            }
            else if (exhibition.AreaNum == 2)
            {
                model.areaNumstring = "B";
            }

            if (booths3.Count > 0)
            {
                CreatBoothsViewModel booth3 = new CreatBoothsViewModel();
                booth3.boothLv = "大型";
                booth3.boothPrice = booths3[0].BoothPrice;
                booth3.boothsum = booths3.Count;
                model.setboothslist.Add(booth3);
            }
            if (booths2.Count > 0)
            {
                CreatBoothsViewModel booth2 = new CreatBoothsViewModel();
                booth2.boothLv = "中型";
                booth2.boothPrice = booths2[0].BoothPrice;
                booth2.boothsum = booths2.Count;
                model.setboothslist.Add(booth2);
            }
            if (booths1.Count > 0)
            {
                CreatBoothsViewModel booth1 = new CreatBoothsViewModel();
                booth1.boothLv = "小型";
                booth1.boothPrice = booths1[0].BoothPrice;
                booth1.boothsum = booths1.Count;
                model.setboothslist.Add(booth1);
            }

            if (exhibition.ExhibitStatus == 3)
            {
                int days = new TimeSpan(Convert.ToDateTime(exhibition.Dateto).Ticks - Convert.ToDateTime(exhibition.Datefrom).Ticks).Days;
                int totalperson = _context.Ticketsses.Where(t => t.EId == exhibitId && t.Enterstate == 1).Count();
                if (totalperson != 0)
                {
                    model.averageperson = totalperson / days;
                }
                List<Ticketss> tl = _context.Ticketsses.Where(t => t.EId == exhibitId && t.Enterstate == 1).ToList();
                //var t = from tl in _context.Ticketsses where tl.EId == exhibitId && tl.Enterstate == 1 && tl.Entertime. == DateTime.Now.AddDays(-1).Date select tl;
                //t.Entertime == DateTime.Now.AddDays(-1).Date
                int yesterdayperson = 0;
                foreach (var item in tl)
                {
                    if (Convert.ToDateTime(item.Entertime).Date == DateTime.Now.AddDays(-1).Date)
                    {
                        yesterdayperson++;
                    }
                }
                Console.WriteLine("=========================================================" + yesterdayperson);
                model.yesterdayperson = yesterdayperson;
                model.soldprice = exhibition.TicketPrice * totalperson;
                model.soldsum = _context.Ticketsses.Where(t => t.EId == exhibitId).Count();
                model.insum = totalperson;
            }
            return View(model);
        }

        [Route("/Admin/{action}/{exhibitId}")]
        [HttpPost]
        public async Task<IActionResult> ExhibitIdDetail(ExhibitIdDetail_1_ model)
        {
            string img_dir = @$"wwwroot/images/";
            Random myRand = new Random();
            var e = _context.Exhibitionsses.Where(e => e.ExhibitId == model.exhibitId);
            Exhibitionss exhibition = e.FirstOrDefault();

            exhibition.ExhibitName = model.exhibitName;
            exhibition.Datefrom = model.datefrom;
            exhibition.Dateto = model.dateto;
            exhibition.ExDescription = model.ex_description;
            exhibition.ExPersonTime = model.ex_personTime;
            exhibition.ExTotalImcome = model.ex_totalImcome;
            exhibition.TicketPrice = model.ticket_Peice;

            if (model.areaNumstring == "A")
            {
                exhibition.AreaNum = 1;
            }
            else if (model.areaNumstring == "B")
            {
                exhibition.AreaNum = 2;
            }

            if (model.fexhibit_T_img != null)
            {
                string Timgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_T_img.FileName);
                exhibition.ExhibitTImg = Timgname;
                using (var stream = System.IO.File.Create(img_dir + Timgname))
                {
                    await model.fexhibit_T_img.CopyToAsync(stream);
                }
            }

            if (model.fexhibit_P_img != null)
            {
                string Pimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_P_img.FileName);
                exhibition.ExhibitPImg = Pimgname;
                using (var stream = System.IO.File.Create(img_dir + Pimgname))
                {
                    await model.fexhibit_P_img.CopyToAsync(stream);
                }
            }

            if (model.fexhibit_Pre_img != null)
            {
                string Preimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.fexhibit_Pre_img.FileName);
                exhibition.ExhibitPreImg = Preimgname;
                using (var stream = System.IO.File.Create(img_dir + Preimgname))
                {
                    await model.fexhibit_Pre_img.CopyToAsync(stream);
                }

            }
            _context.Exhibitionsses.Update(exhibition);

            if (model.exhibitStatus == 1)
            {
                foreach (var booth in _context.BoothMapsses)
                {
                    if (booth.EId == model.exhibitId)
                    {
                        _context.BoothMapsses.Remove(booth);
                    }
                }
            }

            int boothnumber = 1;
            if (model.setboothslist != null)
            {
                for (int i = 0; i < model.setboothslist.Count; i++)
                {
                    for (int j = 0; j < model.setboothslist[i].boothsum; j++)
                    {
                        BoothMapss booths = new BoothMapss();
                        booths.BoothNumber = boothnumber;
                        booths.BoothState = 0;
                        if (model.setboothslist[i].boothLv == "大型")
                        {
                            booths.BoothLv = 3;
                        }
                        else if (model.setboothslist[i].boothLv == "中型")
                        {
                            booths.BoothLv = 2;
                        }
                        else if (model.setboothslist[i].boothLv == "小型")
                        {
                            booths.BoothLv = 1;
                        }
                        booths.BoothPrice = model.setboothslist[i].boothPrice;
                        booths.EId = model.exhibitId;
                        await _context.BoothMapsses.AddAsync(booths);
                        boothnumber++;
                    }
                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }


        public IActionResult CreatExhibition()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatExhibition(CreatExhibitionViewModel model)
        {
            string img_dir = @$"wwwroot/images/";
            Random myRand = new Random();
            Exhibitionss exhibition = new Exhibitionss();
            exhibition.ExhibitName = model.exhibitName;
            exhibition.ExhibitStatus = 1;
            exhibition.Datefrom = model.datefrom;
            exhibition.Dateto = model.dateto;
            exhibition.ExDescription = model.ex_description;
            exhibition.ExPersonTime = model.ex_personTime;
            exhibition.ExTotalImcome = model.ex_totalImcome;
            exhibition.TicketPrice = model.ticket_Peice;
            exhibition.AreaNum = model.areaNum;

            if (model.exhibit_P_img != null)
            {
                string Pimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_P_img.FileName);
                exhibition.ExhibitPImg = Pimgname;
                using (var stream = System.IO.File.Create(img_dir + Pimgname))
                {
                    await model.exhibit_P_img.CopyToAsync(stream);
                }
            }

            if (model.exhibit_T_img != null)
            {
                string Timgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_T_img.FileName);
                exhibition.ExhibitTImg = Timgname;
                using (var stream = System.IO.File.Create(img_dir + Timgname))
                {
                    await model.exhibit_T_img.CopyToAsync(stream);
                }
            }

            if (model.exhibit_Pre_img != null)
            {
                string Preimgname = DateTime.Now.ToString("yyyyMMdHHmmss") + myRand.Next(1000, 10000).ToString() + Path.GetExtension(model.exhibit_Pre_img.FileName);
                exhibition.ExhibitPreImg = Preimgname;
                using (var stream = System.IO.File.Create(img_dir + Preimgname))
                {
                    await model.exhibit_Pre_img.CopyToAsync(stream);
                }
            }
            _context.Exhibitionsses.Add(exhibition);
            Exhibitionss laste =  _context.Exhibitionsses.OrderBy(e=>e.ExhibitId).Last();
            int eid = laste.ExhibitId +1;


            if (model.setboothslist != null)
            {
                int boothnumber = 1;
                for (int i = 0; i < model.setboothslist.Count; i++)
                {
                    for (int j = 0; j < model.setboothslist[i].boothsum; j++)
                    {
                        BoothMapss booths = new BoothMapss();
                        booths.BoothNumber = boothnumber;
                        booths.BoothState = 0;
                        if (model.setboothslist[i].boothLv == "大型")
                        {
                            booths.BoothLv = 3;
                        }
                        else if (model.setboothslist[i].boothLv == "中型")
                        {
                            booths.BoothLv = 2;
                        }
                        else if (model.setboothslist[i].boothLv == "小型")
                        {
                            booths.BoothLv = 1;
                        }
                        booths.BoothPrice = model.setboothslist[i].boothPrice;
                        booths.EId = eid;
                        _context.BoothMapsses.Add(booths);
                        boothnumber++;
                    }
                }

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        public async Task<IActionResult> ChangeExhibitState(int exhibitId)
        {
            var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId);
            Exhibitionss exhibition = e.FirstOrDefault();
            exhibition.ExhibitStatus = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        public async Task<IActionResult> DeleteExhibition(int exhibitId)
        {
            var e = _context.Exhibitionsses.Find(exhibitId);
            _context.Exhibitionsses.Remove(e);
            foreach (var booth in _context.BoothMapsses)
            {
                if (booth.EId == exhibitId)
                {
                    _context.BoothMapsses.Remove(booth);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Master");
        }

        public IActionResult _ApplyPartial_Search(int exhibitId, int? b_num, int? checkstate, int? offset, int? status)
        {
            ExhibitIdDetail_1_ amodel = new ExhibitIdDetail_1_();
            if (status == 3 && b_num.HasValue)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2 && a.BoothNumber == b_num);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Count();
                List<Appliess> applies = a.ToList();
                if (offset.HasValue)
                {
                    applies.Skip((int)offset).Take(10);
                }
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;

            }
            else if (status == 3)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Count();
                List<Appliess> applies = a.ToList();
                if (offset.HasValue)
                {
                    applies.Skip((int)offset).Take(10);
                }
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;
            }
            else if (b_num.HasValue && checkstate.HasValue)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate && a.BoothNumber == b_num).Skip((int)offset).Take(10);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate && a.BoothNumber == b_num).Count();
                List<Appliess> applies = a.ToList();
                if (offset.HasValue)
                {
                    applies.Skip((int)offset).Take(10);
                }
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;
            }
            else if (b_num.HasValue)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.BoothNumber == b_num).Skip((int)offset).Take(10);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.BoothNumber == b_num).Count();
                List<Appliess> applies = a.ToList();
                if (offset.HasValue)
                {
                    applies.Skip((int)offset).Take(10);
                }
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;
            }
            else if (checkstate.HasValue)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate).Skip((int)offset).Take(10);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate).Count();
                List<Appliess> applies = a.ToList();
                if (offset.HasValue)
                {
                    applies.Skip((int)offset).Take(10);
                }
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;
            }
            else
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId).Skip((int)offset).Take(10);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId).Count();
                List<Appliess> applies = a.ToList();
                amodel.applylist = Search(applies);
                amodel.exhibitId = exhibitId;
                var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId).FirstOrDefault();
                amodel.exhibitStatus = e.ExhibitStatus;
            }
            return PartialView("_ApplyPartial", amodel);
        }

        List<CheckApplyViewModel> Search(List<Appliess> applies)
        {
            List<CheckApplyViewModel> modellist = new List<CheckApplyViewModel>();
            for (int i = 0; i < applies.Count; i++)
            {
                CheckApplyViewModel model = new CheckApplyViewModel();
                model.applyNum = applies[i].ApplyNum;
                model.mf_Id = applies[i].MfId;
                model.boothNumber = applies[i].BoothNumber;
                model.checkState = applies[i].CheckState;
                model.mf_logo = applies[i].MfLogo;
                model.mf_P_img = applies[i].MfPImg;
                model.mf_Description = applies[i].MfDescription;
                var m = _context.Manufacturesses.Where(m => m.ManufactureId == applies[i].MfId);
                Manufacturess manufactures = m.FirstOrDefault();
                model.manufactureId = manufactures.ManufactureId;
                model.manufactureAcc = manufactures.ManufactureAcc;
                model.manufactureName = manufactures.ManufactureName;
                model.mfPerson = manufactures.MfPerson;
                model.mfPhoneNum = manufactures.MfPhoneNum;
                modellist.Add(model);
            }
            return modellist;
        }

        public async Task<IActionResult> ChangeBoothApplyState(int exhibitId, int? boothNumber, bool? fail)
        {
            var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.BoothNumber == boothNumber);
            var b = _context.BoothMapsses.Where(b => b.EId == exhibitId && b.BoothNumber == boothNumber);
            Appliess apply = a.FirstOrDefault();
            BoothMapss booth = b.FirstOrDefault();
            if (fail.HasValue)
            {
                apply.CheckState = 3;
            }
            if (apply.CheckState == 0)
            {
                apply.CheckState = 1;
                booth.BoothState = 1;
            }
            else if (apply.CheckState == 1)
            {
                apply.CheckState = 2;
            }
            await _context.SaveChangesAsync();

            int applysum = 0;
            int? applysumprice = 0;
            applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Count();
            var aa = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2);
            var bb = _context.BoothMapsses.Where(b => b.EId == exhibitId);
            List<Appliess> Appliess = aa.ToList();
            List<BoothMapss> boothMapsses = bb.ToList();
            foreach (Appliess Apply in Appliess)
            {
                foreach (BoothMapss Booth in boothMapsses)
                {
                    if (Apply.BoothNumber == Booth.BoothNumber)
                    {
                        applysumprice += Booth.BoothPrice;
                    }
                }
            }

            return Json(new { applysum = applysum, applysumprice = applysumprice });
        }


        [HttpPost]
        public async Task<IActionResult> SearchTicketReport(int exhibitId, DateTime datefrom, DateTime dateto)
        {
            var t = _context.Ticketsses.Where(t => t.EId == exhibitId && t.Enterstate == 1 && DateTime.Compare((DateTime)t.Entertime, datefrom) == 1 && DateTime.Compare((DateTime)t.Entertime, dateto.AddDays(1)) == -1).OrderBy(t => t.Entertime);
            List<Ticketss> ticketsses = t.ToList();
            List<string> reportday = new List<string>();
            List<int> reportsum = new List<int>();

            int i = 0;

            for (var day = datefrom; day <= dateto; day = day.AddDays(1))
            {
                reportsum.Add(0);
                reportday.Add(Convert.ToDateTime(day).ToString("MM-dd"));
                for (int j = 0; j < ticketsses.Count; j++)
                {
                    if (Convert.ToDateTime(ticketsses[j].Entertime).Date == day.Date)
                    {
                        reportsum[i]++;
                    }
                    else
                    {
                        continue;
                    }
                }
                i++;
            }
            return Json(new { reportday = reportday, reportsum = reportsum });
        }
    }
}
