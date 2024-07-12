using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class categoryİmage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CategoryRecord",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "CategoryRecord");
        }
    }
}
