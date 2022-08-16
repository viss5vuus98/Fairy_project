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
using System.Net.Mime;

namespace Fairy_project.Controllers
{


    [Authorize(Roles = "Manufacturer")]
    public class ManufacturerController : Controller
    {
        private readonly woowoContext _woowocontext;
        public ManufacturerController(woowoContext woowocontext)
        {
            _woowocontext = woowocontext;
        }
        public IActionResult Apply()
        {
            return View();
        }
        public IActionResult Policy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ApplyStand(string boothId, string e_Id, string mid)
        {
            Appliess model = new Appliess();
            model.BoothNumber = Convert.ToInt32(boothId);
            model.EId = Convert.ToInt32(e_Id);
            model.MfId = Convert.ToInt32(mid);


            return View(model);
        }
        public IActionResult Index()
        {




            return View();
        }

        //get All exhibition 找所有展覽
        [HttpGet]
        public IActionResult getAllExhibition()
        {
            // DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var exhibitions = _woowocontext.Exhibitionsses.Where(m => m.ExhibitStatus == 2);
            return Json(exhibitions);
        }

        //post id for the exhibition 傳入Ex_id 找展覽
        [HttpPost]
        public IActionResult getTheExhibition([FromBody] GetIdClassModel idClass)
        {
            //Console.WriteLine(idClass.Ex_id + "-------------------");
            //var id = Convert.ToInt32(idClass.Ex_id);
            var exhibition = _woowocontext.Exhibitionsses.FirstOrDefault(m => m.ExhibitId == idClass.Ex_id);
            return Json(exhibition);
        }

        //get the exhibition of applies 找一個展覽的所有審核 傳入Ex_id

        [HttpPost]
        public IActionResult getExApplies([FromBody] GetIdClassModel idClass)
        {
            var applies = _woowocontext.Appliesses.Where(m => m.EId == idClass.Ex_id);
            return Json(applies);
        }

        [HttpPost]
        public IActionResult updateApplies([FromBody] postPaydate d)
         {
            Console.WriteLine("-------------------------------------------------" + d.id);
            Console.WriteLine("-------------------------------------------------" + d.fivenum);
            Console.WriteLine("-------------------------------------------------" + d.time.ToString());
            var a = _woowocontext.Appliesses.First(b => b.ApplyNum == d.id);
            a.PayTime = d.time;
            a.Message = d.fivenum;
            _woowocontext.Appliesses.Update(a);
            _woowocontext.SaveChanges();

            //return Json(new { res= "謝謝您的申請，所有資料均為人工審核，審核完畢後會另行通知" });
            return Json("謝謝您的申請，所有資料均為人工審核，審核完畢後會另行通知");
        }
       
        //create the exhibition of applies 新增申請 傳入applies內的物件
        [HttpPost]
        public async Task<IActionResult> createApplies(CreatAppliessViewModels apply)
        {
            string img_dir = @$"wwwroot/images/";

            Random rnd = new Random();
            var b = _woowocontext.BoothMapsses.FirstOrDefault(b => b.EId == Convert.ToInt32(apply.EId) && b.BoothNumber == Convert.ToInt32(apply.BoothNumber));

            Appliess appliess = new Appliess();

            appliess.MfId = Convert.ToInt32(apply.MfId);
            appliess.EId = Convert.ToInt32(apply.EId);
            appliess.BoothNumber = Convert.ToInt32(apply.BoothNumber);
            appliess.MfDescription = apply.MfDescription;
            appliess.CheckState = 0;

            if (apply.MfPImg != null)
            {
                string pimgName = DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(1000, 10000).ToString() + Path.GetExtension(apply.MfPImg.FileName);
                appliess.MfPImg = pimgName;
                b.MfPImg = pimgName;
                using (var stream = System.IO.File.Create(img_dir + pimgName))
                {
                    await apply.MfPImg.CopyToAsync(stream);
                }
            }
            if (apply.MfLogo != null)
            {
                string LimgName = DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(1000, 10000).ToString() + Path.GetExtension(apply.MfLogo.FileName);
                appliess.MfLogo = LimgName;
                b.MfLogo = LimgName;
                using (var stream = System.IO.File.Create(img_dir + LimgName))
                {
                    await apply.MfLogo.CopyToAsync(stream);
                }
            }
            Console.WriteLine("-------------------------" + b);




            _woowocontext.Appliesses.Add(appliess);
            _woowocontext.BoothMapsses.Update(b);
            _woowocontext.SaveChanges();

            var mail = new MailMessage();
            mail.IsBodyHtml = true;
            var logopath = $"{img_dir + b.MfLogo}";
            var pimgpath = $"{img_dir + b.MfPImg}";
            string ename = _woowocontext.Exhibitionsses.Where(e => e.ExhibitId == b.EId).Select(e => e.ExhibitName).FirstOrDefault().ToString();
            string bnum = b.BoothNumber.ToString();
            string bdes = appliess.MfDescription;
            mail.AlternateViews.Add(await GetEmbeddedImage(logopath, pimgpath, ename, bnum, bdes));
            mail.To.Add("ispanwoo@gmail.com");
            mail.Subject = "TESTTTTTTTT";
            mail.From = new MailAddress("zxc995116@gmail.com", "woohouse");
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("zxc995116@gmail.com", "rpsxltaiqhdmupnp"),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
            mail.Dispose();

            return Redirect("Index");

        }

