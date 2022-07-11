using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairy_project.Migrations
{
    public partial class _220711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "manufacture",
                table: "manufactures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "manufacture",
                table: "manufactures");
        }
    }
}
