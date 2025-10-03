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

            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                StaffId = 1,
                FullName = "Anna Schmidt",
                Role = Domain.Enums.Role.CareAssistant,
                PhoneNumber = "+491234567890"
            });

            modelBuilder.Entity<MealSchedule>().HasData(
                new MealSchedule { Id = 1, ResidentId = 1, MealType = "Frühstück", MealTime = new DateTime(2025, 11, 22, 8, 0, 0), Comments = "Normal" },
                new MealSchedule { Id = 2, ResidentId = 1, MealType = "Mittagessen", MealTime = new DateTime(2025, 11, 22, 12, 30, 0), Comments = "Vegetarisch" },
                new MealSchedule { Id = 3, ResidentId = 1, MealType = "Zwischenmahlzeit", MealTime = new DateTime(2025, 11, 22, 15, 0, 0), Comments = "Obst" },
                new MealSchedule { Id = 4, ResidentId = 1, MealType = "Abendessen", MealTime = new DateTime(2025, 11, 22, 18, 30, 0), Comments = "Leicht" }
            );

            modelBuilder.Entity<ResidentMovement>().HasData(
                new ResidentMovement { Id = 1, ResidentId = 1, StaffId = 1, Room = "Zimmer 101", Object = "Bett", Angle = 0, MovementTime = new DateTime(2025, 10, 02, 18, 00, 00), Notes = "Transfer ins Bett" },
                new ResidentMovement { Id = 2, ResidentId = 1, StaffId = 1, Room = "Zimmer 101", Object = "Rollstuhl", Angle = 90, MovementTime = new DateTime(2025, 10, 02, 13, 24, 00), Notes = "Transfer in Rollstuhl" }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { AppointmentId = 1, ResidentId = 1, StaffId = 1, Type = "Arztbesuch", DateTime = new DateTime(2025, 12, 15, 10, 15, 00), Notes = "Hausarzt" }
            );

            modelBuilder.Entity<Ausscheidung>().HasData(
                new Ausscheidung { Id = 1, ResidentId = 1, StaffId = 1, Time = new DateTime(2025, 10, 02, 14, 00, 00), Menge = "200ml", Konsistenz = "normal", Abstand = "3h" }
            );
        }
    }
}