        async private Task<AlternateView> GetEmbeddedImage(string logopath, string pimgpath, string ename, string bnum, string bdes)
        {
            LinkedResource res = new LinkedResource(logopath, "image/jpeg");
            LinkedResource res2 = new LinkedResource(pimgpath, "image/jpeg");
            res.ContentId = Guid.NewGuid().ToString();
            res2.ContentId = Guid.NewGuid().ToString();
            string htmlBody = $"<h2 style='color: #000000;'>以下是您申請 <span style='color: #c0b0a2;'>{ename}</span> 展覽<br>第 <span style='color: #c0b0a2;'>{bnum}</span> 號攤位的申請資訊</h2><div style='width: 1000px;'><h3 style='color: #000000;'>LOGO圖片</h3><img  src='cid:{res.ContentId}' width='60%'><h3 style='color: #000000;'>廠商圖片</h3><img src='cid:{res2.ContentId}' width='80%'><h3 style='color: #000000;'>攤位描述</h3><p>{bdes}</p></div><h2>謝謝您的申請，所有資料均為人工審核，審核完畢後會另行通知</h2>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            alternateView.LinkedResources.Add(res2);
            return alternateView;
        }

        //get the manufactures of applies 找一個廠商的所有審核 傳入Mf_id
        [HttpPost]
        public IActionResult getMfApplies([FromBody] GetIdClassModel idClass)
        {
            var applies = _woowocontext.Appliesses.Where(m => m.MfId == idClass.Mf_id);
            return Json(applies);
        }

        //get the manufactures  找一個展覽 傳入Mf_id
        [HttpPost]
        public IActionResult GetTheManufactures([FromBody] GetIdClassModel idClass)
        {
            var theManufactures = _woowocontext.Manufacturesses.Where(m => m.ManufactureId == idClass.Mf_id);
            return Json(theManufactures);
        }

        //get the exhibition of booths 取得一個展覽的所有攤位 傳入Ex_id
        [HttpPost]
        public IActionResult GetAllBooth([FromBody] GetIdClassModel idClass)
        {
            var booths = _woowocontext.BoothMapsses.Where(m => m.EId == idClass.Ex_id && m.BoothState == 0);
            return Json(booths);
        }
        //抓廠商申請跟展覽名稱
        [HttpPost]
            public IActionResult getMfAppliesAndExhibitions([FromBody] GetIdClassModel idClass)
        {

            var applies = _woowocontext.Appliesses.Where(m => m.MfId == idClass.Mf_id).ToList();



            List<Exhibitionss> exhibitions = new List<Exhibitionss>();
            if (applies != null)
            {
                foreach (var apply in applies)
                {
                    var a = _woowocontext.Exhibitionsses.First(ex => ex.ExhibitId == apply.EId);
                   if (a.ExhibitId != null)
                    {
                    exhibitions.Add(a);
                    }
                }
                
                GetMfsApplyAndExName getMfsApplyAndExName = new GetMfsApplyAndExName() 
                { 
                    exhibition = exhibitions,
                    appliess = applies,
                };
                 
                return Json(getMfsApplyAndExName);
                
            }

            return Json(applies);
        }

    }
}
