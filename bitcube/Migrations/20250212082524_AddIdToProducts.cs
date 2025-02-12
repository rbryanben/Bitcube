using Microsoft.EntityFrameworkCore.Migrations;

namespace bitcube.Migrations
{
    public partial class AddIdToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_products_productId",
                table: "products",
                column: "productId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_productId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "id",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "products",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "productId");
        }
    }
}
