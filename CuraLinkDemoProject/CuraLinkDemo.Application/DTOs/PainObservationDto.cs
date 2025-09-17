namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class PainObservationDto
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int Score { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }
}
