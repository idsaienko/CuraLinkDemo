namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class ResidentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoomNumber { get; set; }
        public int CareLevel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }
    }

    public class CreateResidentDto
    {
        public string FullName { get; set; }
        public string RoomNumber { get; set; }
        public int CareLevel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }
    }
    public class ResidentWithAppointmentsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoomNumber { get; set; }
        public int CareLevel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }

        public List<AppointmentDto> Appointments { get; set; } = new();
    }
}
