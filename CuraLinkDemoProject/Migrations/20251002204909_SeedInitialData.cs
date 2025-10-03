using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CuraLinkDemoProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MealSchedules",
                columns: new[] { "Id", "Comments", "MealName", "MealTime", "MealType", "ResidentId" },
                values: new object[,]
                {
                    { 1, "Normal", "", new DateTime(2025, 11, 22, 8, 0, 0, 0, DateTimeKind.Unspecified), "Frühstück", 1 },
                    { 2, "Vegetarisch", "", new DateTime(2025, 11, 22, 12, 30, 0, 0, DateTimeKind.Unspecified), "Mittagessen", 1 },
                    { 3, "Obst", "", new DateTime(2025, 11, 22, 15, 0, 0, 0, DateTimeKind.Unspecified), "Zwischenmahlzeit", 1 },
                    { 4, "Leicht", "", new DateTime(2025, 11, 22, 18, 30, 0, 0, DateTimeKind.Unspecified), "Abendessen", 1 }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FullName", "PhoneNumber", "Role" },
                values: new object[] { 1, "Anna Schmidt", "+491234567890", 2 });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "DateTime", "Notes", "ResidentId", "StaffId", "Type" },
                values: new object[] { 1, new DateTime(2025, 12, 15, 10, 15, 0, 0, DateTimeKind.Unspecified), "Hausarzt", 1, 1, "Arztbesuch" });

            migrationBuilder.InsertData(
                table: "Ausscheidungen",
                columns: new[] { "Id", "Abstand", "Konsistenz", "Menge", "ResidentId", "StaffId", "Time" },
                values: new object[] { 1, "3h", "normal", "200ml", 1, 1, new DateTime(2025, 10, 2, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ResidentMovements",
                columns: new[] { "Id", "Angle", "MovementTime", "Notes", "Object", "ResidentId", "Room", "StaffId" },
                values: new object[,]
                {
                    { 1, 0.0, new DateTime(2025, 10, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), "Transfer ins Bett", "Bett", 1, "Zimmer 101", 1 },
                    { 2, 90.0, new DateTime(2025, 10, 2, 13, 24, 0, 0, DateTimeKind.Unspecified), "Transfer in Rollstuhl", "Rollstuhl", 1, "Zimmer 101", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ausscheidungen",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MealSchedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MealSchedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MealSchedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MealSchedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ResidentMovements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ResidentMovements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 1);
        }
    }
}
