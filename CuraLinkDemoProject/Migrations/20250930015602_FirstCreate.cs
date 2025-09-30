using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuraLinkDemoProject.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "PainObservations",
                newName: "Score");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ResidentId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PainObservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PainObservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Ausscheidungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Abstand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Konsistenz = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ausscheidungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ausscheidungen_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "ResidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ausscheidungen_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    MealType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealSchedules_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "ResidentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Object = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Angle = table.Column<double>(type: "float", nullable: true),
                    MovementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentMovements_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "ResidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentMovements_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Residents",
                columns: new[] { "ResidentId", "CareLevel", "DateOfBirth", "FullName", "Notes", "Phone", "PhotoUrl", "RoomNumber" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max Muster", "Vegetarisch, lactosefrei", "+49 176 12345678", "/images/residents/max.jpg", "101A" });

            migrationBuilder.CreateIndex(
                name: "IX_Ausscheidungen_ResidentId",
                table: "Ausscheidungen",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ausscheidungen_StaffId",
                table: "Ausscheidungen",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MealSchedules_ResidentId",
                table: "MealSchedules",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentMovements_ResidentId",
                table: "ResidentMovements",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentMovements_StaffId",
                table: "ResidentMovements",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ausscheidungen");

            migrationBuilder.DropTable(
                name: "MealSchedules");

            migrationBuilder.DropTable(
                name: "ResidentMovements");

            migrationBuilder.DeleteData(
                table: "Residents",
                keyColumn: "ResidentId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ResidentId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PainObservations");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PainObservations");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "PainObservations",
                newName: "Amount");
        }
    }
}
