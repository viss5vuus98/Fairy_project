using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fairy_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Fairy_project.ViewModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Fairy_project.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly woowoContext _context;
    private readonly woowoContext _woocontext;
    private readonly string _path;

    public HomeController(ILogger<HomeController> logger, woowoContext woowoContext, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _context = woowoContext;
        _path = $"{hostEnvironment.WebRootPath}\\images";
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    //[Route("Home/exhibitionDetail/{exhibitId}")]
    //public IActionResult exhibitionDetail(string exhibitId)
    //{

    //    var id = Convert.ToInt32(exhibitId);
    //    eDrtailViewModel eDrtailViewModel = new eDrtailViewModel()
    //    {
    //        Exhibition = _context.Exhibitionsses.FirstOrDefault(m => m.ExhibitId == id),
    //    };


    //    //var theExhibit =  _context.exhibitions.FirstOrDefault(m => m.exhibitId == id);

    //    if (eDrtailViewModel == null)
    //    {
    //        Console.WriteLine("NULLLLLLLLLLLLLLL");
    //        return View();
    //    }
    //    //IList<eDrtailViewModel> manufactures = _context.boothMaps.OrderBy(m => m).Take(3);
    //    return View(eDrtailViewModel);
    //}

    public ActionResult GetData()
    {
        var theExhibit = _context.Exhibitionsses.OrderBy(m => m.ExhibitId);
        var theManufactures = _context.Manufacturesses.OrderBy(m => m.ManufactureId);

        return Json(theExhibit);
    }

    [HttpGet, Route("GetManufactures")]
  /*  public IActionResult GetManufactures()
    {
   //     var theManufactures = _woocontext.Manufacturesses.OrderBy(m => m.ManufactureId);
   //     return Json(theManufactures);
    }*/

    [HttpGet]
    public IActionResult getViewMode()
    {
        invide invide = new invide()
        {
            Exhibitionsses = _context.Exhibitionsses.FirstOrDefault(m => m.ExhibitId == 34) ?? new Exhibitionss(),
            Manufactures = _context.Manufacturesses.Where(m => m.ManufactureId > 0).ToList()
        };
        return Json(invide);
    }

    public IActionResult exhibitionSearch()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //get Member
    [HttpGet]
    public IActionResult getMember(int id)
    {
        var theMember = _context.Membersses.FirstOrDefault(m => m.MemberId == id) ?? new Memberss();
        return Json(theMember);
    }

    //updata Member


    //get All exhibition
    [HttpGet]
    public IActionResult getAllExhibition()
    {
        DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        var exhibitions = _context.Exhibitionsses.Where(m => m.Datefrom > dtToday);
        return Json(exhibitions);
    }

    //post id for the exhibition
    [HttpPost]
    public IActionResult getTheExhibition([FromBody] GetIdClassModel idClass)
    {
        //Console.WriteLine(idClass.Ex_id + "-------------------");
        //var id = Convert.ToInt32(idClass.Ex_id);
        var exhibition = _context.Exhibitionsses.FirstOrDefault(m => m.ExhibitId == idClass.Ex_id);
        return Json(exhibition);
    }

    //get the exhibition of applies

    [HttpPost]
    public IActionResult getExApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Appliesses.Where(m => m.EId == idClass.Ex_id);
        return Json(applies);
    }

    //create the exhibition of applies
    [HttpPost]
    public bool createApplies([FromBody]Appliess apply)
    {
        _context.Appliesses.Add(apply);
        _context.SaveChanges();
        return true;
    }

    //get the manufactures of applies
    [HttpPost]
    public IActionResult getMfApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Appliesses.Where(m => m.MfId == idClass.Mf_id);
        return Json(applies);
    }

    //get the manufactures 
    [HttpPost]
    public IActionResult GetTheManufactures([FromBody] GetIdClassModel idClass)
    {
        var theManufactures = _context.Manufacturesses.Where(m => m.ManufactureId == idClass.Mf_id);
        return Json(theManufactures);
    }

    //search exhibition keyword
    [HttpPost]
    public IActionResult searchKeyWord([FromBody] KeyWord keyWord)
    {
        Console.WriteLine(keyWord.ex_keyWord + "1111111111111");
        string key = Regex.Replace(keyWord.ex_keyWord, @"\s", "");
        var exhibitions = _context.Exhibitionsses.Where(m => m.ExhibitName.Contains(key) && m.ExhibitStatus == 1);
        return Json(exhibitions);
    }

    //search exhibition date
    [HttpPost]
    public IActionResult searchDate([FromBody] SearchDate searchDate)
    {
        DateTime dtStart = Convert.ToDateTime(searchDate.dateStart);
        DateTime dtEnd = Convert.ToDateTime(searchDate.dateEnd);
        var exhibitions = _context.Exhibitionsses.Where(m => m.Dateto > dtStart && m.Datefrom < dtEnd && m.ExhibitStatus == 1);
        return Json(exhibitions);
    }

    //radom tickets VerificationCode
    [HttpPost]
    public IActionResult getVerificationCode([FromBody] GetIdClassModel idClass)
    {
        string VfCode = "";
        string dt = DateTime.Now.ToString("ddMMyyyyHHmmss");
        string id = idClass.Ex_id.ToString();
        VfCode += dt;
        VfCode += id;
        Random radom = new Random();
        for (int i = 0; i < 5; i++)
        {
            int radomNum = radom.Next(97, 122);
            string vfStr = ((char)radomNum).ToString();
            VfCode += vfStr;
        }
        //ToDo：修改Tickets資料表中的驗證碼欄位
        //ToDO: 前端產生QR_code
        return Json(VfCode);
    }

    public IActionResult shoppingcart()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> shoppingcart(string i)
    {
        if (string.IsNullOrEmpty(i))
        {
            return RedirectToAction("exhibitionDetail");
        }
        else
        {
            //Console.WriteLine(i);
            var id = i.Split("|").SkipLast(1).ToArray();
            //Console.WriteLine(id.Length);
            List<shoppingcartViewModel> exhibitions = new List<shoppingcartViewModel>();

            for (int a = 0; a < id.Length; a++)
            {
                int exid = Convert.ToInt32(id[a]);
                Console.WriteLine(exid);
                shoppingcartViewModel model = new shoppingcartViewModel()
                {
                    exhibitions = await _context.Exhibitionsses.FirstOrDefaultAsync(m => m.ExhibitId == exid),
                    //ticket = await _context.tickets.Where(m => m.e_Id == exid).FirstOrDefaultAsync(),
                };
                exhibitions.Add(model);
            }
            //Console.WriteLine(exhibitions[1].exhibition.datefrom);
            return Json(exhibitions);
        }
    }

    [HttpPost]
    public IActionResult clearCart([Formbody] List<TicketRoot> obj)
    {
        for (int i = 0; i < obj.Count; i++)
        {
            _context.Ticketsses.Add(obj[i].ticket);
        }
        _context.SaveChanges();
        return Json(obj);
    }

}

