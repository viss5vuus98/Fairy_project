﻿using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models
{
    public class Manufactures
    {
        [Key]
        public int manufactureName { get; set; }
        //後面要改manufactureId
        public string? manufactureAcc { get; set; }
        public string? manufacture { get; set; }
        //後面要改manufactureＮame
        public string mfPerson { get; set; }
        public string mfEmail { get; set; }
        public int mfPhoneNum { get; set; }
    }
}
