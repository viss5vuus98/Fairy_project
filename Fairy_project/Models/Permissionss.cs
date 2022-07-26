using System;
using System.Collections.Generic;

namespace Fairy_project.Models
{
    public partial class Permissionss
    {
        public Permissionss()
        {
            Membersses = new HashSet<Memberss>();
        }

        public string Account { get; set; } = null!;
        public string? Password { get; set; }
        public int? PermissionsLv { get; set; }

        public virtual ICollection<Memberss> Membersses { get; set; }
    }
}
