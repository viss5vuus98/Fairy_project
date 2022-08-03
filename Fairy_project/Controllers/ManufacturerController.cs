using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;

namespace Fairy_project.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly woowoContext _woowocontext;
        public ManufacturerController(woowoContext woowocontext)
        {
            _woowocontext = woowocontext;
        }
      //  [Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult IndexOld()
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
            ViewBag.BId = boothId;
            ViewBag.EId = e_Id;
            ViewBag.MId = mid;
            return View();
        }
        public IActionResult StandProcess()
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
        public bool createApplies([FromBody] Appliess apply)
        {
            _woowocontext.Appliesses.Add(apply);
            _woowocontext.SaveChanges();
            return true;
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
