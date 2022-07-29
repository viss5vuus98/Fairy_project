using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Exhibitionss
    {
        public int ExhibitId { get; set; }
        public string? ExhibitName { get; set; }
        public DateTime Datefrom { get; set; }
        public DateTime Dateto { get; set; }
        public int? ExhibitStatus { get; set; }
        public string? ExhibitPImg { get; set; }
        public string? ExhibitTImg { get; set; }
        public string? ExhibitPreImg { get; set; }
        public int? AreaNum { get; set; }
        public int? ExPersonTime { get; set; }
        public int? ExTotalImcome { get; set; }
        public string? ExDescription { get; set; }
        public int? TicketPrice { get; set; }
    }
}
