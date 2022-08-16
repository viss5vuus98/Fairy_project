using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fairy_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Fairy_project.ViewModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using static Fairy_project.ViewModels.HomeViewModel;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Text.Json;
using Newtonsoft.Json;

namespace Fairy_project.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly woowoContext _context;
    private readonly string _path;

    public HomeController(ILogger<HomeController> logger, woowoContext woowoContext, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _context = woowoContext;
        _path = $"{hostEnvironment.WebRootPath}\\images";
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [Route("Home/exhibitionDetail/{id}")]
    public IActionResult exhibitionDetail(string id)
    {
        var exId = Convert.ToInt32(id);

        eDrtailViewModel eDrtailVM = new eDrtailViewModel()
        {
            Exhibition = _context.Exhibitionsses.First(e => e.ExhibitId == exId)
        };

        //eDrtailViewModel eDrtailViewModel = new eDrtailViewModel()
        //{
        //var exhibition = _context.Exhibitionsses.First(m => m.ExhibitId == id);
        //};


        //var theExhibit =  _context.exhibitions.FirstOrDefault(m => m.exhibitId == id);

        //if (eDrtailViewModel == null)
        //{
        //    Console.WriteLine("NULLLLLLLLLLLLLLL");
        //    return View();
        //}
        //IList<eDrtailViewModel> manufactures = _context.boothMaps.OrderBy(m => m).Take(3);
        return View(eDrtailVM);
    }

    public ActionResult GetData()
    {
        var theExhibit = _context.Exhibitionsses.OrderBy(m => m.ExhibitId);
        var theManufactures = _context.Manufacturesses.OrderBy(m => m.ManufactureId);

        return Json(theExhibit);
    }

    [HttpGet, Route("GetManufactures")]
  /*  public IActionResult GetManufactures()
    {
   //     var theManufactures = _woocontext.Manufacturesses.OrderBy(m => m.ManufactureId);
   //     return Json(theManufactures);
    }*/

    [HttpGet]
    public IActionResult getViewMode()
    {
        invide invide = new invide()
        {
            Exhibitionsses = _context.Exhibitionsses.Where(m => m.ExhibitId > 1).ToList(),
            Manufactures = _context.Ticketsses.Where(m => m.MId == 6).ToList()
        };
        return Json(invide);
    }

    public IActionResult exhibitionSearch()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //get Member
    [HttpGet]
    public IActionResult getMember(int id)
    {
        var theMember = _context.Membersses.FirstOrDefault(m => m.MemberId == id) ?? new Memberss();
        return Json(theMember);
    }

    //updata Member


    //get All exhibition
    [HttpGet]
    public IActionResult getAllExhibition()
    {
        DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        var exhibitions = _context.Exhibitionsses.Where(m => m.Datefrom > dtToday).Take(6);
        return Json(exhibitions);
    }

    //post id for the exhibition
    [HttpPost]
    public IActionResult getTheExhibition([FromBody] GetIdClassModel idClass)
    {
        //Console.WriteLine(idClass.Ex_id + "-------------------");
        //var id = Convert.ToInt32(idClass.Ex_id);
        var exhibition = _context.Exhibitionsses.FirstOrDefault(m => m.ExhibitId == idClass.Ex_id);
        return Json(exhibition);
    }

    //get the exhibition of applies

    [HttpPost]
    public IActionResult getExApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Appliesses.Where(m => m.EId == idClass.Ex_id);
        return Json(applies);
    }

    //create the exhibition of applies
    [HttpPost]
    public bool createApplies([FromBody]Appliess apply)
    {
        _context.Appliesses.Add(apply);
        _context.SaveChanges();
        return true;
    }

    //get the manufactures of applies
    [HttpPost]
    public IActionResult getMfApplies([FromBody] GetIdClassModel idClass)
    {
        var applies = _context.Appliesses.Where(m => m.MfId == idClass.Mf_id);
        return Json(applies);
    }

    //get the manufactures 
    [HttpPost]
    public IActionResult GetTheManufactures([FromBody] GetIdClassModel idClass)
    {
        var theManufactures = _context.Manufacturesses.Where(m => m.ManufactureId == idClass.Mf_id);
        return Json(theManufactures);
    }

    [HttpPost]
    public IActionResult GetInvideManufactures([FromBody] GetIdClassModel idClass)
    {
        var booths = _context.BoothMapsses.Where(m => m.EId == idClass.Ex_id && m.MfId != null).Distinct().ToList()??new List<BoothMapss>();
        for (int i = 0; i < booths.Count; i++)
        {
            var apply = _context.Appliesses.First(a => a.EId == idClass.Ex_id && a.MfId == booths[i].MfId);
        }
        return Json(booths);
    }

    //search exhibition keyword
    //[HttpPost]
    //public IActionResult searchKeyWord([FromBody] KeyWord keyWord)
    //{
    //    string key = Regex.Replace(keyWord.ex_keyWord, @"\s", "");
    //    var exhibitions = _context.Exhibitionsses.Where(m => m.ExhibitName.Contains(key) && 1 < m.ExhibitStatus && m.ExhibitStatus < 4);
    //    return Json(exhibitions);
    //}

    public IActionResult search(int id = 1)
    {
        var exhibitions = _context.Exhibitionsses.Where(m => 1 < m.ExhibitStatus && m.ExhibitStatus < 4).OrderBy(m => m.ExhibitId).ToList();
        IList<Exhibitionss> products = null;

        if (id == 1)
        {
            products = exhibitions.OrderBy(x => x.TicketPrice).ToList();
            return View(products);
        }
        else if (id == 2)
        {
            products = exhibitions.OrderByDescending(x => x.TicketPrice).ToList();
            return View(products);
        }
        return View(exhibitions);
    }

    [HttpPost]
    public IActionResult search(string txtKeyword,string dtStart, string dtEnd)
    {
        //if (string.IsNullOrEmpty(txtKeyword))
        //{
        //    ViewBag.dtStart = dtStart;
        //    ViewBag.dtEnd = dtEnd;
        //    DateTime dateStart = Convert.ToDateTime(dtStart);
        //    DateTime dateEnd = Convert.ToDateTime(dtEnd);
        //    var exhibitions = _context.Exhibitionsses.Where(m => m.Datefrom > dateStart && m.Dateto < dateEnd && 1 < m.ExhibitStatus && m.ExhibitStatus < 4);
        //    return View(exhibitions);
        //}
        //else
        //{
        if (string.IsNullOrEmpty(txtKeyword) || string.IsNullOrEmpty(dtStart) || string.IsNullOrEmpty(dtEnd))
        {
            var exhibition = _context.Exhibitionsses.Where(m => 1 < m.ExhibitStatus && m.ExhibitStatus < 4).OrderBy(m => m.ExhibitId).ToList();
            return View(exhibition);
        }
        else
        {       
            ViewBag.txtKeyword = txtKeyword;
            ViewBag.dtStart = dtStart;
            ViewBag.dtEnd = dtEnd;
            DateTime dateStart = Convert.ToDateTime(dtStart);
            DateTime dateEnd = Convert.ToDateTime(dtEnd);
            var exhibitions = _context.Exhibitionsses
            .Where(m => m.ExhibitName.Contains(txtKeyword) && m.Datefrom > dateStart && m.Dateto < dateEnd && 1 < m.ExhibitStatus && m.ExhibitStatus < 4)
            .OrderBy(m => m.ExhibitId)
            .ToList();
            return View(exhibitions);
        }
        //}
    }

    //search exhibition date
    //[HttpPost]
    //public IActionResult searchDate([FromBody] SearchDate searchDate)
    //{
    //    DateTime dtStart = Convert.ToDateTime(searchDate.dateStart);
    //    DateTime dtEnd = Convert.ToDateTime(searchDate.dateEnd);
    //    var exhibitions = _context.Exhibitionsses.Where(m => m.Dateto > dtStart && m.Datefrom < dtEnd && m.ExhibitStatus == 1);
    //    return Json(exhibitions);
    //}

    //radom tickets VerificationCode
    [HttpPost]
    public IActionResult getVerificationCode([FromBody] GetIdClassModel idClass)
    {
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
        //ToDo：修改Tickets資料表中的驗證碼欄位
        //ToDO: 前端產生QR_code
        return Json(VfCode);
    }

    public IActionResult shoppingcart()
    {
        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        // 產生測試資訊
        ViewData["MerchantID"] = Config.GetSection("MerchantID").Value;
        ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");  //訂單編號
        ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd"); //繳費有效期限
        ViewData["ReturnURL"] = $"{Request.Scheme}://{Request.Host}/Home/CallbackReturn"; //支付完成返回商店網址
        ViewData["CustomerURL"] = $"{Request.Scheme}://{Request.Host}/Home/CallbackCustomer"; //商店取號網址
        ViewData["NotifyURL"] = $"{Request.Scheme}://{Request.Host}/Home/CallbackNotify"; //支付通知網址
        ViewData["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}"; //返回商店網址
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> shoppingcart(string i)
    {
        if (string.IsNullOrEmpty(i))
        {
            return RedirectToAction("exhibitionDetail");
        }
        else
        {
            //Console.WriteLine(i);
            var id = i.Split("|").SkipLast(1).ToArray();
            //Console.WriteLine(id.Length);
            List<shoppingcartViewModel> exhibitions = new List<shoppingcartViewModel>();

            for (int a = 0; a < id.Length; a++)
            {
                int exid = Convert.ToInt32(id[a]);
                Console.WriteLine(exid);
                shoppingcartViewModel model = new shoppingcartViewModel()
                {
                    exhibitions = await _context.Exhibitionsses.FirstOrDefaultAsync(m => m.ExhibitId == exid),
                    //tickets = await _context.Ticketsses.Where(m => m.EId == exid).FirstOrDefaultAsync(),
                };
                exhibitions.Add(model);
            }
            //Console.WriteLine(exhibitions[1].exhibition.datefrom);
            return Json(exhibitions);
        }
    }

    [HttpPost]
    public IActionResult clearCart([Formbody] List<TicketRoot> obj)
    {
        for (int i = 0; i < obj.Count; i++)
        {
            _context.Ticketsses.Add(obj[i].ticket);
        }
        _context.SaveChanges();
        return Json(obj);
    }

    //postQrcode

    [HttpPost]
    public IActionResult postQrCode([FromBody] QrCode ticket)
    {
        var ticketCoent = _context.Ticketsses.FirstOrDefault(t => t.VerificationCode == ticket.VerificationCode && t.Enterstate == 0)??new Ticketss();
        if(ticketCoent.VerificationCode != null)
        {
            var exId = ticketCoent.EId;
            if (exId == Convert.ToInt32(ticket.Ex_id))
            {
                ticketCoent.Enterstate = 1;
                ticketCoent.Entertime = DateTime.Now;
                _context.SaveChanges();
                return Json("成功進入");
            }
            else
            {
                return Json("展覽錯誤");
            }
        }else
        {
            return Json("無此票券");
        }
    }

    //postMfQrcode

    [HttpPost]
    public IActionResult postMfQrCode([FromBody] MfQrCode mfQrCode)
    {
        int exId = Convert.ToInt32(mfQrCode.ex_id);
        int boothNum = Convert.ToInt32(mfQrCode.boothNum);
        int mfId = Convert.ToInt32(mfQrCode.mf_id);
        IList<BoothMapss> booths = _context.BoothMapsses.Where(b => b.EId == exId && b.BoothState == 2).ToList();
        if (booths.Count > 0)
        {
            var TheBooth = booths.FirstOrDefault(b => b.BoothNumber == boothNum);
            if (TheBooth.MfId == mfId)
            {
                return Json("歡迎入場");
            }
            else
            {
                return Json("無效攤位");
            }
        }

        return Json("無效展覽");
    }


    /// <summary>
    /// 傳送訂單至藍新金流
    /// </summary>
    /// <param name="inModel"></param>
    /// <returns></returns>
    [ValidateAntiForgeryToken]
    public dynamic SendToNewebPay(SendToNewebPayIn inModel)
    {
        SendToNewebPayOut outModel = new SendToNewebPayOut();

        // 藍新金流線上付款

        //交易欄位
        List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();
        // 商店代號
        TradeInfo.Add(new KeyValuePair<string, string>("MerchantID", inModel.MerchantID));
        // 回傳格式
        TradeInfo.Add(new KeyValuePair<string, string>("RespondType", "String"));
        // TimeStamp
        TradeInfo.Add(new KeyValuePair<string, string>("TimeStamp", DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0)).ToUnixTimeSeconds().ToString()));
        // 串接程式版本
        TradeInfo.Add(new KeyValuePair<string, string>("Version", "2.0"));
        // 商店訂單編號
        TradeInfo.Add(new KeyValuePair<string, string>("MerchantOrderNo", inModel.MerchantOrderNo));
        // 訂單金額
        TradeInfo.Add(new KeyValuePair<string, string>("Amt", inModel.Amt));
        // 商品資訊
        TradeInfo.Add(new KeyValuePair<string, string>("ItemDesc", inModel.ItemDesc));
        // 繳費有效期限(適用於非即時交易)
        TradeInfo.Add(new KeyValuePair<string, string>("ExpireDate", inModel.ExpireDate));
        // 支付完成返回商店網址
        TradeInfo.Add(new KeyValuePair<string, string>("ReturnURL", inModel.ReturnURL));
        // 支付通知網址
        TradeInfo.Add(new KeyValuePair<string, string>("NotifyURL", inModel.NotifyURL));
        // 商店取號網址
        TradeInfo.Add(new KeyValuePair<string, string>("CustomerURL", inModel.CustomerURL));
        // 支付取消返回商店網址
        TradeInfo.Add(new KeyValuePair<string, string>("ClientBackURL", inModel.ClientBackURL));
        // 付款人電子信箱
        TradeInfo.Add(new KeyValuePair<string, string>("Email", inModel.Email));
        // 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
        TradeInfo.Add(new KeyValuePair<string, string>("EmailModify", "0"));

        //信用卡 付款
        if (inModel.ChannelID == "CREDIT")
        {
            TradeInfo.Add(new KeyValuePair<string, string>("CREDIT", "1"));
        }
        //ATM 付款
        if (inModel.ChannelID == "VACC")
        {
            TradeInfo.Add(new KeyValuePair<string, string>("VACC", "1"));
        }
        string TradeInfoParam = string.Join("&", TradeInfo.Select(x => $"{x.Key}={x.Value}"));

        // API 傳送欄位
        // 商店代號
        outModel.MerchantID = inModel.MerchantID;
        // 串接程式版本
        outModel.Version = "2.0";
        //交易資料 AES 加解密
        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
        string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
        string TradeInfoEncrypt = EncryptAESHex(TradeInfoParam, HashKey, HashIV);
        outModel.TradeInfo = TradeInfoEncrypt;
        //交易資料 SHA256 加密
        outModel.TradeSha = EncryptSHA256($"HashKey={HashKey}&{TradeInfoEncrypt}&HashIV={HashIV}");
        dynamic json = JsonConvert.SerializeObject(outModel);

        //return Json(new { MerchantID = outModel.MerchantID, TradeInfo = outModel.TradeInfo, TradeSha = outModel.TradeSha, Version = outModel.Version });
        return json;
    }

    /// <summary>
    /// 支付完成返回網址
    /// </summary>
    /// <returns></returns>
    public IActionResult CallbackReturn()
    {
        // 接收參數
        StringBuilder receive = new StringBuilder();
        foreach (var item in Request.Form)
        {
            receive.AppendLine(item.Key + "=" + item.Value + "<br>");
        }
        ViewData["ReceiveObj"] = receive.ToString();

        // 解密訊息
        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
        string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼

        string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
        NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
        receive.Length = 0;
        foreach (String key in decryptTradeCollection.AllKeys)
        {
            receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
        }
        ViewData["TradeInfo"] = receive.ToString();

        return View();
    }

    /// <summary>
    /// 商店取號網址
    /// </summary>
    /// <returns></returns>
    public IActionResult CallbackCustomer()
    {
        // 接收參數
        StringBuilder receive = new StringBuilder();
        foreach (var item in Request.Form)
        {
            receive.AppendLine(item.Key + "=" + item.Value + "<br>");
        }
        ViewData["ReceiveObj"] = receive.ToString();

        // 解密訊息
        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
        string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
        string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
        NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
        receive.Length = 0;
        foreach (String key in decryptTradeCollection.AllKeys)
        {
            receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
        }
        ViewData["TradeInfo"] = receive.ToString();
        return View();
    }

    /// <summary>
    /// 支付通知網址
    /// </summary>
    /// <returns></returns>
    public IActionResult CallbackNotify()
    {
        // 接收參數
        StringBuilder receive = new StringBuilder();
        foreach (var item in Request.Form)
        {
            receive.AppendLine(item.Key + "=" + item.Value + "<br>");
        }
        ViewData["ReceiveObj"] = receive.ToString();

        // 解密訊息
        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
        string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
        string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
        NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
        receive.Length = 0;
        foreach (String key in decryptTradeCollection.AllKeys)
        {
            receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
        }
        ViewData["TradeInfo"] = receive.ToString();

        return View();
    }

    /// <summary>
    /// 加密後再轉 16 進制字串
    /// </summary>
    /// <param name="source">加密前字串</param>
    /// <param name="cryptoKey">加密金鑰</param>
    /// <param name="cryptoIV">cryptoIV</param>
    /// <returns>加密後的字串</returns>
    public string EncryptAESHex(string source, string cryptoKey, string cryptoIV)
    {
        string result = string.Empty;

        if (!string.IsNullOrEmpty(source))
        {
            var encryptValue = EncryptAES(Encoding.UTF8.GetBytes(source), cryptoKey, cryptoIV);

            if (encryptValue != null)
            {
                result = BitConverter.ToString(encryptValue)?.Replace("-", string.Empty)?.ToLower();
            }
        }

        return result;
    }

    /// <summary>
    /// 字串加密AES
    /// </summary>
    /// <param name="source">加密前字串</param>
    /// <param name="cryptoKey">加密金鑰</param>
    /// <param name="cryptoIV">cryptoIV</param>
    /// <returns>加密後字串</returns>
    public byte[] EncryptAES(byte[] source, string cryptoKey, string cryptoIV)
    {
        byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
        byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Mode = System.Security.Cryptography.CipherMode.CBC;
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            aes.Key = dataKey;
            aes.IV = dataIV;

            using (var encryptor = aes.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(source, 0, source.Length);
            }
        }
    }

    /// <summary>
    /// 字串加密SHA256
    /// </summary>
    /// <param name="source">加密前字串</param>
    /// <returns>加密後字串</returns>
    public string EncryptSHA256(string source)
    {
        string result = string.Empty;

        using (System.Security.Cryptography.SHA256 algorithm = System.Security.Cryptography.SHA256.Create())
        {
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));

            if (hash != null)
            {
                result = BitConverter.ToString(hash)?.Replace("-", string.Empty)?.ToUpper();
            }

        }
        return result;
    }

    /// <summary>
    /// 16 進制字串解密
    /// </summary>
    /// <param name="source">加密前字串</param>
    /// <param name="cryptoKey">加密金鑰</param>
    /// <param name="cryptoIV">cryptoIV</param>
    /// <returns>解密後的字串</returns>
    public string DecryptAESHex(string source, string cryptoKey, string cryptoIV)
    {
        string result = string.Empty;

        if (!string.IsNullOrEmpty(source))
        {
            // 將 16 進制字串 轉為 byte[] 後
            byte[] sourceBytes = ToByteArray(source);

            if (sourceBytes != null)
            {
                // 使用金鑰解密後，轉回 加密前 value
                result = Encoding.UTF8.GetString(DecryptAES(sourceBytes, cryptoKey, cryptoIV)).Trim();
            }
        }

        return result;
    }

    /// <summary>
    /// 將16進位字串轉換為byteArray
    /// </summary>
    /// <param name="source">欲轉換之字串</param>
    /// <returns></returns>
    public byte[] ToByteArray(string source)
    {
        byte[] result = null;

        if (!string.IsNullOrWhiteSpace(source))
        {
            var outputLength = source.Length / 2;
            var output = new byte[outputLength];

            for (var i = 0; i < outputLength; i++)
            {
                output[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
            }
            result = output;
        }

        return result;
    }

    /// <summary>
    /// 字串解密AES
    /// </summary>
    /// <param name="source">解密前字串</param>
    /// <param name="cryptoKey">解密金鑰</param>
    /// <param name="cryptoIV">cryptoIV</param>
    /// <returns>解密後字串</returns>
    public static byte[] DecryptAES(byte[] source, string cryptoKey, string cryptoIV)
    {
        byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
        byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Mode = System.Security.Cryptography.CipherMode.CBC;
            // 智付通無法直接用PaddingMode.PKCS7，會跳"填補無效，而且無法移除。"
            // 所以改為PaddingMode.None並搭配RemovePKCS7Padding
            aes.Padding = System.Security.Cryptography.PaddingMode.None;
            aes.Key = dataKey;
            aes.IV = dataIV;

            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] data = decryptor.TransformFinalBlock(source, 0, source.Length);
                int iLength = data[data.Length - 1];
                var output = new byte[data.Length - iLength];
                Buffer.BlockCopy(data, 0, output, 0, output.Length);
                return output;
            }
        }
    }
}

