using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShort.Migrations
{
    public partial class AboutText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutTexts",
                columns: table => new
                {
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutTexts");
        }
    }
}
