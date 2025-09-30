using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
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
        public DbSet<ResidentMovement> ResidentMovements { get; set; }
        public DbSet<MealSchedule> MealSchedules { get; set; }
        public DbSet<Ausscheidung> Ausscheidungen { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

            modelBuilder.Entity<Ausscheidung>()
                .HasOne(a => a.Resident)
                .WithMany(s => s.Ausscheidungen)
                .HasForeignKey(a => a.ResidentId);

            modelBuilder.Entity<ResidentMovement>()
                .HasOne(a => a.Resident)
                .WithMany(s => s.ResidentMovements)
                .HasForeignKey(a => a.ResidentId);

            modelBuilder.Entity<ResidentMovement>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.ResidentMovements)
                .HasForeignKey(a => a.StaffId);

            modelBuilder.Entity<Resident>().HasData(
                new Resident
                {
                    ResidentId = 1,
                    FullName = "Max Muster",
                    RoomNumber = "101A",
                    PhotoUrl = "/images/residents/max.jpg",
                    Notes = "Vegetarisch, lactosefrei",
                    Phone = "+49 176 12345678"
                }
            );
        }

    }
}
