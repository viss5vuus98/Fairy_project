namespace Fairy_project.ViewModels
{
    public class CreatAppliessViewModels
    {
        public int ApplyNum { get; set; }
        public int? EId { get; set; }
        public int? MfId { get; set; }
        public int? BoothNumber { get; set; }
        public int? CheckState { get; set; }
        public IFormFile? MfLogo { get; set; }
        public IFormFile? MfPImg { get; set; }
        public string? MfDescription { get; set; }
        public string? Message { get; set; }
    }
}
