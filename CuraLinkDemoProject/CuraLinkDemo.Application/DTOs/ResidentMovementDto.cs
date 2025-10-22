using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class ResidentMovementDto
    {
        public string Room { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
        public double? Angle { get; set; }
        public DateTime MovementTime { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int ResidentId { get; set; }
        public Resident Resident { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
