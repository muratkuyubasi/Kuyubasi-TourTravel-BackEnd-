using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class ActiveTours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ActiveTours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourRecordId = table.Column<int>(type: "int", nullable: false),
                    PeriodRecordId = table.Column<int>(type: "int", nullable: false),
                    RegionRecordId = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChild = table.Column<bool>(type: "bit", nullable: false),
                    ChildQuota = table.Column<int>(type: "int", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ActiveTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveTours_PeriodRecords_PeriodRecordId",
                        column: x => x.PeriodRecordId,
                        principalTable: "PeriodRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActiveTours_RegionRecords_RegionRecordId",
                        column: x => x.RegionRecordId,
                        principalTable: "RegionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActiveTours_TourRecords_TourRecordId",
                        column: x => x.TourRecordId,
                        principalTable: "TourRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActiveTours_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActiveTours_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActiveTours_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TourCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    CategoryRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourCategories_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourCategories_CategoryRecord_CategoryRecordId",
                        column: x => x.CategoryRecordId,
                        principalTable: "CategoryRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourDays_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourDepartures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    DepartureRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDepartures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourDepartures_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourDepartures_DepartureRecords_DepartureRecordId",
                        column: x => x.DepartureRecordId,
                        principalTable: "DepartureRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCover = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourMedias_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ExtraPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPrices_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InPrice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourSpecifications_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTransportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveTourId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTransportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourTransportations_ActiveTours_ActiveTourId",
                        column: x => x.ActiveTourId,
                        principalTable: "ActiveTours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_CreatedBy",
                table: "ActiveTours",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_DeletedBy",
                table: "ActiveTours",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_ModifiedBy",
                table: "ActiveTours",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_PeriodRecordId",
                table: "ActiveTours",
                column: "PeriodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_RegionRecordId",
                table: "ActiveTours",
                column: "RegionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTours_TourRecordId",
                table: "ActiveTours",
                column: "TourRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TourCategories_ActiveTourId",
                table: "TourCategories",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourCategories_CategoryRecordId",
                table: "TourCategories",
                column: "CategoryRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TourDays_ActiveTourId",
                table: "TourDays",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourDepartures_ActiveTourId",
                table: "TourDepartures",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourDepartures_DepartureRecordId",
                table: "TourDepartures",
                column: "DepartureRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TourMedias_ActiveTourId",
                table: "TourMedias",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrices_ActiveTourId",
                table: "TourPrices",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourSpecifications_ActiveTourId",
                table: "TourSpecifications",
                column: "ActiveTourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTransportations_ActiveTourId",
                table: "TourTransportations",
                column: "ActiveTourId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourCategories");

            migrationBuilder.DropTable(
                name: "TourDays");

            migrationBuilder.DropTable(
                name: "TourDepartures");

            migrationBuilder.DropTable(
                name: "TourMedias");

            migrationBuilder.DropTable(
                name: "TourPrices");

            migrationBuilder.DropTable(
                name: "TourSpecifications");

            migrationBuilder.DropTable(
                name: "TourTransportations");

            migrationBuilder.DropTable(
                name: "ActiveTours");

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
    }
}
