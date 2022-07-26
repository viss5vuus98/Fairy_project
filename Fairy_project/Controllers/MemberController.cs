using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;
using Fairy_project.ViewModels;
using NuGet.Packaging;

namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Home")]
    [ApiController]
    [Route("api/Member/Post")]
    public class MemberController : Controller
    {
        private readonly woowoContext _context;

        public MemberController(woowoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // load Member's tickets and exhibition
        [HttpPost, Route("getTicketsss")]
        public IActionResult getTicketsss([FromBody] GetIdClassModel idClass)
        {
            var tickets = _context.Ticketsses.Where(m => m.MId == idClass.Mf_id).ToList();

            List<Exhibitionss> exhibitions = new List<Exhibitionss>();
            if (tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    exhibitions.AddRange(_context.Exhibitionsses.Where(ex => ex.ExhibitId == ticket.EId).ToList());
                }
                TicketsViewModel ticketsViewModel = new TicketsViewModel()
                {
                    exhibition = exhibitions,
                    tickets = tickets,
                };
                return Json(ticketsViewModel);
            }

            return Json(tickets);
        }

        //[HttpGet]
        //public IActionResult getViewMode()
        //{
        //    invide invide = new invide()
        //    {
        //        Exhibition = _context.exhibitions.FirstOrDefault(m => m.exhibitId == 34) ?? new Exhibition(),
        //        Manufactures = _context.manufactures.Where(m => m.manufactureId > 0).ToList()
        //    };
        //    return Json(invide);
        //}

    }
}
