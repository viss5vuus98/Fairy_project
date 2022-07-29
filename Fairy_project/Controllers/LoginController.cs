using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Fairy_project.Models;
using Fairy_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Fairy_project.Controllers
{
    public class LoginController : Controller
    {

        private woowoContext _context;
        public LoginController(woowoContext context)
        {
            _context = context;
        }

        public Permissionss GetMember(string uid, string pwd)
        {
            IList<Permissionss> members = new List<Permissionss>();
            var member = _context.Permissionsses.FirstOrDefault(m => m.Account == uid && m.Password == pwd);
            return member;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MemberCreateAcc(LoginViewModels mem)
        {

            if (mem.permissions != null)
            {
                try
                {
                    _context.Permissionsses.Add(mem.permissions);
                    _context.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "會員新增失敗，帳號可能重複";
                }
            }
            if (mem.member != null)
            {
                try
                {
                    _context.Membersses.Add(mem.member);
                    _context.SaveChanges();
                    TempData["Success"] = "會員新增成功";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "會員新增失敗，帳號可能重複";
                }
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult ManufacturesCreateAcc(LoginViewModels mem)
        {
            
            if (mem.permissions != null)
            {
                try
                {
                    _context.Permissionsses.Add(mem.permissions);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    TempData["Error"] = "會員新增失敗，帳號可能重複";
                }
            }
            if (mem.manufactures != null)
            {
                try
                {
                    _context.Manufacturesses.Add(mem.manufactures);
                    _context.SaveChanges();
                    TempData["Success"] = "廠商新增成功";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "廠商新增失敗，帳號可能重複";
                }
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Index(LoginViewModels mem, string uid, string pwd)
        {
            var member = _context.Permissionsses.FirstOrDefault(m => m.Account == uid && m.Password == pwd);
            //var account = member.account;
            if (member != null)
            {
                string permissions = "";
                switch (member.PermissionsLv)
                {
                    case 1:
                        permissions = "Home";
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
                       new Claim(ClaimTypes.Name, member.Account),
                       new Claim(ClaimValueTypes.Email, "123"),
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
                var account = member.Account;
                if (permissions == "Home")
                {
                    var person = _context.Membersses.FirstOrDefault(m => m.MemberAc == account);
                    TempData["MemberId"] = person.MemberId;
                    //HttpContext.Session.SetString("MemberId", person.MemberId.ToString());
                    //ViewBag.id = HttpContext.Session.GetString("MemberId");
                }
                else if (permissions == "Manufacturer")
                {
                    var person = _context.Manufacturesses.FirstOrDefault(m => m.ManufactureAcc == account);
                    TempData["manufactureId"] = person.ManufactureId;
                }
               
                return RedirectToAction("Index", permissions);
            }
            TempData["Error"] = "帳密錯誤，請重新檢查";
            return View();
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
