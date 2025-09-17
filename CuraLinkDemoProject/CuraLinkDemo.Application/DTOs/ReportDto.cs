namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int StaffId { get; set; }
        public string TextReport { get; set; } = string.Empty;
    }
}
