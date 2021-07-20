using Microsoft.EntityFrameworkCore.Migrations;

namespace Homework_18.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    LoanRate = table.Column<int>(type: "int", nullable: false),
                    DepositRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Departments_DepartmentRefId",
                        column: x => x.DepartmentRefId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Funds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Loan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Money_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ClientRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Clients_ClientRefId",
                        column: x => x.ClientRefId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepositRate", "LoanRate", "Name" },
                values: new object[] { 1, 5, 15, 0 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepositRate", "LoanRate", "Name" },
                values: new object[] { 2, 10, 10, 1 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepositRate", "LoanRate", "Name" },
                values: new object[] { 3, 15, 5, 2 });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DepartmentRefId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Orson Avery" },
                    { 28, 3, "Nicholas King" },
                    { 27, 3, "Aphrodite Dixon" },
                    { 26, 3, "Baxter Macias" },
                    { 25, 3, "Cameron Dillon" },
                    { 24, 3, "Thane Talley" },
                    { 23, 3, "Leo Little" },
                    { 22, 3, "Trevor Mercado" },
                    { 21, 3, "Myles Marsh" },
                    { 20, 2, "Oscar Coleman" },
                    { 19, 2, "Juliet Clark" },
                    { 18, 2, "Xanthus Knapp" },
                    { 17, 2, "Gabriel Perez" },
                    { 16, 2, "Alden Ingram" },
                    { 15, 2, "Ray Lyons" },
                    { 14, 2, "Ora Weaver" },
                    { 13, 2, "Tiger Whitehead" },
                    { 12, 2, "Austin Wilkins" },
                    { 11, 2, "Wynne Gilliam" },
                    { 10, 1, "Dexter Huber" },
                    { 9, 1, "Felicia Sutton" },
                    { 8, 1, "Vance Barlow" },
                    { 7, 1, "Hilary Coleman" },
                    { 6, 1, "Emma Sharp" },
                    { 5, 1, "Hamish Cole" },
                    { 4, 1, "Yoshi Gallagher" },
                    { 3, 1, "Kermit Olsen" },
                    { 2, 1, "Whoopi Franks" },
                    { 29, 3, "Lydia Kirk" },
                    { 30, 3, "Hop Buckley" }
                });

            migrationBuilder.InsertData(
                table: "Money",
                columns: new[] { "ClientId", "Deposit", "Funds", "Loan", "Type" },
                values: new object[,]
                {
                    { 1, 0m, 13922m, 0m, 0 },
                    { 28, 0m, 11960m, 0m, 0 },
                    { 27, 0m, 7256m, 0m, 0 },
                    { 26, 0m, 4652m, 0m, 0 },
                    { 25, 0m, 1806m, 0m, 0 },
                    { 24, 0m, 29278m, 0m, 0 },
                    { 23, 0m, 41542m, 0m, 0 },
                    { 22, 0m, 21236m, 0m, 0 },
                    { 21, 0m, 36542m, 0m, 0 },
                    { 20, 0m, 45049m, 0m, 0 },
                    { 19, 0m, 9670m, 0m, 0 },
                    { 18, 0m, 18124m, 0m, 0 },
                    { 17, 0m, 12695m, 0m, 0 },
                    { 16, 0m, 15563m, 0m, 0 },
                    { 15, 0m, 37097m, 0m, 0 },
                    { 14, 0m, 5459m, 0m, 0 },
                    { 13, 0m, 21018m, 0m, 0 },
                    { 12, 0m, 1213m, 0m, 0 },
                    { 11, 0m, 26993m, 0m, 0 },
                    { 10, 0m, 10740m, 0m, 0 },
                    { 9, 0m, 10516m, 0m, 0 },
                    { 8, 0m, 41162m, 0m, 0 },
                    { 7, 0m, 17358m, 0m, 0 },
                    { 6, 0m, 25378m, 0m, 0 },
                    { 5, 0m, 4595m, 0m, 0 },
                    { 4, 0m, 40967m, 0m, 0 },
                    { 3, 0m, 20543m, 0m, 0 },
                    { 2, 0m, 8452m, 0m, 0 },
                    { 29, 0m, 31206m, 0m, 0 },
                    { 30, 0m, 32768m, 0m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DepartmentRefId",
                table: "Clients",
                column: "DepartmentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ClientRefId",
                table: "Transactions",
                column: "ClientRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
