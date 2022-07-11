using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Fairy_project.Models;

namespace Fairy_project.Controllers
{
    public class LoginController : Controller
    {
        public class Member
        {
            public string Uid { get; set; }
            public string Pwd { get; set; }
            public string Role { get; set; }
        }

        public class MemberList
        {
            public Member GetMember(string uid, string pwd)
            {
                IList<Member> members = new List<Member>();
                members.Add(new Member { Uid = "jasper", Pwd = "123", Role = "Admin" });
                members.Add(new Member { Uid = "mary", Pwd = "123", Role = "Member" });
                members.Add(new Member { Uid = "anita", Pwd = "123", Role = "Member" });
                var member = members.FirstOrDefault(m => m.Uid == uid && m.Pwd == pwd);
                return member;
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string uid, string pwd)
        {
            var member = new MemberList().GetMember(uid, pwd);

            if (member != null)
            {
                IList<Claim> claims = new List<Claim> {
                       new Claim(ClaimTypes.Name, member.Uid),
                       new Claim(ClaimTypes.Role, member.Role)
                };

                var claimsIndentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIndentity),
                    authProperties);
                return RedirectToAction("Index", member.Role);
            }
            ViewBag.Show = "帳密錯誤";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
