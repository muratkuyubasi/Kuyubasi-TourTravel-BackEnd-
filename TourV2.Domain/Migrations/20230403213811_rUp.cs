using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class rUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TourReservations_ActiveTourId",
                table: "TourReservations");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservations_ActiveTourId",
                table: "TourReservations",
                column: "ActiveTourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TourReservations_ActiveTourId",
                table: "TourReservations");

            migrationBuilder.CreateIndex(
                name: "IX_TourReservations_ActiveTourId",
                table: "TourReservations",
                column: "ActiveTourId",
                unique: true);
        }
    }
}
