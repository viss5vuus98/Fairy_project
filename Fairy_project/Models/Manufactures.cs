using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models
{
    public class Manufactures
    {
        [Key]
        public int manufactureName { get; set; }
        public string manufactureAcc { get; set; }
        public string manufacture { get; set; }
        public string mfPerson { get; set; }
        public string mfEmail { get; set; }
        public int mfPhoneNum { get; set; }
    }
}
