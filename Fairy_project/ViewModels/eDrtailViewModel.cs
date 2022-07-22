using System;
using System.Collections.Generic;
using System.Linq;
using Fairy_project.Models;
namespace Fairy_project.ViewModels
{
    public class eDrtailViewModel
    {

        public Manufactures? Manufactures
        {
            get;
            set;
        }
        public Exhibition Exhibition
        {
            get;
            set;
        }

        public IList<Booths> booths
        {
            get;
            set;
        }
    }
}
