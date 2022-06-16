using Microsoft.EntityFrameworkCore.Migrations;

namespace APBD8.Migrations
{
    public partial class AddedTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Login);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Login", "Password", "RefreshToken" },
                values: new object[] { "User", "User", "247fh249f429f10330d298ff43f" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Login", "Password", "RefreshToken" },
                values: new object[] { "User2", "User2", "4vn3v3409v89YSN48f40ffnb40f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
