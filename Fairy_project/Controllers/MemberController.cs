using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;


namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Member")]
    [ApiController]
    [Route("api/Member/Post")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
