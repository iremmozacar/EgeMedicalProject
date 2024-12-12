using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace EgeApp.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')"),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    SalesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDiscounted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFreeShipping = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsSpecialProduct = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsSameDayShipping = table.Column<bool>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsHome = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')"),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "date('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2670), "1" },
                    { 2, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680), "2" },
                    { 3, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680), "3" },
                    { 4, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680), "4" },
                    { 5, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2690), "5" },
                    { 6, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2690), "6" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "IsActive", "ModifiedDate", "Name", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), "Ortopedik ürünler", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), "Ortopedik Ürünler", "ortopedik-urunler" },
                    { 2, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), "Solunum cihazları", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), "Solunum Cihazları", "solunum-cihazlari" },
                    { 3, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Solunum maskeleri", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Solunum Maskeleri", "solunum-maskeleri" },
                    { 4, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Hasta bakım ürünleri", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Hasta Bakım Ürünleri", "hasta-bakim-urunleri" },
                    { 5, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Tıbbi test ve sarf malzemeleri", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Tıbbi Test ve Sarf Malzemeleri", "tibbi-test-ve-sarf-malzemeleri" },
                    { 6, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), "Tansiyon ve nabız ölçüm cihazları", null, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4050), "Tansiyon ve Nabız Ölçüm Cihazları", "tansiyon-ve-nabiz-olcum-cihazlari" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "CreatedDate", "Description", "DiscountedPrice", "ImageUrl", "IsActive", "IsDiscounted", "IsFreeShipping", "IsHome", "IsSameDayShipping", "IsSpecialProduct", "ModifiedDate", "Name", "Price", "SalesCount", "Url" },
                values: new object[,]
                {
                    { 1, "KorseMarka", 1, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5950), "Diz ve sırt destekleyici", null, "http://localhost:5200/images/products/dorselumberkorse.webp", true, true, true, true, true, false, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5950), "Dorselumber Korse", 1000m, 0, "dorselumber-korse" },
                    { 2, "SolunumMarka", 2, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5960), "Profesyonel solunum cihazı", null, "http://localhost:5200/images/products/devisbissoksijen.webp", true, false, true, true, true, false, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5960), "Devisbiss Oksijen Konsantratörü", 8000m, 0, "devisbiss-oksijen-konsantratoru" },
                    { 3, "MaskeMarka", 3, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5970), "Solunum desteği için maske", null, "http://localhost:5200/images/products/tamyuzmaskesi.webp", true, false, false, true, true, true, new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5970), "Tam Yüz Maskesi", 500m, 0, "tam-yuz-maskesi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
