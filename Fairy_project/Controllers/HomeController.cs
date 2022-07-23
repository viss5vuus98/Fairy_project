using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fairy_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Fairy_project.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            Console.WriteLine(i);
            var id = i.Split("|").SkipLast(1).ToArray();
            Console.WriteLine(id.Length);
            List<shoppingcartViewModel> exhibitions = new List<shoppingcartViewModel>();

            for (int a = 0; a < id.Length; a++)
            {
                int exid = Convert.ToInt32(id[a]);
                Console.WriteLine(exid);
                shoppingcartViewModel model = new shoppingcartViewModel()
                {
                    exhibition = await _context.exhibitions.FirstOrDefaultAsync(m => m.exhibitId == exid),
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
        //for (int i = 0; i < obj.Count; i++)
        //{
        //    _context.tickets.Add(obj[i].ticket);
        //}
        //_context.SaveChanges();
        return Json(obj);
    }
}

