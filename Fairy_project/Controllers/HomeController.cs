using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fairy_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Fairy_project.ViewModels;
using System.Collections.Generic;

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
    public async Task<IActionResult> exhibitionDetail(string exhibitId)
    {
        int id = Convert.ToInt32(exhibitId);
        var theExhibit = await _context.exhibitions.FirstOrDefaultAsync(m => m.exhibitId == id);
        if (theExhibit == null)
        {
            Console.WriteLine("NULLLLLLLLLLLLLLL");
            return View();
        }
        //IList<eDrtailViewModel> manufactures = (IList<eDrtailViewModel>)_context.boothMaps.OrderBy(m => m).Take(3);
        return View(theExhibit);
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



    public ActionResult shoppingcart(string exid)
    {
        Console.WriteLine("---------------------" + exid);
        //var id = data.Split('|');
        ////List<Exhibition> exhibitions = new List<Exhibition>();
        //for (int i = 0; i < id.Length; i++)
        //{
        //    int exid = Convert.ToInt32(id[i]);
        //    var exhibitbuy = _context.exhibitions.Where(m => m.exhibitId == exid).Select(m => new { m.exhibitName, m.exhibit_T_img, m.ticket_Price }).ToList();
        //    Console.WriteLine(exhibitbuy);
        //}
        return Content(exid);
    }


}

