using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    [Keyless]
    public class PainObservation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
        public int ResidentId { get; set; }
        public Resident Resident { get; set; }
    }
}
