using System;
using System.Collections.Generic;
using System.Linq;
using Fairy_project.Models;
namespace Fairy_project.ViewModels
{
    public class eDrtailViewModel
    {

        public IList<Manufacturess> Manufactures
        {
            get;
            set;
        }

        public Exhibitionss Exhibition
        {
            get;
            set;
        }

        public IList<BoothMapss> booths
        {
            get;
            set;
        }
    }
}
