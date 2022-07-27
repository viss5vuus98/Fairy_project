using System;
using Fairy_project.Models;


namespace Fairy_project.ViewModels
{
    public class QrcodeModal: Exhibitionss
    {
        public string? ExhibitName { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? Dateto { get; set; }
        public string? VerificationCode { get; set; }
    }
}

