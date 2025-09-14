namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class Resident
    {
        public int ResidentId { get; set; }           // Primary Key
        public string FullName { get; set; }          // Bewohner Name
        public string RoomNumber { get; set; }        // Zimmer Nummer
        public int CareLevel { get; set; }            // Pflege Niveu (Beispiel: 1 – mindest, 5 – intensiv)
        public DateTime DateOfBirth { get; set; }     // Geburtsdatum
        public string? Notes { get; set; }            // Extra Noten

        // 🔹 Navigation Eigenschaften (Verbindungen mit anderen Tabellen)
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        //public ICollection<Report> Reports { get; set; } = new List<Report>();
        //public ICollection<Medication> Medications { get; set; } = new List<Medication>();
        //public ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();
        //public ICollection<PainObservation> PainObservations { get; set; } = new List<PainObservation>();
    }
}
