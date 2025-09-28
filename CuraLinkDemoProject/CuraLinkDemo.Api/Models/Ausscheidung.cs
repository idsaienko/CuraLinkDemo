using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Models
{
    public class Ausscheidung
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public Resident Resident { get; set; } = null!;

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        public DateTime Time { get; set; }

        public string Abstand { get; set; } = string.Empty;
        public string Menge { get; set; } = string.Empty;
        public string Konsistenz { get; set; } = string.Empty;
    }
}
