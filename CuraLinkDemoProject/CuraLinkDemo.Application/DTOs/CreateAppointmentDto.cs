namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public int Id { get; set; }
        public string ResidentName { get; set; }   // вместо ResidentId
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
