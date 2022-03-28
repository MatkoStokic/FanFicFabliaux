using Microsoft.EntityFrameworkCore.Migrations;

namespace FanFicFabliaux.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title" },
                values: new object[] { 1, null, "LOTR: Fellowship Of The Ring" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title" },
                values: new object[] { 2, null, "LOTR: Two Towers" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title" },
                values: new object[] { 3, null, "LOTR: The Return Of The King" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
