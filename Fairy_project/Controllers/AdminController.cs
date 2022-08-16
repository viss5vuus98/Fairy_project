using Fairy_project.Models;
using Fairy_project.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly woowoContext _context;

        public AdminController(woowoContext context)
        {
            _context = context;
        }
        // GET: AdminController
        public IActionResult Index()
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
            model.areaNum = exhibition.AreaNum;
            model.datefrom = (DateTime)exhibition.Datefrom;
            model.dateto = (DateTime)exhibition.Dateto;
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
                int totalperson = _context.Ticketsses.Where(t => t.EId == exhibitId && t.Enterstate == 1).Count();
                model.insum = totalperson;
                //int days = new TimeSpan(Convert.ToDateTime(exhibition.Dateto).Ticks - Convert.ToDateTime(exhibition.Datefrom).Ticks).Days;
                //if (totalperson != 0)
                //{
                //    model.averageperson = totalperson / days;
                //}
                //else
                //{
                //    model.averageperson = 0;
                //}
                int mostpeoplt = 0;
                for (var day = exhibition.Datefrom.Date; day.Date <= exhibition.Dateto.Date; day = day.AddDays(1))
                {

                    //var q = from t in _context.Ticketsses where t.EId == exhibitId && (DateTime)t.Entertime.Date == day.Date && t.Enterstate == 1 select t;
                    var q = from t in _context.Ticketsses where t.EId == exhibitId && (DateTime)t.Entertime.Date == day.Date && t.Enterstate == 1 select t;
                    if (mostpeoplt < q.Count())
                    {
                        mostpeoplt = q.Count();
                    }
                }
                model.mostpeople = mostpeoplt;
                Console.WriteLine("=========================================================" + mostpeoplt);

                int yesterdayperson = 0;
                var qq = from t in _context.Ticketsses where t.EId == exhibitId && (DateTime)t.Entertime.Date == DateTime.Now.AddDays(-1).Date && t.Enterstate == 1 select t;
                List<Ticketss> tl = _context.Ticketsses.Where(t => t.EId == exhibitId && t.Enterstate == 1).ToList();
                yesterdayperson = qq.Count();
                //foreach (var item in tl)
                //{
                //    if (Convert.ToDateTime(item.Entertime).Date == DateTime.Now.AddDays(-1).Date)
                //    {
                //        yesterdayperson++;
                //    }
                //}
                model.yesterdayperson = yesterdayperson;
                model.soldprice = exhibition.TicketPrice * totalperson;
                model.soldsum = _context.Ticketsses.Where(t => t.EId == exhibitId).Count();
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
            exhibition.AreaNum = model.areaNum;
            //if (model.areaNumstring == "A")
            //{
            //    exhibition.AreaNum = 1;
            //}
            //else if (model.areaNumstring == "B")
            //{
            //    exhibition.AreaNum = 2;
            //}

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
                for (int i = 0; i < model.setboothslist.Count(); i++)
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
            return RedirectToAction("Index");
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
            Exhibitionss laste = _context.Exhibitionsses.OrderBy(e => e.ExhibitId).Last();
            int eid = laste.ExhibitId + 1;

            if (model.setboothslist != null)
            {
                int boothnumber = 1;
                for (int i = 0; i < model.setboothslist.Count(); i++)
                {
            Console.WriteLine("------------------56+5645456----------" + model.setboothslist.Count());
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
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeExhibitState(int exhibitId)
        {
            var e = _context.Exhibitionsses.Where(e => e.ExhibitId == exhibitId);
            Exhibitionss exhibition = e.FirstOrDefault();
            exhibition.ExhibitStatus = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public IActionResult _ApplyPartial_Search(int exhibitId, int? b_num, int? checkstate, int? offset, int? status)
        {
            ExhibitIdDetail_1_ amodel = new ExhibitIdDetail_1_();
            if (status == 3 && b_num.HasValue)
            {
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2 && a.BoothNumber == b_num).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
                amodel.applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2 && a.BoothNumber == b_num).Count();
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
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
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
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate && a.BoothNumber == b_num).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
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
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.BoothNumber == b_num).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
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
                var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == checkstate).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
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
                var a = _context.Appliesses.Where(a => a.EId == exhibitId).Skip((int)offset).Take(10).OrderBy(a => a.BoothNumber);
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
                model.boothLv = _context.BoothMapsses.Where(b => b.BoothNumber == applies[i].BoothNumber).Select(b => b.BoothLv).FirstOrDefault();
                model.boothPrice = _context.BoothMapsses.Where(b => b.BoothNumber == applies[i].BoothNumber).Select(b => b.BoothPrice).FirstOrDefault();
                var m = _context.Manufacturesses.Where(m => m.ManufactureId == applies[i].MfId);
                model.message = applies[i].Message;
                model.paytime = applies[i].PayTime;
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

        public async Task<IActionResult> ChangeBoothApplyState(int exhibitId, int? applynum, string? fail)
        {
            var a = _context.Appliesses.Where(a => a.EId == exhibitId && a.ApplyNum == applynum);
            Appliess apply = a.FirstOrDefault();
            var b = _context.BoothMapsses.Where(b => b.EId == exhibitId && b.BoothNumber == apply.BoothNumber);
            BoothMapss booth = b.FirstOrDefault();
            if (fail != null)
            {
                apply.CheckState = 3;
                apply.Message = fail;
                string email = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.MfEmail).FirstOrDefault().ToString();
                string title = "woohouse-您的攤位申請未通過";
                string mname = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.ManufactureName).FirstOrDefault().ToString();
                string ename = _context.Exhibitionsses.Where(e => e.ExhibitId == apply.EId).Select(e => e.ExhibitName).FirstOrDefault().ToString();
                string content = $"親愛的廠商{mname}您好：\n很抱歉，您申請參加{ename}之{booth.BoothNumber}號攤位未通過\n詳情請登入後查看，謝謝您";
                sendmail(email, title, content);
            }
            if (apply.CheckState == 0)
            {
                List<Appliess>? to3 = _context.Appliesses.Where(a => a.BoothNumber == apply.BoothNumber).ToList();
                foreach (var item in to3)
                {
                    item.CheckState = 3;
                }
                apply.CheckState = 1;
                string email = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.MfEmail).FirstOrDefault().ToString();
                string title = "woohouse-您的攤位申請已通過";
                string mname = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.ManufactureName).FirstOrDefault().ToString();
                string ename = _context.Exhibitionsses.Where(e => e.ExhibitId == apply.EId).Select(e => e.ExhibitName).FirstOrDefault().ToString();
                string bprice = _context.BoothMapsses.Where(b => b.EId == apply.EId && b.BoothNumber == apply.BoothNumber).Select(b => b.BoothPrice).FirstOrDefault().ToString();
                string content = $"親愛的廠商{mname}您好：\n您申請參加{ename}之{booth.BoothNumber}號攤位已通過\n\n\n請於三日內完成轉帳付款\n銀行代碼：000\n帳號：012345678\n金額：{bprice}\n\n\n謝謝您！";
                sendmail(email, title, content);

            }
            else if (apply.CheckState == 1)
            {
                apply.CheckState = 2;
                booth.BoothState = 1;
                booth.MfId = apply.MfId;
                string email = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.MfEmail).FirstOrDefault().ToString();
                string title = "woohouse-您的攤位付款已對帳";
                string mname = _context.Manufacturesses.Where(m => m.ManufactureId == apply.MfId).Select(m => m.ManufactureName).FirstOrDefault().ToString();
                string ename = _context.Exhibitionsses.Where(e => e.ExhibitId == apply.EId).Select(e => e.ExhibitName).FirstOrDefault().ToString();
                string content = $"親愛的廠商{mname}您好：\n您申請參加{ename}之{booth.BoothNumber}號攤位已確認對帳\n詳情請登入後查看，謝謝您";
                sendmail(email, title, content);

            }
            await _context.SaveChangesAsync();

            int applysum = 0;
            int? applysumprice = 0;
            applysum = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Count();
            applysumprice = _context.BoothMapsses.Where(b => b.EId == exhibitId && _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2).Select(a => a.BoothNumber).Contains(b.BoothNumber)).Select(b => b.BoothPrice).Sum();

            //var aa = _context.Appliesses.Where(a => a.EId == exhibitId && a.CheckState == 2);
            //var bb = _context.BoothMapsses.Where(b => b.EId == exhibitId);
            //List<Appliess> Appliess = aa.ToList();
            //List<BoothMapss> boothMapsses = bb.ToList();
            //foreach (Appliess Apply in Appliess)
            //{
            //    foreach (BoothMapss Booth in boothMapsses)
            //    {
            //        if (Apply.BoothNumber == Booth.BoothNumber)
            //        {
            //            applysumprice += Booth.BoothPrice;
            //        }
            //    }
            //}

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

        [HttpPost]
        public async Task<IActionResult> _Exhibition_Search(string? keyword, List<int>? state, DateTime? datefrom, DateTime? dateto)
        {

            Console.WriteLine("-------------------------");
            var q = _context.Exhibitionsses.Where(e => e.ExhibitStatus != 4);
            if (keyword != null)
            {
                q = q.Where(e => e.ExhibitName.Contains(keyword) || e.ExDescription.Contains(keyword));
            }
            if (state.Count() > 0)
            {
                q = q.Where(e => state.Contains((int)e.ExhibitStatus));

            }
            if (datefrom != null && dateto != null)
            {
                q = q.Where(e => e.Datefrom.Date >= datefrom && e.Dateto.Date <= dateto);
            }
            List<Exhibitionss> el = q.OrderBy(e => e.ExhibitStatus).ToList();
            return PartialView("_ExhibitionPartial", el);
        }

        public IActionResult Report()
        {
            ViewBag.ename = _context.Exhibitionsses.Where(e => e.ExhibitStatus == 4).Select(e => e.ExhibitName).ToList();
            return View();
        }

        public IActionResult DateSearchExhibition(int? year, int? month)
        {
            List<string> ename = new List<string>();
            if (year.HasValue && month.HasValue)
            {
                ename = _context.Exhibitionsses.Where(e => e.ExhibitStatus == 4 && e.Datefrom.Year == year && e.Datefrom.Month == month).Select(e => e.ExhibitName).ToList();
            }
            else if (year.HasValue)
            {
                ename = _context.Exhibitionsses.Where(e => e.ExhibitStatus == 4 && e.Datefrom.Year == year).Select(e => e.ExhibitName).ToList();
            }
            else if (month.HasValue)
            {
                ename = _context.Exhibitionsses.Where(e => e.ExhibitStatus == 4 && e.Datefrom.Month == month).Select(e => e.ExhibitName).ToList();
            }
            return Json(new { ename = ename });
        }

        public IActionResult ReportShow(string ename)
        {
            Exhibitionss e = _context.Exhibitionsses.Where(e => e.ExhibitName == ename).FirstOrDefault();
            string name = e.ExhibitName;
            string edate = Convert.ToDateTime(e.Datefrom).ToShortDateString() + " - " + Convert.ToDateTime(e.Dateto).ToShortDateString();
            bool priceattain;
            bool peopleattain;
            int? ExTotalImcome = e.ExTotalImcome;
            int? ExPersonTime = e.ExPersonTime;
            decimal people = 0;
            if (_context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count() != 0)
            {
                //decimal enterpeople = _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count();
                //decimal soldtickets = _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Count();
                //people = _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count() / _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Count();
                people = Math.Round((decimal)_context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count() / _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Count() * 100, 0);
            }
            else
            {
                people = 0;
            }
            decimal booth = 0;
            if (_context.BoothMapsses.Where(b => b.EId == e.ExhibitId && b.BoothState == 1).Count() != 0)
            {
                booth = Math.Round((decimal)_context.Appliesses.Where(a => a.EId == e.ExhibitId && a.CheckState == 3).Count() / _context.BoothMapsses.Where(b => b.EId == e.ExhibitId).Count() * 100, 0);
            }
            else
            {
                booth = 0;
            }


            int soldticketsum = _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Count();
            int? soldticketprice = _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Select(t => t.Price).Sum();
            int soldboothsum = _context.BoothMapsses.Where(b => b.EId == e.ExhibitId && _context.Appliesses.Where(a => a.EId == e.ExhibitId && a.CheckState == 2).Select(a => a.BoothNumber).ToList().Contains(b.BoothNumber)).Count();
            int? soldboothprice = _context.BoothMapsses.Where(b => b.EId == e.ExhibitId && _context.Appliesses.Where(a => a.EId == e.ExhibitId && a.CheckState == 2).Select(a => a.BoothNumber).ToList().Contains(b.BoothNumber)).Select(b => b.BoothPrice).Sum();

            if (e.ExTotalImcome < soldticketprice + soldticketprice)
            {
                priceattain = true;
            }
            else
            {
                priceattain = false;
            }
            if (e.ExPersonTime < _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count())
            {
                peopleattain = true;
            }
            else
            {
                peopleattain = false;
            }

            int averageperson = 0;
            if (_context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count() != 0)
            {
                averageperson = _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Count() / new TimeSpan(Convert.ToDateTime(e.Dateto).Ticks - Convert.ToDateTime(e.Datefrom).Ticks).Days;
            }
            else
            {
                averageperson = 0;
            }

            int? pricesum = 0;
            //List<Dictionary<int, int>> boothandprice = new List<Dictionary<int, int>>();
            List<BoothMapss> b = _context.BoothMapsses.Where(b => b.EId == e.ExhibitId && _context.Appliesses.Where(a => a.EId == e.ExhibitId && a.CheckState == 2).Select(a => a.BoothNumber).ToList().Contains(b.BoothNumber)).ToList();
            if (b.Count != 0)
            {
                foreach (var item in b)
                {
                    pricesum += item.BoothPrice;
                }
            }
            pricesum += _context.Ticketsses.Where(t => t.EId == e.ExhibitId).Select(t => t.Price).Sum();

            int female = _context.Membersses.Where(m => _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Select(t => t.MId).ToList().Contains(m.MemberId) && m.Gender == "2").Count();
            int male = _context.Membersses.Where(m => m.Gender == "1" && _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1).Select(t => t.MId).ToList().Contains(m.MemberId)).Count();


            var t = _context.Ticketsses.Where(t => t.EId == e.ExhibitId && t.Enterstate == 1);
            List<Ticketss> ticketsses = t.ToList();
            List<string> datepick = new List<string>();
            List<string> datereport = new List<string>();
            List<int> reportsum = new List<int>();
            int i = 0;

            for (var day = e.Datefrom; day <= e.Dateto; day = day.AddDays(1))
            {
                reportsum.Add(0);
                datepick.Add(Convert.ToDateTime(day).ToString("yyyy-MM-dd"));
                datereport.Add(Convert.ToDateTime(day).ToString("MM-dd"));

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

            return Json(new { ename = name, edate = edate, people = people, booth = booth, soldticketsum = soldticketsum, soldticketprice = soldticketprice, soldboothsum = soldboothsum, soldboothprice = soldboothprice, averageperson = averageperson, pricesum = pricesum, female = female, male = male, datepick = datepick, reportsum = reportsum, datereport = datereport, exTotalImcome = ExTotalImcome, exPersonTime = ExPersonTime, priceattain = priceattain, peopleattain = peopleattain });
        }
        public IActionResult RenderChart(string? edate, string? ename)
        {
            edate.Split(" - ").ToList();
            DateTime datefrom = Convert.ToDateTime(edate.Split(" - ")[0]);
            DateTime dateto = Convert.ToDateTime(edate.Split(" - ")[1]);
            int eid = _context.Exhibitionsses.Where(e => e.ExhibitName == ename).Select(e => e.ExhibitId).FirstOrDefault();

            var t = _context.Ticketsses.Where(t => t.EId == eid && t.Enterstate == 1 && DateTime.Compare((DateTime)t.Entertime, datefrom) == 1 && DateTime.Compare((DateTime)t.Entertime, dateto.AddDays(1)) == -1).OrderBy(t => t.Entertime);
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


        public IActionResult ToHomePage()
        {
            TempData["layout"] = "admin";
            return RedirectToAction("Index", "Home");
        }


        public async void sendmail(string email, string title, string content)
        {
            var mail = new MailMessage();
            mail.To.Add($"{email}");
            mail.Subject = $"{title}";
            mail.Body = $"{content}";
            mail.From = new MailAddress("zxc995116@gmail.com", "woohouse");
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("zxc995116@gmail.com", "rpsxltaiqhdmupnp"),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
            mail.Dispose();
        }



























        [HttpPost]
        public IActionResult postQrCode([FromBody] QrCode ticket)
        {
            var ticketCoent = _context.Ticketsses.FirstOrDefault(t => t.VerificationCode == ticket.VerificationCode && t.Enterstate == 0) ?? new Ticketss();
            if (ticketCoent.VerificationCode != null)
            {
                var exId = ticketCoent.EId;
                if (exId == Convert.ToInt32(ticket.Ex_id))
                {
                    ticketCoent.Enterstate = 1;
                    ticketCoent.Entertime = DateTime.Now;
                    _context.SaveChanges();
                    return Json($"歡迎會員No.{ticketCoent.MId}入場");
                }
                else
                {
                    return Json("展覽錯誤");
                }
            }
            else
            {
                return Json("無此票券");
            }
        }

        [HttpPost]
        public IActionResult postMfQrCode([FromBody] MfQrCode mfQrCode)
        {
            int exId = Convert.ToInt32(mfQrCode.ex_id);
            int boothNum = Convert.ToInt32(mfQrCode.boothNum);
            int mfId = Convert.ToInt32(mfQrCode.mf_id);
            IList<BoothMapss> booths = _context.BoothMapsses.Where(b => b.EId == exId && b.BoothState == 1).ToList();
            if (booths.Count > 0)
            {
                var TheBooth = booths.FirstOrDefault(b => b.BoothNumber == boothNum);
                if (TheBooth.MfId == mfId)
                {
                    return Json($"廠商No.{mfId}成功進入");
                }
                else
                {
                    return Json("無效攤位");
                }
            }

            return Json("無效展覽");
        }


    }
}
