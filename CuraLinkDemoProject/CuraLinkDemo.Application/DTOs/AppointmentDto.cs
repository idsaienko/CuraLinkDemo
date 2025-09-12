namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string ResidentName { get; set; }   // for ResidentId
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
