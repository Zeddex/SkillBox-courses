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
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName1 = table.Column<string>(type: "nvarchar(50)", nullable: false),
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
                    DepositType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepositType1 = table.Column<string>(type: "nvarchar(50)", nullable: false)
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
                columns: new[] { "Id", "DepartmentName1", "DepartmentName", "DepositRate", "LoanRate" },
                values: new object[] { 1, "Individual", "Individual", 5, 15 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName1", "DepartmentName", "DepositRate", "LoanRate" },
                values: new object[] { 2, "Business", "Business", 10, 10 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName1", "DepartmentName", "DepositRate", "LoanRate" },
                values: new object[] { 3, "Vip", "Vip", 15, 5 });

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
                columns: new[] { "ClientId", "Deposit", "DepositType1", "DepositType", "Funds", "Loan" },
                values: new object[,]
                {
                    { 1, 0m, "No", "No", 13922m, 0m },
                    { 28, 0m, "No", "No", 11960m, 0m },
                    { 27, 0m, "No", "No", 7256m, 0m },
                    { 26, 0m, "No", "No", 4652m, 0m },
                    { 25, 0m, "No", "No", 1806m, 0m },
                    { 24, 0m, "No", "No", 29278m, 0m },
                    { 23, 0m, "No", "No", 41542m, 0m },
                    { 22, 0m, "No", "No", 21236m, 0m },
                    { 21, 0m, "No", "No", 36542m, 0m },
                    { 20, 0m, "No", "No", 45049m, 0m },
                    { 19, 0m, "No", "No", 9670m, 0m },
                    { 18, 0m, "No", "No", 18124m, 0m },
                    { 17, 0m, "No", "No", 12695m, 0m },
                    { 16, 0m, "No", "No", 15563m, 0m },
                    { 15, 0m, "No", "No", 37097m, 0m },
                    { 14, 0m, "No", "No", 5459m, 0m },
                    { 13, 0m, "No", "No", 21018m, 0m },
                    { 12, 0m, "No", "No", 1213m, 0m },
                    { 11, 0m, "No", "No", 26993m, 0m },
                    { 10, 0m, "No", "No", 10740m, 0m },
                    { 9, 0m, "No", "No", 10516m, 0m },
                    { 8, 0m, "No", "No", 41162m, 0m },
                    { 7, 0m, "No", "No", 17358m, 0m },
                    { 6, 0m, "No", "No", 25378m, 0m },
                    { 5, 0m, "No", "No", 4595m, 0m },
                    { 4, 0m, "No", "No", 40967m, 0m },
                    { 3, 0m, "No", "No", 20543m, 0m },
                    { 2, 0m, "No", "No", 8452m, 0m },
                    { 29, 0m, "No", "No", 31206m, 0m },
                    { 30, 0m, "No", "No", 32768m, 0m }
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
