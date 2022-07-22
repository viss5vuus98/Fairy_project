using System;
namespace Fairy_project.Models
{
    public class OrrPermissions : Permissions
    {
        public List<Member>? Members { get; set; }
        public List<Manufactures>? Manufactures { get; set; }
    }
}

