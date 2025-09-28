namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class AusscheidungDto
    {
        public int ResidentId { get; set; }
        public int StaffId { get; set; }
        public DateTime Time { get; set; }
        public string Abstand { get; set; } = string.Empty;
        public string Menge { get; set; } = string.Empty;
        public string Konsistenz { get; set; } = string.Empty;
    }
}
