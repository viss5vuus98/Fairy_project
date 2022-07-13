using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairy_project.Migrations
{
    public partial class _222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booths",
                columns: table => new
                {
                    boothId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_Id = table.Column<int>(type: "int", nullable: false),
                    mf_Id = table.Column<int>(type: "int", nullable: false),
                    boothImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_P_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    checkStatus = table.Column<int>(type: "int", nullable: false),
                    boothNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booths", x => x.boothId);
                });

            migrationBuilder.CreateTable(
                name: "exhibitions",
                columns: table => new
                {
                    exhibitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exhibitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    exhibitStatus = table.Column<int>(type: "int", nullable: false),
                    exhibit_P_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exhibit_T_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exhibit_Pre_img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exhibitions", x => x.exhibitId);
                });

            migrationBuilder.CreateTable(
                name: "manufactures",
                columns: table => new
                {
                    manufactureName = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    manufactureAcc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mfPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mfEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mfPhoneNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufactures", x => x.manufactureName);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    memberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberAc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    memberName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.memberId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    account = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    permissionsLv = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.account);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_Id = table.Column<int>(type: "int", nullable: false),
                    m_Id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.orderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booths");

            migrationBuilder.DropTable(
                name: "exhibitions");

            migrationBuilder.DropTable(
                name: "manufactures");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "tickets");
        }
    }
}
