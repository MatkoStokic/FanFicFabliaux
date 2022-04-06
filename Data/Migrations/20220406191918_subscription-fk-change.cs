using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFicFabliaux.Data.Migrations
{
    public partial class subscriptionfkchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Books_BookId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_BookId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AuthorId",
                table: "Subscriptions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_AuthorId",
                table: "Subscriptions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_AuthorId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_AuthorId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_BookId",
                table: "Subscriptions",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Books_BookId",
                table: "Subscriptions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
