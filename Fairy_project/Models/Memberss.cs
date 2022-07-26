using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Memberss
    {
        public Memberss()
        {
            Ticketsses = new HashSet<Ticketss>();
        }

        public int MemberId { get; set; }
        public string? MemberAc { get; set; }
        public string? MemberName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual Permissionss? MemberAcNavigation { get; set; }
        public virtual ICollection<Ticketss> Ticketsses { get; set; }
    }
}
