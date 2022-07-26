using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;
using Fairy_project.ViewModels;

namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Home")]
    [ApiController]
    [Route("api/Member/Post")]
    public class MemberController : Controller
    {
        private readonly ServerContext _context;

        public MemberController(ServerContext context)
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
            var tickets = _context.tickets.Where(m => m.m_Id == idClass.Mf_id).ToList();

            List<Exhibition> exhibitions = new List<Exhibition>();
            if (tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    Console.WriteLine(ticket.e_Id);
                    exhibitions.AddRange(_context.exhibitions.Where(ex => ex.exhibitId == ticket.e_Id).ToList());
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
