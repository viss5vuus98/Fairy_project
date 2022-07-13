using System;
using System.ComponentModel.DataAnnotations;
namespace Fairy_project.Models
{
    public class Manager
    {
        [Key]
        public int managerId { get; set; }
        public string? center { get; set; }
        public string? managerName { get; set; }
        public string? centerEmail { get; set; }
        public string? phoneNumber { get; set; }
        public string? address { get; set; }
    }
}

