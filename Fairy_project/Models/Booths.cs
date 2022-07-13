using System;
using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models
{
    public class Booths
    {
        [Key]
        public int serialNumber { get; set; }
        public int? boothNumber { get; set; }
        public int? e_Id { get; set; }
        public int? mf_Id { get; set; }
        public int? boothState { get; set; }
        public int? boothLv { get; set; }
        //型別的
        public string? mf_logo { get; set; }
        public string? mf_P_img { get; set; }
        public int? boothPrice { get; set; }
    }
}

