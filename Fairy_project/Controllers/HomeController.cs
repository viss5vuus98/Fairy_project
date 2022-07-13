using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fairy_project.Models;
//using System.Data.Entity; 錯誤的async
using Microsoft.EntityFrameworkCore;

namespace Fairy_project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ServerContext _context;
    private string _path;

    public HomeController(ILogger<HomeController> logger, ServerContext context, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _context = context;
        //_path = $"{hostEnvironment.WebRootPath}\\"
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    //exhibitionDetail
    [Route("/Home/exhibitionDetail/{exhibitId}")]
    public async Task<IActionResult> exhibitionDetail(int? exhibitId)
    {
        var theExhibition = await _context.Exhibitions.FirstOrDefaultAsync(m => m.exhibitId == Convert.ToInt32(exhibitId));
        HttpClient
        return View(theExhibition);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

