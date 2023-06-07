using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesmanCore.DataAccess.Migrations
{
    public partial class UserFileUniqueIndexMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_FileName_UserId",
                table: "UserFiles",
                columns: new[] { "FileName", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFiles_FileName_UserId",
                table: "UserFiles");
        }
    }
}
