using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Fairy_project.Models;

namespace Fairy_project.Controllers
{
    public class LoginController : Controller
    {
        private ServerContext _context;

        public LoginController(ServerContext context)
        {
            _context = context;
        }

        public Permissions GetMember(string uid, string pwd)
        {
            IList<Permissions> members = new List<Permissions>();
            var member = _context.Permissions.FirstOrDefault(m => m.account == uid && m.password == pwd);
            return member;           
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string uid, string pwd)
        {
            var member = _context.Permissions.FirstOrDefault(m => m.account == uid && m.password == pwd);
            //var account = member.account;
            if (member != null)
            {
                string permissions = "";
                switch(member.permissionsLv)
                {
                    case 1: 
                        permissions="Member";
                        break;
                    case 2: 
                        permissions="mfu";
                        break;
                    case 3:
                        permissions = "Admin";
                        break;
                }
                //宣告身分識別
                //var memberid = _context.members.FirstOrDefault(m => m.memberAc == account);
                IList<Claim> claims = new List<Claim> {
                       new Claim(ClaimTypes.Name, member.account),
                       //new Claim(ClaimTypes.NameIdentifier, memberid.memberId.ToString()),
                       new Claim(ClaimTypes.Role, permissions)
                };

                var claimsIndentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIndentity),
                    authProperties);
                return RedirectToAction("Index", permissions);
            }
            ViewBag.Show = "帳號密碼錯誤";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
