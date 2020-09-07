using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApi.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentACarCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true),
                    AverageGrade = table.Column<double>(nullable: false),
                    WeekRentalDiscount = table.Column<double>(nullable: false),
                    MonthRentalDiscount = table.Column<double>(nullable: false),
                    HeadOfficeId = table.Column<int>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Admin = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentACarCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MapString = table.Column<string>(nullable: true),
                    RentACarCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_RentACarCompanies_RentACarCompanyId",
                        column: x => x.RentACarCompanyId,
                        principalTable: "RentACarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RentACarCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyRatings_RentACarCompanies_RentACarCompanyId",
                        column: x => x.RentACarCompanyId,
                        principalTable: "RentACarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    RentACarCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Income_RentACarCompanies_RentACarCompanyId",
                        column: x => x.RentACarCompanyId,
                        principalTable: "RentACarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    AverageGrade = table.Column<double>(nullable: false),
                    CurrentDestination = table.Column<string>(nullable: true),
                    Doors = table.Column<int>(nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsOnDiscount = table.Column<bool>(nullable: false),
                    OldPrice = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    RentACarCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_RentACarCompanies_RentACarCompanyId",
                        column: x => x.RentACarCompanyId,
                        principalTable: "RentACarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAuthId = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NumberOfDays = table.Column<int>(nullable: false),
                    StartingLocation = table.Column<string>(nullable: true),
                    ReturningLocation = table.Column<string>(nullable: true),
                    DaysLeft = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservedDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    VehicleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservedDate_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleRatings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RentACarCompanyId",
                table: "Branches",
                column: "RentACarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRatings_RentACarCompanyId",
                table: "CompanyRatings",
                column: "RentACarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Income_RentACarCompanyId",
                table: "Income",
                column: "RentACarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarCompanies_HeadOfficeId",
                table: "RentACarCompanies",
                column: "HeadOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehicleId",
                table: "Reservations",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedDate_VehicleId",
                table: "ReservedDate",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRatings_VehicleId",
                table: "VehicleRatings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RentACarCompanyId",
                table: "Vehicles",
                column: "RentACarCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACarCompanies_Branches_HeadOfficeId",
                table: "RentACarCompanies",
                column: "HeadOfficeId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_RentACarCompanies_RentACarCompanyId",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "CompanyRatings");

            migrationBuilder.DropTable(
                name: "Income");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservedDate");

            migrationBuilder.DropTable(
                name: "VehicleRatings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "RentACarCompanies");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
