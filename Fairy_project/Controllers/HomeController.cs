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

namespace Fairy_project.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly ServerContext _context;
    private readonly string _path;

    public HomeController(ILogger<HomeController> logger, ServerContext context, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _context = context;
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
    [Route("Home/exhibitionDetail/{exhibitId}")]
    public IActionResult exhibitionDetail(string exhibitId)
    {

        var id = Convert.ToInt32(exhibitId);
        eDrtailViewModel eDrtailViewModel = new eDrtailViewModel()
        {
            Exhibition = _context.exhibitions.FirstOrDefault(m => m.exhibitId == id),
        };


        //var theExhibit =  _context.exhibitions.FirstOrDefault(m => m.exhibitId == id);

        if (eDrtailViewModel == null)
        {
            Console.WriteLine("NULLLLLLLLLLLLLLL");
            return View();
        }
        //IList<eDrtailViewModel> manufactures = _context.boothMaps.OrderBy(m => m).Take(3);
        return View(eDrtailViewModel);
    }

    public ActionResult GetData()
    {
        var theExhibit = _context.exhibitions.OrderBy(m => m.exhibitId);
        var theManufactures = _context.manufactures.OrderBy(m => m.manufactureId);

        return Json(theExhibit);
    }

    public IActionResult GetManufactures()
    {
        var theManufactures = _context.manufactures.OrderBy(m => m.manufactureId);
        return Json(theManufactures);
    }

    [HttpGet]
    public IActionResult getViewMode()
    {
        invide invide = new invide()
        {
            Exhibition = _context.exhibitions.FirstOrDefault(m => m.exhibitId == 34) ?? new Exhibition(),
            Manufactures = _context.manufactures.Where(m => m.manufactureId > 0).ToList()
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
        var theMember = _context.members.FirstOrDefault(m => m.memberId == id) ?? new Member();
        return Json(theMember);
    }

    //updata Member


    //get All exhibition
    [HttpGet]
    public IActionResult getAllExhibition()
    {
        DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        var exhibitions = _context.exhibitions.Where(m => m.datefrom > dtToday);
        return Json(exhibitions);
    }

    //post id for the exhibition
    [HttpPost]
    public IActionResult getTheExhibition([FromBody] GetIdClassModel idClass)
    {
        //Console.WriteLine(idClass.Ex_id + "-------------------");
        //var id = Convert.ToInt32(idClass.Ex_id);
        var exhibition = _context.exhibitions.FirstOrDefault(m => m.exhibitId == idClass.Ex_id);
        return Json(exhibition);
    }

    //get the exhibition of applies

    [HttpPost]
    public IActionResult getExApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Applies.Where(m => m.e_Id == idClass.Ex_id);
        return Json(applies);
    }

    //create the exhibition of applies
    [HttpPost]
    public bool createApplies([FromBody]Apply apply)
    {
        _context.Applies.Add(apply);
        _context.SaveChanges();
        return true;
    }

    //get the manufactures of applies
    [HttpPost]
    public IActionResult getMfApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Applies.Where(m => m.mf_Id == idClass.Mf_id);
        return Json(applies);
    }

    //get the manufactures 
    [HttpPost]
    public IActionResult GetTheManufactures([FromBody] GetIdClassModel idClass)
    {
        var theManufactures = _context.manufactures.Where(m => m.manufactureId == idClass.Mf_id);
        return Json(theManufactures);
    }

    //search exhibition keyword
    [HttpPost]
    public IActionResult searchKeyWord([FromBody] KeyWord keyWord)
    {
        Console.WriteLine(keyWord.ex_keyWord + "1111111111111");
        string key = Regex.Replace(keyWord.ex_keyWord, @"\s", "");
        var exhibitions = _context.exhibitions.Where(m => m.exhibitName.Contains(key) && m.exhibitStatus == 1);
        return Json(exhibitions);
    }

    //search exhibition date
    [HttpPost]
    public IActionResult searchDate([FromBody] SearchDate searchDate)
    {
        DateTime dtStart = Convert.ToDateTime(searchDate.dateStart);
        DateTime dtEnd = Convert.ToDateTime(searchDate.dateEnd);
        var exhibitions = _context.exhibitions.Where(m => m.dateto > dtStart && m.datefrom < dtEnd && m.exhibitStatus == 1);
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
}

