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
                    Console.WriteLine(ticket.EId);
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

        //radom tickets VerificationCode
        [HttpPost, Route("getVerificationCode")]
        public IActionResult getVerificationCode([FromBody] GetIdClassModel idClass)
        {
            //產生QR_code
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
            var exhibition = _context.Exhibitionsses.FirstOrDefault(e => e.ExhibitId == idClass.Ex_id); ;
            QrcodeModal qrcodeModal = new QrcodeModal()
            {
                ExhibitName = exhibition.ExhibitName,
                Datefrom = exhibition.Datefrom,
                Dateto = exhibition.Dateto,
                VerificationCode = VfCode
            };
            //ToDo：修改Tickets資料表中的驗證碼欄位
            updateVfcode(VfCode, idClass.Mf_id);


            return Json(qrcodeModal);
        }

        [HttpPost, Route("updateVfcode")]
        public void updateVfcode(string vfCode, int orderNum)
        {
            var ticket = _context.Ticketsses.First(t => t.OrderNum == orderNum);
            if(ticket != null)
            {
                ticket.VerificationCode = vfCode;
                _context.SaveChanges();
            }
        }
    }
}
