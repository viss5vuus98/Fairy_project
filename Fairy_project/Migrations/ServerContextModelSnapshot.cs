﻿// <auto-generated />
using System;
using Fairy_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fairy_project.Migrations
{
    [DbContext(typeof(ServerContext))]
    partial class ServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fairy_project.Models.Apply", b =>
                {
                    b.Property<int>("applyNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("applyNum"), 1L, 1);

                    b.Property<int?>("boothNumber")
                        .HasColumnType("int");

                    b.Property<int?>("checkState")
                        .HasColumnType("int");

                    b.Property<int?>("e_Id")
                        .HasColumnType("int");

                    b.Property<string>("mf_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("mf_Id")
                        .HasColumnType("int");

                    b.Property<string>("mf_P_img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mf_logo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("applyNum");

                    b.ToTable("Applies");
                });

            modelBuilder.Entity("Fairy_project.Models.Area", b =>
                {
                    b.Property<int>("areaNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("areaNumber"), 1L, 1);

                    b.Property<string>("areaSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("limitBooth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("limitPeople")
                        .HasColumnType("int");

                    b.HasKey("areaNumber");

                    b.ToTable("areas");
                });

            modelBuilder.Entity("Fairy_project.Models.Booths", b =>
                {
                    b.Property<int>("serialNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("serialNumber"), 1L, 1);

                    b.Property<int?>("boothLv")
                        .HasColumnType("int");

                    b.Property<int?>("boothNumber")
                        .HasColumnType("int");

                    b.Property<int?>("boothPrice")
                        .HasColumnType("int");

                    b.Property<int?>("boothState")
                        .HasColumnType("int");

                    b.Property<int?>("e_Id")
                        .HasColumnType("int");

                    b.Property<int?>("mf_Id")
                        .HasColumnType("int");

                    b.Property<string>("mf_P_img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mf_logo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("serialNumber");

                    b.ToTable("boothMaps");
                });

            modelBuilder.Entity("Fairy_project.Models.Exhibition", b =>
                {
                    b.Property<int>("exhibitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("exhibitId"), 1L, 1);

                    b.Property<int?>("areaNum")
                        .HasColumnType("int");

                    b.Property<DateTime?>("datefrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateto")
                        .HasColumnType("datetime2");

                    b.Property<string>("ex_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ex_personTime")
                        .HasColumnType("int");

                    b.Property<int?>("ex_totalImcome")
                        .HasColumnType("int");

                    b.Property<string>("exhibitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("exhibitStatus")
                        .HasColumnType("int");

                    b.Property<string>("exhibit_P_img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("exhibit_Pre_img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("exhibit_T_img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ticket_Price")
                        .HasColumnType("int");

                    b.HasKey("exhibitId");

                    b.ToTable("exhibitions");
                });

            modelBuilder.Entity("Fairy_project.Models.Manager", b =>
                {
                    b.Property<int>("managerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("managerId"), 1L, 1);

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("center")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("centerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("managerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("managerId");

                    b.ToTable("managers");
                });

            modelBuilder.Entity("Fairy_project.Models.Manufactures", b =>
                {
                    b.Property<int>("manufactureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("manufactureId"), 1L, 1);

                    b.Property<string>("manufactureAcc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("manufactureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mfEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mfPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mfPhoneNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("manufactureId");

                    b.ToTable("manufactures");
                });

            modelBuilder.Entity("Fairy_project.Models.Member", b =>
                {
                    b.Property<int>("memberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("memberId"), 1L, 1);

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("memberAc")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("memberName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("memberId");

                    b.ToTable("members");
                });

            modelBuilder.Entity("Fairy_project.Models.Permissions", b =>
                {
                    b.Property<string>("account")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("permissionsLv")
                        .HasColumnType("int");

                    b.HasKey("account");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Fairy_project.Models.Ticket", b =>
                {
                    b.Property<int>("orderNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderNum"), 1L, 1);

                    b.Property<int?>("e_Id")
                        .HasColumnType("int");

                    b.Property<int>("enterstate")
                        .HasColumnType("int");

                    b.Property<DateTime?>("entertime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("m_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ordertime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("payTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("personNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("presonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("orderNum");

                    b.ToTable("tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
