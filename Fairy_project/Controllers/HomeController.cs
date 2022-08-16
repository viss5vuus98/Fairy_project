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
    [Route("Home/exhibitionDetail/{id}")]
    public IActionResult exhibitionDetail(string id)
    {
        var exId = Convert.ToInt32(id);

        eDrtailViewModel eDrtailVM = new eDrtailViewModel()
        {
            Exhibition = _context.Exhibitionsses.First(e => e.ExhibitId == exId)
        };

        //eDrtailViewModel eDrtailViewModel = new eDrtailViewModel()
        //{
        //var exhibition = _context.Exhibitionsses.First(m => m.ExhibitId == id);
        //};


        //var theExhibit =  _context.exhibitions.FirstOrDefault(m => m.exhibitId == id);

        //if (eDrtailViewModel == null)
        //{
        //    Console.WriteLine("NULLLLLLLLLLLLLLL");
        //    return View();
        //}
        //IList<eDrtailViewModel> manufactures = _context.boothMaps.OrderBy(m => m).Take(3);
        return View(eDrtailVM);
    }

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
            Exhibitionsses = _context.Exhibitionsses.Where(m => m.ExhibitId > 1).ToList(),
            Manufactures = _context.Ticketsses.Where(m => m.MId == 6).ToList()
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
        var exhibitions = _context.Exhibitionsses.Where(m => m.Datefrom > dtToday).Take(6);
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

    [HttpPost]
    public IActionResult GetInvideManufactures([FromBody] GetIdClassModel idClass)
    {
        var booths = _context.BoothMapsses.Where(m => m.EId == idClass.Ex_id && m.MfId != null).Distinct().ToList()??new List<BoothMapss>();
        for (int i = 0; i < booths.Count; i++)
        {
            var apply = _context.Appliesses.First(a => a.EId == idClass.Ex_id && a.MfId == booths[i].MfId);
        }
        return Json(booths);
    }

    //search exhibition keyword
    //[HttpPost]
    //public IActionResult searchKeyWord([FromBody] KeyWord keyWord)
    //{
    //    string key = Regex.Replace(keyWord.ex_keyWord, @"\s", "");
    //    var exhibitions = _context.Exhibitionsses.Where(m => m.ExhibitName.Contains(key) && 1 < m.ExhibitStatus && m.ExhibitStatus < 4);
    //    return Json(exhibitions);
    //}

    public IActionResult search(int id = 1)
    {
        var exhibitions = _context.Exhibitionsses.Where(m => 1 < m.ExhibitStatus && m.ExhibitStatus < 4).OrderBy(m => m.ExhibitId).ToList();
        IList<Exhibitionss> products = null;

        if (id == 1)
        {
            products = exhibitions.OrderBy(x => x.TicketPrice).ToList();
            return View(products);
        }
        else if (id == 2)
        {
            products = exhibitions.OrderByDescending(x => x.TicketPrice).ToList();
            return View(products);
        }
        return View(exhibitions);
    }

    [HttpPost]
    public IActionResult search(string txtKeyword,string dtStart, string dtEnd)
    {
        //if (string.IsNullOrEmpty(txtKeyword))
        //{
        //    ViewBag.dtStart = dtStart;
        //    ViewBag.dtEnd = dtEnd;
        //    DateTime dateStart = Convert.ToDateTime(dtStart);
        //    DateTime dateEnd = Convert.ToDateTime(dtEnd);
        //    var exhibitions = _context.Exhibitionsses.Where(m => m.Datefrom > dateStart && m.Dateto < dateEnd && 1 < m.ExhibitStatus && m.ExhibitStatus < 4);
        //    return View(exhibitions);
        //}
        //else
        //{
        if (string.IsNullOrEmpty(txtKeyword) || string.IsNullOrEmpty(dtStart) || string.IsNullOrEmpty(dtEnd))
        {
            var exhibition = _context.Exhibitionsses.Where(m => 1 < m.ExhibitStatus && m.ExhibitStatus < 4).OrderBy(m => m.ExhibitId).ToList();
            return View(exhibition);
        }
        else
        {       
            ViewBag.txtKeyword = txtKeyword;
            ViewBag.dtStart = dtStart;
            ViewBag.dtEnd = dtEnd;
            DateTime dateStart = Convert.ToDateTime(dtStart);
            DateTime dateEnd = Convert.ToDateTime(dtEnd);
            var exhibitions = _context.Exhibitionsses
            .Where(m => m.ExhibitName.Contains(txtKeyword) && m.Datefrom > dateStart && m.Dateto < dateEnd && 1 < m.ExhibitStatus && m.ExhibitStatus < 4)
            .OrderBy(m => m.ExhibitId)
            .ToList();
            return View(exhibitions);
        }
        //}
    }

    //search exhibition date
    //[HttpPost]
    //public IActionResult searchDate([FromBody] SearchDate searchDate)
    //{
    //    DateTime dtStart = Convert.ToDateTime(searchDate.dateStart);
    //    DateTime dtEnd = Convert.ToDateTime(searchDate.dateEnd);
    //    var exhibitions = _context.Exhibitionsses.Where(m => m.Dateto > dtStart && m.Datefrom < dtEnd && m.ExhibitStatus == 1);
    //    return Json(exhibitions);
    //}

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
                    //tickets = await _context.Ticketsses.Where(m => m.EId == exid).FirstOrDefaultAsync(),
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

    //postQrcode

    [HttpPost]
    public IActionResult postQrCode([FromBody] QrCode ticket)
    {
        var ticketCoent = _context.Ticketsses.FirstOrDefault(t => t.VerificationCode == ticket.VerificationCode && t.Enterstate == 0)??new Ticketss();
        if(ticketCoent.VerificationCode != null)
        {
            var exId = ticketCoent.EId;
            if (exId == Convert.ToInt32(ticket.Ex_id))
            {
                ticketCoent.Enterstate = 1;
                ticketCoent.Entertime = DateTime.Now;
                _context.SaveChanges();
                return Json("成功進入");
            }
            else
            {
                return Json("展覽錯誤");
            }
        }else
        {
            return Json("無此票券");
        }
    }

    //postMfQrcode

    [HttpPost]
    public IActionResult postMfQrCode([FromBody] MfQrCode mfQrCode)
    {
        int exId = Convert.ToInt32(mfQrCode.ex_id);
        int boothNum = Convert.ToInt32(mfQrCode.boothNum);
        int mfId = Convert.ToInt32(mfQrCode.mf_id);
        IList<BoothMapss> booths = _context.BoothMapsses.Where(b => b.EId == exId && b.BoothState == 2).ToList();
        if (booths.Count > 0)
        {
            var TheBooth = booths.FirstOrDefault(b => b.BoothNumber == boothNum);
            if (TheBooth.MfId == mfId)
            {
                return Json("歡迎入場");
            }
            else
            {
                return Json("無效攤位");
            }
        }

        return Json("無效展覽");
    }
}

