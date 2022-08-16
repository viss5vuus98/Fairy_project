namespace Fairy_project.ViewModels
{
    public class CreatAppliessViewModels
    {
        public int ApplyNum { get; set; }
        public string? EId { get; set; }
        public string? MfId { get; set; }
        public string? BoothNumber { get; set; }
        public int? CheckState { get; set; }
        public IFormFile? MfLogo { get; set; }
        public IFormFile? MfPImg { get; set; }
        public string? MfDescription { get; set; }
        public string? Message { get; set; }
    }
}
