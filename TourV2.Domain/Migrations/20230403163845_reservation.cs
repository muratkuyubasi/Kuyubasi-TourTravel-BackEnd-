using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    ReservationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDoorNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxAdministration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicePostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDoorNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancedPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvaliableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPayment = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReservations_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TourReservationPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourReservationId = table.Column<int>(type: "int", nullable: false),
                    TourDepartureId = table.Column<int>(type: "int", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourReservationPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourReservationPersons_TourDepartures_TourDepartureId",
                        column: x => x.TourDepartureId,
                        principalTable: "TourDepartures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TourReservationPersons_TourReservations_TourReservationId",
                        column: x => x.TourReservationId,
                        principalTable: "TourReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourReservationPersons_TourDepartureId",
                table: "TourReservationPersons",
                column: "TourDepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservationPersons_TourReservationId",
                table: "TourReservationPersons",
                column: "TourReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservations_ActiveTourId",
                table: "TourReservations",
                column: "ActiveTourId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourReservationPersons");

            migrationBuilder.DropTable(
                name: "TourReservations");
        }
    }
}
