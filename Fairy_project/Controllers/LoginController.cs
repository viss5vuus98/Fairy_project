using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Fairy_project.Models;
using Fairy_project.ViewModels;

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
        public IActionResult MemberCreateAcc(LoginViewModels mem)
        {
            Console.WriteLine("1111111111" + mem.permissions);

            if (mem.permissions != null)
            {
                try
                {
                    _context.Permissions.Add(mem.permissions);
                    _context.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("perMisssssss2222222222");
                }
            }
            if (mem.member != null)
            {
                try
                {
                    _context.members.Add(mem.member);
                    _context.SaveChanges();
                    TempData["Success"] = "會員新增成功";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "會員新增失敗，帳號可能重複";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Index(LoginViewModels mem, string uid, string pwd)
        {
            //MemberCreate(mem);
            //ManufacturerCreate(mem);
            PermissionsCreate(mem);
            var member = _context.Permissions.FirstOrDefault(m => m.account == uid && m.password == pwd);
            //var account = member.account;
            if (member != null)
            {
                string permissions = "";
                switch (member.permissionsLv)
                {
                    case 1:
                        permissions = "Member";
                        break;
                    case 2:
                        permissions = "Manufacturer";
                        break;
                    case 3:
                        permissions = "Admin";
                        break;
                    default:
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
                TempData["Success"] = "登入成功";
                return RedirectToAction("Index", permissions);
            }
            TempData["Error"] = "帳密錯誤，請重新檢查";
            return View();
        }

        private void MemberCreate(LoginViewModels mem)
        {
            if (mem.member != null)
            {
                try
                {
                    _context.members.Add(mem.member);
                    _context.SaveChanges();
                    TempData["Success"] = "會員新增成功";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "會員新增失敗，帳號可能重複";
                }
            }
        }

        private void ManufacturerCreate(LoginViewModels men)
        {
            if (men.manufactures != null)
            {
                try
                {
                    _context.manufactures.Add(men.manufactures);
                    _context.SaveChanges();
                    TempData["Success"] = "廠商新增成功";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "廠商新增失敗，帳號可能重複";
                }
            }
        }

        private void PermissionsCreate(LoginViewModels men)
        {
            if (men.permissions != null)
            {
                try
                {
                    _context.Permissions.Add(men.permissions);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult NoAuthorization()
        {
            return View();
        }
        
    }
}
