using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data
{
    public class CuraLinkDbContext : DbContext
    {
        public CuraLinkDbContext(DbContextOptions<CuraLinkDbContext> options) : base(options) { }

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<PainObservation> PainObservations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Resident → Appointments (1:N)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Resident)
                .WithMany(r => r.Appointments)
                .HasForeignKey(a => a.ResidentId);

            // Staff → Appointment (1:N)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StaffId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
