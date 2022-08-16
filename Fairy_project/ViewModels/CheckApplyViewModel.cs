using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class CheckApplyViewModel
    {

        //public Apply? apply { get; set; }
        //public Manufactures? manufactures { get; set; }



        public int applyNum { get; set; }
        public int? mf_Id { get; set; }
        public int? boothNumber { get; set; }
        public int? checkState { get; set; }
        public string? mf_logo { get; set; }
        public string? mf_P_img { get; set; }
        public string? mf_Description { get; set; }


        public int manufactureId { get; set; }
        public string? manufactureAcc { get; set; }
        public string? manufactureName { get; set; }
        public string? mfPerson { get; set; }
        public string? mfPhoneNum { get; set; }


        public int? boothLv { get; set; }
        public int? boothPrice { get; set; }

        public string? message { get; set; }
        public DateTime? paytime { get; set; }

    }
}
