using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class ExhibitIdDetail_1_
    {
        public int exhibitId { get; set; }
        public string? exhibitName { get; set; }
        public int? exhibitStatus { get; set; }
        public string? exhibit_P_img { get; set; }
        public string? exhibit_T_img { get; set; }
        public string? exhibit_Pre_img { get; set; }
        public IFormFile? fexhibit_P_img { get; set; }
        public IFormFile? fexhibit_T_img { get; set; }
        public IFormFile? fexhibit_Pre_img { get; set; }
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
        public int? areaNum { get; set; }
        public string? areaNumstring { get; set; }
        public string? ex_description { get; set; }
        public int? ex_personTime { get; set; }
        public int? ex_totalImcome { get; set; }
        public int? ticket_Peice { get; set; }

        public List<CreatBoothsViewModel>? setboothslist { get; set; }

        //---------------------------------------------------

        public List<CheckApplyViewModel>? applylist { get; set; }

        public int? applysum { get; set; }
        public int? applysumprice { get; set; }

        //---------------------------------------------------

        public int? averageperson { get; set; }
        public int? yesterdayperson { get; set; }
        public int? soldprice { get; set; }
        public int? soldsum { get; set; }

        public int? insum { get; set; }
        
        public int? mostpeople  { get; set; }   

    }
}
