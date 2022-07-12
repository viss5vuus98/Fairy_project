using System;
using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models
{
    public class Apply
    {
        [Key]
        public int applyNum { get; set; }
        public int? e_Id { get; set; }
        public int? mf_Id { get; set; }
        public int? boothNumber { get; set; }
        public int? checkState { get; set; }
        public string? mf_logo { get; set; }
        public string? mf_P_img { get; set; }
        public string? mf_Description { get; set; }
    }
}

