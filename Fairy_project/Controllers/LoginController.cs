using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Fairy_project.Models;

namespace Fairy_project.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //public IActionResult Index(string uid, string pwd)
        //{
            

        //    if (member != null)
        //    {
        //        IList<Claim> claims = new List<Claim> {
        //               new Claim(ClaimTypes.Name, member.Uid),
        //               new Claim(ClaimTypes.Role, member.Role)
        //        };

        //        var claimsIndentity = new ClaimsIdentity(claims,
        //            CookieAuthenticationDefaults.AuthenticationScheme);
        //        var authProperties = new AuthenticationProperties { IsPersistent = true };

        //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIndentity),
        //            authProperties);
        //        return RedirectToAction("Index", member.Role);
        //    }
        //    ViewBag.Show = "帳密錯誤";
        //    return View();
        //}

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
