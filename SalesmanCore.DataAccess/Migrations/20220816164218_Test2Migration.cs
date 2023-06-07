using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesmanCore.DataAccess.Migrations
{
    public partial class Test2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
