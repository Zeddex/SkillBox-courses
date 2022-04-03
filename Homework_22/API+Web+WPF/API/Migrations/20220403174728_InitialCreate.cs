using Microsoft.EntityFrameworkCore.Migrations;

namespace Homework_22_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Address", "Iban", "Name", "Phone", "Surname" },
                values: new object[,]
                {
                    { 1, "548-9419 Ac St.", "MC4687627477700586662925905", "Clinton", "073-950-032", "Nielsen" },
                    { 2, "P.O. Box 755, 5715 Velit Street", "DK0458275512361305", "Summer", "981-871-867", "Horton" },
                    { 3, "5539 Elit. St.", "IE50OCEY16214666156540", "Brock", "644-470-655", "Benson" },
                    { 4, "Ap #972-9103 Eu Rd.", "SM7625407558845802833383352", "Donovan", "021-627-378", "Sanchez" },
                    { 5, "432-4010 Molestie Road", "AL04700264981286482617074842", "Martena", "464-878-841", "Stewart" },
                    { 6, "P.O. Box 351, 8135 Lorem Av.", "PL39719618279189562460931448", "Breanna", "436-251-379", "Benson" },
                    { 7, "448-9849 Blandit Ave", "GB98GWGL12119886433060", "Octavia", "494-387-366", "Cleveland" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
