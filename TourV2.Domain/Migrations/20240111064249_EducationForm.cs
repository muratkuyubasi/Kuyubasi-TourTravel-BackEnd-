using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourV2.Domain.Migrations
{
    public partial class EducationForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonType = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    MosqueId = table.Column<int>(type: "int", nullable: false),
                    Airport = table.Column<int>(type: "int", nullable: false),
                    İsPrice = table.Column<bool>(type: "bit", nullable: false),
                    StudentNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Studentbirthdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentGender = table.Column<int>(type: "int", nullable: false),
                    StudentPassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMobilePhoneNumberGermany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMobilePhoneNumberTurkey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranferDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferTransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentFatherNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentFatherPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMotherNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMotherPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MosqueReligiousOfficialFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MosqueReligiousOfficialPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mosques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mosques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mosques_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mosques_StateId",
                table: "Mosques",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationForms");

            migrationBuilder.DropTable(
                name: "Mosques");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
