using Fairy_project.Models;
using Microsoft.AspNetCore.Http;

namespace Fairy_project.ViewModels
{
    public class LoginViewModels
    {
        public Memberss member { get; set; }
        public Permissionss permissions { get; set; }
        public Manufacturess manufactures { get; set; }

        ISession Session { get; set; }

    }
}

