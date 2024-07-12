using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class departureUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Departures");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Departures");

            migrationBuilder.DropColumn(
                name: "LatLng",
                table: "Departures");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "DepartureRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "DepartureRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LatLng",
                table: "DepartureRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "DepartureRecords");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "DepartureRecords");

            migrationBuilder.DropColumn(
                name: "LatLng",
                table: "DepartureRecords");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Departures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Departures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LatLng",
                table: "Departures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
