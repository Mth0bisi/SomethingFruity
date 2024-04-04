using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomethingFruity.Migrations
{
    public partial class priceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4275), 34.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4280), 32.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4277), 45.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 49, 3, 291, DateTimeKind.Local).AddTicks(4279), 28.0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4da80dd1-bcf3-47db-982a-d000f5b14583",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c4eaf9f-50f2-461a-9839-75e127b74102", "b0eb471e-abc5-4dd8-8bdb-b0af7f68b387" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3076));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3077));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3093), 34m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3097), 32m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3096), 45m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "DateCreated", "Price" },
                values: new object[] { new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3097), 28m });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4da80dd1-bcf3-47db-982a-d000f5b14583",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03bcd765-23e8-4680-bd24-b010569d9e45", "6dbe8939-70e6-40cc-a80d-b5d75cdcfa38" });
        }
    }
}
