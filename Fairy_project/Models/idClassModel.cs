using System;
namespace Fairy_project.Models
{
    public class GetIdClassModel
    {
        public int Ex_id { get; set; }
        public int Mf_id { get; set; }
    }

    public class KeyWord
    {
        public string ex_keyWord { get; set; }
    }

    public class SearchDate
    {
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
    }

    public class QrCode
    {
        public int Ex_id { get; set; }
        public string VerificationCode { get; set; }
    }

    public class MfQrCode
    {
        public string ex_id { get; set; }
        public string mf_id { get; set; }
        public string boothNum { get; set; }
    }
}

