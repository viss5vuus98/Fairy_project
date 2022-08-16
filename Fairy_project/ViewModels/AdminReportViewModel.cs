namespace Fairy_project.ViewModels
{
    public class AdminReportViewModel
    {
        public int ExhibitId { get; set; }
        public string? ExhibitName { get; set; }

        public DateTime Datefrom { get; set; }
        public DateTime Dateto { get; set; }
        public int? people { get; set; }
        public int? booth { get; set; }
        public int? averageperson { get; set; }
        public int? pricesum { get; set; }
        public int? female { get; set; }
        public int? male { get; set; }

    }
}
