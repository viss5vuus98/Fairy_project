using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class BoothMapss
    {
        public int SerialNumber { get; set; }
        public int? BoothNumber { get; set; }
        public int? EId { get; set; }
        public int? MfId { get; set; }
        public int? BoothState { get; set; }
        public int? BoothLv { get; set; }
        public string? MfLogo { get; set; }
        public string? MfPImg { get; set; }
        public int? BoothPrice { get; set; }
    }
}
