using Fairy_project.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Fairy_project.ViewModels
{
    public class MasterViewModels
    {
        public int exhibitId { get; set; }
        public string exhibitName { get; set; }
        public string exhibit_Pre_img { get; set; }

        public int exhibitStatus { get; set; }

        public int soldbooth { get; set; }
        public int enteredpeople { get; set; }
        public int soldticket { get; set; }
    }
}
