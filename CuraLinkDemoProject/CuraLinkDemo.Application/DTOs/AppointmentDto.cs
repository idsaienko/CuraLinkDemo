namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public string ResidentName { get; set; }
        public string StaffName { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
