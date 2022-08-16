using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Ticketss
    {
        public int OrderNum { get; set; }
        public int? EId { get; set; }
        public int? MId { get; set; }
        public int? Price { get; set; }
        public int? Enterstate { get; set; }
        public DateTime Entertime { get; set; }
        public DateTime? Ordertime { get; set; }
        public string? PresonName { get; set; }
        public string? PersonNumber { get; set; }
        public DateTime? PayTime { get; set; }
        public string? VerificationCode { get; set; }
    }
}
