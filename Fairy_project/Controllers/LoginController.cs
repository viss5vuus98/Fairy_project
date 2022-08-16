using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Fairy_project.Models;
using Fairy_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Google.Apis.Auth;

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


        /// <summary>
        /// 驗證 Google 登入授權
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ValidGoogleLogin()
        {
            string? formCredential = Request.Form["credential"]; //回傳憑證
            string? formToken = Request.Form["g_csrf_token"]; //回傳令牌
            string? cookiesToken = Request.Cookies["g_csrf_token"]; //Cookie 令牌

            // 驗證 Google Token
            GoogleJsonWebSignature.Payload? payload = VerifyGoogleToken(formCredential, formToken, cookiesToken).Result;
            if (payload == null)
            {
                // 驗證失敗
                ViewData["Msg"] = "驗證 Google 授權失敗";
            }
            else
            {
                //驗證成功，取使用者資訊內容
                ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
                ViewData["Msg"] += "Email:" + payload.Email + "<br>";
                ViewData["Msg"] += "Name:" + payload.Name + "<br>";
                ViewData["Msg"] += "Picture:" + payload.Picture;
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 驗證 Google Token
        /// </summary>
        /// <param name="formCredential"></param>
        /// <param name="formToken"></param>
        /// <param name="cookiesToken"></param>
        /// <returns></returns>
        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
        {
            // 檢查空值
            if (formCredential == null || formToken == null && cookiesToken == null)
            {
                return null;
            }

            GoogleJsonWebSignature.Payload? payload;
            try
            {
                // 驗證 token
                if (formToken != cookiesToken)
                {
                    return null;
                }

                // 驗證憑證
                IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
                string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { GoogleApiClientId }
                };
                payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
                if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
                {
                    return null;
                }
                if (payload.ExpirationTimeSeconds == null)
                {
                    return null;
                }
                else
                {
                    DateTime now = DateTime.Now.ToUniversalTime();
                    DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
                    if (now > expiration)
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return payload;
        }
    }
}
