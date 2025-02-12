using Microsoft.EntityFrameworkCore.Migrations;

namespace bitcube.Migrations
{
    public partial class UserApiKeyFeild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "apiKey",
                table: "users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apiKey",
                table: "users");
        }
    }
}
