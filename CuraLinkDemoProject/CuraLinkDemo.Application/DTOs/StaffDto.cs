using CuraLinkDemoProject.CuraLinkDemo.Domain.Enums;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }     // Nurse, Doctor и т.д.
        public string PhoneNumber { get; set; }
    }
}
