using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeApp.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDetailsToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 12, 12, 8, 45, 209, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(640), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(640) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(640), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(660), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2460), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2470), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2470) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2470), new DateTime(2024, 12, 12, 12, 8, 45, 210, DateTimeKind.Local).AddTicks(2470) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(4200));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6570), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6590), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6590), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6600), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6600), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6610), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9740), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9750), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9760), new DateTime(2024, 12, 5, 19, 7, 17, 816, DateTimeKind.Local).AddTicks(9760) });
        }
    }
}
