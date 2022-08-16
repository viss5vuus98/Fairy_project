using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Appliess
    {
        public int ApplyNum { get; set; }
        public int? EId { get; set; }
        public int? MfId { get; set; }
        public int? BoothNumber { get; set; }
        public int? CheckState { get; set; }
        public string? MfLogo { get; set; }
        public string? MfPImg { get; set; }
        public string? MfDescription { get; set; }
        public string? Message { get; set; }
        public DateTime? PayTime { get; set; }
    }
}
