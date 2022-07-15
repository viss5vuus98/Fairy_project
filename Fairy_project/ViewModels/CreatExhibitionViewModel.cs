using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class CreatExhibitionViewModel
    {
        public string? exhibitName { get; set; }
        public int exhibitStatus { get; set; }
        public FileInfo? exhibit_P_img { get; set; }
        public FileInfo? exhibit_T_img { get; set; }
        public FileInfo? exhibit_Pre_img { get; set; }
        public DateTime? datefrom { get; set; }
        public DateTime? dateto { get; set; }
        public string? ex_description { get; set; }
        public int? ex_personTime { get; set; }
        public int? ex_totalImcome { get; set; }
        public int? ticket_Peice { get; set; }
        public List<CreatBoothsViewModel>? setboothslist { get; set; }
    }
}
