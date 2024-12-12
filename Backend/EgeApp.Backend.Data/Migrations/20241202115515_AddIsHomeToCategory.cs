using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeApp.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsHomeToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 2, 14, 55, 15, 33, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "IsHome", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(650), true, new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "IsHome", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(650), true, new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "IsHome", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660), true, new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(670), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(670) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2530), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2530), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2540), new DateTime(2024, 12, 2, 14, 55, 15, 34, DateTimeKind.Local).AddTicks(2540) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(2690));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5950), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5960), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5970), new DateTime(2024, 12, 1, 21, 25, 20, 410, DateTimeKind.Local).AddTicks(5970) });
        }
    }
}
