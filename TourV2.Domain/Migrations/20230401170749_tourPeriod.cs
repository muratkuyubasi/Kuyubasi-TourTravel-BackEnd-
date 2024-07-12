using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class tourPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecords_Categories_CategoryId",
                table: "CategoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TourCategories_Tours_TourId",
                table: "TourCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourCategories",
                table: "TourCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecords",
                table: "CategoryRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "TourCategories",
                newName: "TourCategory");

            migrationBuilder.RenameTable(
                name: "CategoryRecords",
                newName: "CategoryRecord");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_TourCategories_TourId",
                table: "TourCategory",
                newName: "IX_TourCategory_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourCategories_CategoryId",
                table: "TourCategory",
                newName: "IX_TourCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecords_CategoryId",
                table: "CategoryRecord",
                newName: "IX_CategoryRecord_CategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CategoryRecord",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourCategory",
                table: "TourCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecord",
                table: "CategoryRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodRecords_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPeriod_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourPeriod_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodRecords_PeriodId",
                table: "PeriodRecords",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPeriod_PeriodId",
                table: "TourPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPeriod_TourId",
                table: "TourPeriod",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecord_Category_CategoryId",
                table: "CategoryRecord",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategory_Category_CategoryId",
                table: "TourCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategory_Tours_TourId",
                table: "TourCategory",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRecord_Category_CategoryId",
                table: "CategoryRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TourCategory_Category_CategoryId",
                table: "TourCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TourCategory_Tours_TourId",
                table: "TourCategory");

            migrationBuilder.DropTable(
                name: "PeriodRecords");

            migrationBuilder.DropTable(
                name: "TourPeriod");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourCategory",
                table: "TourCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryRecord",
                table: "CategoryRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CategoryRecord");

            migrationBuilder.RenameTable(
                name: "TourCategory",
                newName: "TourCategories");

            migrationBuilder.RenameTable(
                name: "CategoryRecord",
                newName: "CategoryRecords");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_TourCategory_TourId",
                table: "TourCategories",
                newName: "IX_TourCategories_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourCategory_CategoryId",
                table: "TourCategories",
                newName: "IX_TourCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryRecord_CategoryId",
                table: "CategoryRecords",
                newName: "IX_CategoryRecords_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourCategories",
                table: "TourCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryRecords",
                table: "CategoryRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRecords_Categories_CategoryId",
                table: "CategoryRecords",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategories_Categories_CategoryId",
                table: "TourCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategories_Tours_TourId",
                table: "TourCategories",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
