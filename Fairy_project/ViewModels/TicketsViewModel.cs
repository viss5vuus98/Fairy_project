using System;
using Fairy_project.Models;

namespace Fairy_project.ViewModels
{
    public class TicketsViewModel
    {
        public IList<Exhibition> exhibition { get; set; }
        public IList<Ticket> tickets { get; set; }
    }
}

                    