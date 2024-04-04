using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomethingFruity.Migrations
{
    public partial class normalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "UserId");

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
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3093));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3097));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3096));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 35, 46, 888, DateTimeKind.Local).AddTicks(3097));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4da80dd1-bcf3-47db-982a-d000f5b14583", 0, "03bcd765-23e8-4680-bd24-b010569d9e45", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "Passw0rd!", null, null, false, "6dbe8939-70e6-40cc-a80d-b5d75cdcfa38", false, "user@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4da80dd1-bcf3-47db-982a-d000f5b14583");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8550));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8562));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8586));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8589));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8587));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 4, 4, 8, 7, 12, 254, DateTimeKind.Local).AddTicks(8588));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "UserId", 0, "62954e15-e43b-478d-a3a3-a5f9618ae8ba", "user@gmail.com", false, false, null, null, null, "Passw0rd!", null, null, false, "30ff9fa9-fd07-4a0d-a9e5-9baf1e18e60b", false, "user@gmail.com" });
        }
    }
}
