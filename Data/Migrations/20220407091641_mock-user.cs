using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFicFabliaux.Data.Migrations
{
    public partial class mockuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d7be6443-40ac-4998-b1a8-b8d0c5b2d991", 0, "604265ea-13d5-44ad-88b5-e409297baf85", null, false, false, null, null, null, null, null, false, "4e9a3696-8a53-4bec-9f14-bb8f3ab789b7", false, "J.R.R. Tolkien" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "UserId" },
                values: new object[] { null, "d7be6443-40ac-4998-b1a8-b8d0c5b2d991" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "UserId" },
                values: new object[] { null, "d7be6443-40ac-4998-b1a8-b8d0c5b2d991" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "UserId" },
                values: new object[] { null, "d7be6443-40ac-4998-b1a8-b8d0c5b2d991" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7be6443-40ac-4998-b1a8-b8d0c5b2d991");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "UserId" },
                values: new object[] { "J.R.R. Tolkien", null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "UserId" },
                values: new object[] { "J.R.R. Tolkien", null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "UserId" },
                values: new object[] { "J.R.R. Tolkien", null });
        }
    }
}
