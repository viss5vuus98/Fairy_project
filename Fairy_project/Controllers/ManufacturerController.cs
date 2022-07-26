using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;

namespace Fairy_project.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly woowoContext _woowocontext;
        private readonly ServerContext _context;
        public ManufacturerController(ServerContext context , woowoContext woowocontext)
        {
            _context = context;
            _woowocontext = woowocontext;
        }
      //  [Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Policy()
        {
            return View();
        }

        public IActionResult ApplyStand()
        {
            return View();
        }
        public IActionResult StandProcess()
        {
            return View();
        }

        //get All exhibition 找所有展覽
        [HttpGet]
     /*   public IActionResult getAllExhibition()
        {
           // DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
      //      var exhibitions = _woowocontext.Exhibitionsses.Where(m => m.ExhibitStatus == 2);
            return Json(exhibitions);
        }*/

        //post id for the exhibition 傳入Ex_id 找展覽
        [HttpPost]
        public IActionResult getTheExhibition([FromBody] GetIdClassModel idClass)
        {
            //Console.WriteLine(idClass.Ex_id + "-------------------");
            //var id = Convert.ToInt32(idClass.Ex_id);
            var exhibition = _context.exhibitions.FirstOrDefault(m => m.exhibitId == idClass.Ex_id);
            return Json(exhibition);
        }

        //get the exhibition of applies 找一個展覽的所有審核 傳入Ex_id

        [HttpPost]
        public IActionResult getExApplies([FromBody] GetIdClassModel idClass)
        {
            var applies = _context.Applies.Where(m => m.e_Id == idClass.Ex_id);
            return Json(applies);
        }

        //create the exhibition of applies 新增申請 傳入applies內的物件
        [HttpPost]
        public bool createApplies([FromBody] Apply apply)
        {
            _context.Applies.Add(apply);
            _context.SaveChanges();
            return true;
        }

        //get the manufactures of applies 找一個廠商的所有審核 傳入Mf_id
        [HttpPost]
        public IActionResult getMfApplies([FromBody] GetIdClassModel idClass)
        {
            var applies = _context.Applies.Where(m => m.mf_Id == idClass.Mf_id);
            return Json(applies);
        }

        //get the manufactures  找一個展覽 傳入Mf_id
        [HttpPost]
        public IActionResult GetTheManufactures([FromBody] GetIdClassModel idClass)
        {
            var theManufactures = _context.manufactures.Where(m => m.manufactureId == idClass.Mf_id);
            return Json(theManufactures);
        }

        //get the exhibition of booths 取得一個展覽的所有攤位 傳入Ex_id
        [HttpPost]
        public IActionResult GetAllBooth([FromBody] GetIdClassModel idClass)
        {
            var booths = _context.boothMaps.Where(m => m.e_Id == idClass.Ex_id);
            return Json(booths);
        }
    }
}
