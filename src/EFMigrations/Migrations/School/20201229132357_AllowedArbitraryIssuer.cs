using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMigrations.Migrations.School
{
    public partial class AllowedArbitraryIssuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowedArbitraryIssuer",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedArbitraryIssuer",
                table: "Clients");
        }
    }
}
