using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourCategory_Category_CategoryId",
                table: "TourCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPeriod_Periods_PeriodId",
                table: "TourPeriod");

            migrationBuilder.DropIndex(
                name: "IX_TourPeriod_PeriodId",
                table: "TourPeriod");

            migrationBuilder.DropIndex(
                name: "IX_TourCategory_CategoryId",
                table: "TourCategory");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionRecords_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionRecords_RegionId",
                table: "RegionRecords",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionRecords");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.CreateIndex(
                name: "IX_TourPeriod_PeriodId",
                table: "TourPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TourCategory_CategoryId",
                table: "TourCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategory_Category_CategoryId",
                table: "TourCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourPeriod_Periods_PeriodId",
                table: "TourPeriod",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
