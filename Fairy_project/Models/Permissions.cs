using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models
{
    public class Permissions
    {
        [Key]
        [MaxLength (250)]
        public string? account { get; set; }
        [MaxLength(50)]
        public string? password { get; set; }
        public int? permissionsLv { get; set; }
        //public List<Member> Members { get; set; }
    }
}
