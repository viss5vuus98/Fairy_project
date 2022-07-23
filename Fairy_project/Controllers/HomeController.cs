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

    public IActionResult exhibitionSearch()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}

