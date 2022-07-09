using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models
{
    public class Booth
    {
        [Key]
        public int boothId { get; set; }
        public int e_Id { get; set; }
        public int mf_Id { get; set; }
        public string? boothImg { get; set; }
        public string? mf_logo { get; set; }
        public string? mf_P_img { get; set; }
        public string? mf_Description { get; set; }
        public int checkStatus { get; set; }
        public int boothNumber { get; set; }
    }
}
