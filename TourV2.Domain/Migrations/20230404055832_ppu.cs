using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class ppu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TourPriceId",
                table: "TourReservationPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TourReservationPersons_TourPriceId",
                table: "TourReservationPersons",
                column: "TourPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourReservationPersons_TourPrices_TourPriceId",
                table: "TourReservationPersons",
                column: "TourPriceId",
                principalTable: "TourPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourReservationPersons_TourPrices_TourPriceId",
                table: "TourReservationPersons");

            migrationBuilder.DropIndex(
                name: "IX_TourReservationPersons_TourPriceId",
                table: "TourReservationPersons");

            migrationBuilder.DropColumn(
                name: "TourPriceId",
                table: "TourReservationPersons");
        }
    }
}
