using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairy_project.Migrations
{
    public partial class ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "enterstate",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "entertime",
                table: "tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ordertime",
                table: "tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enterstate",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "entertime",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "ordertime",
                table: "tickets");
        }
    }
}
