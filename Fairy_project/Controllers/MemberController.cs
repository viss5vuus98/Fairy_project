using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;


namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Home")]
    [ApiController]
    [Route("api/Member/Post")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult getTickets([FromBody] GetIdClassModel getIdClass)
        //{
            
        //}

    }
}
