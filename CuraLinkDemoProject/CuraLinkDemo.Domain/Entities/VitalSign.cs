using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class VitalSign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public DateTime Time { get; set; }

        public int ResidentId { get; set; }
        public Resident Resident { get; set; }
    }
}
