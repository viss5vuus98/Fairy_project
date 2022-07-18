using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fairy_project.Migrations
{
    public partial class _0718 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applies",
                columns: table => new
                {
                    applyNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_Id = table.Column<int>(type: "int", nullable: true),
                    mf_Id = table.Column<int>(type: "int", nullable: true),
                    boothNumber = table.Column<int>(type: "int", nullable: true),
                    checkState = table.Column<int>(type: "int", nullable: true),
                    mf_logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_P_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applies", x => x.applyNum);
                });

            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    areaNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    areaSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    limitBooth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    limitPeople = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.areaNumber);
                });

            migrationBuilder.CreateTable(
                name: "boothMaps",
                columns: table => new
                {
                    serialNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    boothNumber = table.Column<int>(type: "int", nullable: true),
                    e_Id = table.Column<int>(type: "int", nullable: true),
                    mf_Id = table.Column<int>(type: "int", nullable: true),
                    boothState = table.Column<int>(type: "int", nullable: true),
                    boothLv = table.Column<int>(type: "int", nullable: true),
                    mf_logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mf_P_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    boothPrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boothMaps", x => x.serialNumber);
                });

            migrationBuilder.CreateTable(
                name: "exhibitions",
                columns: table => new
                {
                    exhibitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exhibitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datefrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateto = table.Column<DateTime>(type: "datetime2", nullable: true),
                    exhibitStatus = table.Column<int>(type: "int", nullable: true),
                    exhibit_P_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exhibit_T_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exhibit_Pre_img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    areaNum = table.Column<int>(type: "int", nullable: true),
                    ex_personTime = table.Column<int>(type: "int", nullable: true),
                    ex_totalImcome = table.Column<int>(type: "int", nullable: true),
                    ex_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ticket_Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exhibitions", x => x.exhibitId);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    managerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    center = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    managerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    centerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.managerId);
                });

            migrationBuilder.CreateTable(
                name: "manufactures",
                columns: table => new
                {
                    manufactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    manufactureAcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    manufactureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mfPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mfEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mfPhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufactures", x => x.manufactureId);
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
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    permissionsLv = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.account);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    orderNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_Id = table.Column<int>(type: "int", nullable: true),
                    m_Id = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    enterstate = table.Column<int>(type: "int", nullable: false),
                    entertime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ordertime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    presonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.orderNum);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applies");

            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "boothMaps");

            migrationBuilder.DropTable(
                name: "exhibitions");

            migrationBuilder.DropTable(
                name: "managers");

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
