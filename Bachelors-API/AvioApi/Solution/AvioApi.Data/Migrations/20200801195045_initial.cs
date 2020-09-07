using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvioApi.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirCompanyBranches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MapString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCompanyBranches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PromoDescription = table.Column<string>(nullable: true),
                    AverageGrade = table.Column<double>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    HeadOfficeId = table.Column<int>(nullable: true),
                    Admin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirCompanies_AirCompanyBranches_HeadOfficeId",
                        column: x => x.HeadOfficeId,
                        principalTable: "AirCompanyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvioCompanyRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AirCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioCompanyRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvioCompanyRatings_AirCompanies_AirCompanyId",
                        column: x => x.AirCompanyId,
                        principalTable: "AirCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvioIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AirCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvioIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvioIncomes_AirCompanies_AirCompanyId",
                        column: x => x.AirCompanyId,
                        principalTable: "AirCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    AirCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinations_AirCompanies_AirCompanyId",
                        column: x => x.AirCompanyId,
                        principalTable: "AirCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    DepartureDestinationId = table.Column<int>(nullable: true),
                    ArrivalDestinationId = table.Column<int>(nullable: true),
                    TravelTime = table.Column<double>(nullable: false),
                    Mileage = table.Column<double>(nullable: false),
                    AverageGrade = table.Column<double>(nullable: false),
                    Discount = table.Column<bool>(nullable: false),
                    TicketPrice = table.Column<double>(nullable: false),
                    Luggage = table.Column<double>(nullable: false),
                    AirCompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_AirCompanies_AirCompanyId",
                        column: x => x.AirCompanyId,
                        principalTable: "AirCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Destinations_ArrivalDestinationId",
                        column: x => x.ArrivalDestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Destinations_DepartureDestinationId",
                        column: x => x.DepartureDestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightRating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FlightId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRating_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAuthId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    FlightId = table.Column<int>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyPhoto = table.Column<string>(nullable: true),
                    DepartureDestination = table.Column<string>(nullable: true),
                    ArrivalDestination = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TravelLength = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightReservations_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirCompanies_HeadOfficeId",
                table: "AirCompanies",
                column: "HeadOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_AvioCompanyRatings_AirCompanyId",
                table: "AvioCompanyRatings",
                column: "AirCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AvioIncomes_AirCompanyId",
                table: "AvioIncomes",
                column: "AirCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_AirCompanyId",
                table: "Destinations",
                column: "AirCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRating_FlightId",
                table: "FlightRating",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightReservations_FlightId",
                table: "FlightReservations",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirCompanyId",
                table: "Flights",
                column: "AirCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalDestinationId",
                table: "Flights",
                column: "ArrivalDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureDestinationId",
                table: "Flights",
                column: "DepartureDestinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvioCompanyRatings");

            migrationBuilder.DropTable(
                name: "AvioIncomes");

            migrationBuilder.DropTable(
                name: "FlightRating");

            migrationBuilder.DropTable(
                name: "FlightReservations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "AirCompanies");

            migrationBuilder.DropTable(
                name: "AirCompanyBranches");
        }
    }
}
