using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairy_project.Migrations
{
    public partial class datefrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "exhibitions",
                newName: "dateto");

            migrationBuilder.AddColumn<DateTime>(
                name: "datefrom",
                table: "exhibitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datefrom",
                table: "exhibitions");

            migrationBuilder.RenameColumn(
                name: "dateto",
                table: "exhibitions",
                newName: "Date");
        }
    }
}
