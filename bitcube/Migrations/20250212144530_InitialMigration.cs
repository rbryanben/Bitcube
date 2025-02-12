using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bitcube.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    firstname = table.Column<string>(type: "TEXT", nullable: false),
                    lastname = table.Column<string>(type: "TEXT", nullable: false),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    apiKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    reference = table.Column<string>(type: "TEXT", nullable: false),
                    ownerusername = table.Column<string>(type: "TEXT", nullable: true),
                    open = table.Column<bool>(type: "INTEGER", nullable: false),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_cart_users_ownerusername",
                        column: x => x.ownerusername,
                        principalTable: "users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productId = table.Column<string>(type: "TEXT", nullable: true),
                    productName = table.Column<string>(type: "TEXT", nullable: false),
                    productPrice = table.Column<double>(type: "REAL", nullable: false),
                    quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    createdByusername = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_users_createdByusername",
                        column: x => x.createdByusername,
                        principalTable: "users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cartProducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cartid = table.Column<int>(type: "INTEGER", nullable: true),
                    quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    productid = table.Column<int>(type: "INTEGER", nullable: true),
                    last_updated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartProducts", x => x.id);
                    table.ForeignKey(
                        name: "FK_cartProducts_cart_cartid",
                        column: x => x.cartid,
                        principalTable: "cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cartProducts_products_productid",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_ownerusername",
                table: "cart",
                column: "ownerusername");

            migrationBuilder.CreateIndex(
                name: "IX_cartProducts_cartid",
                table: "cartProducts",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_cartProducts_productid",
                table: "cartProducts",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_products_createdByusername",
                table: "products",
                column: "createdByusername");

            migrationBuilder.CreateIndex(
                name: "IX_products_productId",
                table: "products",
                column: "productId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartProducts");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
