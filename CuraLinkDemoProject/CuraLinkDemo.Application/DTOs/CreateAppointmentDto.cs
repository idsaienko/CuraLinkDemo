namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public int ResidentId { get; set; }
        public string ResidentName { get; set; }
        public int StaffId { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
