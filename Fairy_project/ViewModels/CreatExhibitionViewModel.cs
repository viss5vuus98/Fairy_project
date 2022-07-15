using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class CreatExhibitionViewModel
    {
        public string? exhibitName { get; set; }
        public int exhibitStatus { get; set; }
        public string? exhibit_P_img { get; set; }
        public string? exhibit_T_img { get; set; }
        public string? exhibit_Pre_img { get; set; }
        public DateTime? datefrom { get; set; }
        public DateTime? dateto { get; set; }
        //public DateTime? detail { get; set; } 詳細
        public int? ex_personTime { get; set; }
        public int? ex_totalImcome { get; set; }
        //public DateTime? price { get; set; } 價格
        public List<Booths> setboothslist { get; set; }
    }
}
