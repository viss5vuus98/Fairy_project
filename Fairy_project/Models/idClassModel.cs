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
}

