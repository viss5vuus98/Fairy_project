using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Fairy_project.Models;


namespace Fairy_project.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
