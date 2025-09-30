using CuraLinkDemoProject.CuraLinkDemo.Domain.Enums;

namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }         
        public Role Role { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<ResidentMovement> ResidentMovements { get; set; } = new List<ResidentMovement>();
    }
}
