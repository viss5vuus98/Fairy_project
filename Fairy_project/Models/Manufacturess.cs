using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Manufacturess
    {
        public Manufacturess()
        {
            BoothMapsses = new HashSet<BoothMapss>();
        }

        public int ManufactureId { get; set; }
        public string? ManufactureAcc { get; set; }
        public string? ManufactureName { get; set; }
        public string? MfPerson { get; set; }
        public string? MfEmail { get; set; }
        public string? MfPhoneNum { get; set; }

        public virtual ICollection<BoothMapss> BoothMapsses { get; set; }
    }
}
