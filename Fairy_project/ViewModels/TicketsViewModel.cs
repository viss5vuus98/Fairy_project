using System;
using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class TicketsViewModel
    {
        public IList<Exhibitionss> exhibition { get; set; }
        public IList<Ticketss> tickets { get; set; }
    }

    public class SussessMessage
    {
        public string eamil { get; set; }
        public int order { get; set; }
    }
}

                    