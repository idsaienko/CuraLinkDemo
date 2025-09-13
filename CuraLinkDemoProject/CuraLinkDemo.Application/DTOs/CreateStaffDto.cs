using CuraLinkDemoProject.CuraLinkDemo.Domain.Enums;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class CreateStaffDto
    {
        public string FullName { get; set; }
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
