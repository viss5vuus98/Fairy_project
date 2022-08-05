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

namespace Fairy_project.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly woowoContext _woowocontext;
        public ManufacturerController(woowoContext woowocontext)
        {
            _woowocontext = woowocontext;
        }
        [Authorize(Roles = "Manufacturer")]
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


        //create the exhibition of applies 新增申請 傳入applies內的物件
        [HttpPost]
        public async Task<IActionResult> createApplies(CreatAppliessViewModels apply )
        {
            string img_dir = @$"wwwroot/images/";
      
            Random rnd = new Random();

            Appliess appliess = new Appliess();

            appliess.MfId = Convert.ToInt32(apply.MfId);
            appliess.EId = Convert.ToInt32(apply.EId);
            appliess.BoothNumber = Convert.ToInt32(apply.BoothNumber);
            appliess.MfDescription = apply.MfDescription;
            appliess.CheckState = 0;
            if (apply.MfPImg != null)
            {
                string pimgName = DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(1000, 10000).ToString() + Path.GetExtension(apply.MfPImg.FileName);
                appliess.MfPImg= pimgName;
                using (var stream = System.IO.File.Create(img_dir + pimgName))
                {
                    await apply.MfPImg.CopyToAsync(stream);
                }
            }
            if (apply.MfLogo != null)
            {
               string LimgName = DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(1000, 10000).ToString() + Path.GetExtension(apply.MfLogo.FileName);
                appliess.MfLogo= LimgName;
                using (var stream = System.IO.File.Create(img_dir + LimgName))
                {
                    await apply.MfLogo.CopyToAsync(stream);
                }
            }


            


            _woowocontext.Appliesses.Add(appliess);
            _woowocontext.SaveChanges();
            return Redirect("StandProcess");

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
            var booths = _woowocontext.BoothMapsses.Where(m => m.EId == idClass.Ex_id);
            return Json(booths);
        }
    }
}
