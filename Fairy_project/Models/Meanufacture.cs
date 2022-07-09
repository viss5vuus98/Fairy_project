using System;
using System.ComponentModel.DataAnnotations;

namespace Fairy_project.Models
{
    public class Manufacture
    {
        [Key]
        public int manufactureName { get; set; }
        public string manufactureAcc { get; set; }
        public string mfPerson { get; set; }
        public string mfEmail { get; set; }
        public int mfPhoneNum { get; set; }
    }
}
