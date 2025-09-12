namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int ResidentId { get; set; }
        public Resident Resident { get; set; }

        public DateTime DateTime { get; set; }
        public string Type { get; set; }   // Checkup, Therapy, FamilyVisit
        public string Notes { get; set; }
    }
}
