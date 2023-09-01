using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fairy_project.Models
{
    public partial class woowoContext : DbContext
    {
        public woowoContext()
        {
        }

        public woowoContext(DbContextOptions<woowoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appliess> Appliesses { get; set; } = null!;
        public virtual DbSet<Areass> Areasses { get; set; } = null!;
        public virtual DbSet<BoothMapss> BoothMapsses { get; set; } = null!;
        public virtual DbSet<Exhibitionss> Exhibitionsses { get; set; } = null!;
        public virtual DbSet<Managerss> Managersses { get; set; } = null!;
        public virtual DbSet<Manufacturess> Manufacturesses { get; set; } = null!;
        public virtual DbSet<Memberss> Membersses { get; set; } = null!;
        public virtual DbSet<Permissionss> Permissionsses { get; set; } = null!;
        public virtual DbSet<Ticketss> Ticketsses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=tcp:pohaniii.database.windows.net,1433;Initial Catalog=woowo;Persist Security Info=False;User ID=ispan;Password=P@ssw0rd-iii;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=woowo;Integrated Security=SSPI;Trusted_Connection=Yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appliess>(entity =>
            {
                entity.HasKey(e => e.ApplyNum);

                entity.ToTable("Appliess");

                entity.Property(e => e.ApplyNum).HasColumnName("applyNum");

                entity.Property(e => e.BoothNumber).HasColumnName("boothNumber");

                entity.Property(e => e.CheckState).HasColumnName("checkState");

                entity.Property(e => e.EId).HasColumnName("e_Id");

                entity.Property(e => e.Message)
                    .HasMaxLength(150)
                    .HasColumnName("message");

                entity.Property(e => e.MfDescription).HasColumnName("mf_Description");

                entity.Property(e => e.MfId).HasColumnName("mf_Id");

                entity.Property(e => e.MfLogo).HasColumnName("mf_logo");

                entity.Property(e => e.MfPImg).HasColumnName("mf_P_img");

                entity.Property(e => e.PayTime)
                    .HasPrecision(0)
                    .HasColumnName("payTime");
            });

            modelBuilder.Entity<Areass>(entity =>
            {
                entity.HasKey(e => e.AreaNumber);

                entity.ToTable("areass");

                entity.Property(e => e.AreaNumber).HasColumnName("areaNumber");

                entity.Property(e => e.AreaSize).HasColumnName("areaSize");

                entity.Property(e => e.LimitBooth).HasColumnName("limitBooth");

                entity.Property(e => e.LimitPeople).HasColumnName("limitPeople");
            });

            modelBuilder.Entity<BoothMapss>(entity =>
            {
                entity.HasKey(e => e.SerialNumber);

                entity.ToTable("boothMapss");

                entity.Property(e => e.SerialNumber).HasColumnName("serialNumber");

                entity.Property(e => e.BoothLv).HasColumnName("boothLv");

                entity.Property(e => e.BoothNumber).HasColumnName("boothNumber");

                entity.Property(e => e.BoothPrice).HasColumnName("boothPrice");

                entity.Property(e => e.BoothState).HasColumnName("boothState");

                entity.Property(e => e.EId).HasColumnName("e_Id");

                entity.Property(e => e.MfId).HasColumnName("mf_Id");

                entity.Property(e => e.MfLogo).HasColumnName("mf_logo");

                entity.Property(e => e.MfPImg).HasColumnName("mf_P_img");
            });

            modelBuilder.Entity<Exhibitionss>(entity =>
            {
                entity.HasKey(e => e.ExhibitId);

                entity.ToTable("exhibitionss");

                entity.Property(e => e.ExhibitId).HasColumnName("exhibitId");

                entity.Property(e => e.AreaNum).HasColumnName("areaNum");

                entity.Property(e => e.Datefrom).HasColumnName("datefrom");

                entity.Property(e => e.Dateto).HasColumnName("dateto");

                entity.Property(e => e.ExDescription).HasColumnName("ex_Description");

                entity.Property(e => e.ExPersonTime).HasColumnName("ex_personTime");

                entity.Property(e => e.ExTotalImcome).HasColumnName("ex_totalImcome");

                entity.Property(e => e.ExhibitName).HasColumnName("exhibitName");

                entity.Property(e => e.ExhibitPImg).HasColumnName("exhibit_P_img");

                entity.Property(e => e.ExhibitPreImg).HasColumnName("exhibit_Pre_img");

                entity.Property(e => e.ExhibitStatus).HasColumnName("exhibitStatus");

                entity.Property(e => e.ExhibitTImg).HasColumnName("exhibit_T_img");

                entity.Property(e => e.TicketPrice).HasColumnName("ticket_Price");
            });

            modelBuilder.Entity<Managerss>(entity =>
            {
                entity.HasKey(e => e.ManagerId);

                entity.ToTable("managerss");

                entity.Property(e => e.ManagerId).HasColumnName("managerId");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Center).HasColumnName("center");

                entity.Property(e => e.CenterEmail).HasColumnName("centerEmail");

                entity.Property(e => e.ManagerName).HasColumnName("managerName");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Manufacturess>(entity =>
            {
                entity.HasKey(e => e.ManufactureId);

                entity.ToTable("manufacturess");

                entity.Property(e => e.ManufactureId).HasColumnName("manufactureId");

                entity.Property(e => e.ManufactureAcc).HasColumnName("manufactureAcc");

                entity.Property(e => e.ManufactureName).HasColumnName("manufactureName");

                entity.Property(e => e.MfEmail).HasColumnName("mfEmail");

                entity.Property(e => e.MfPerson).HasColumnName("mfPerson");

                entity.Property(e => e.MfPhoneNum).HasColumnName("mfPhoneNum");
            });

            modelBuilder.Entity<Memberss>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("memberss");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.MemberAc)
                    .HasMaxLength(250)
                    .HasColumnName("memberAc");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(50)
                    .HasColumnName("memberName");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Permissionss>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("Permissionss");

                entity.Property(e => e.Account)
                    .HasMaxLength(250)
                    .HasColumnName("account");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.PermissionsLv).HasColumnName("permissionsLv");
            });

            modelBuilder.Entity<Ticketss>(entity =>
            {
                entity.HasKey(e => e.OrderNum);

                entity.ToTable("ticketss");

                entity.Property(e => e.OrderNum).HasColumnName("orderNum");

                entity.Property(e => e.EId).HasColumnName("e_Id");

                entity.Property(e => e.Enterstate).HasColumnName("enterstate");

                entity.Property(e => e.Entertime).HasColumnName("entertime");

                entity.Property(e => e.MId).HasColumnName("m_Id");

                entity.Property(e => e.Ordertime).HasColumnName("ordertime");

                entity.Property(e => e.PayTime).HasColumnName("payTime");

                entity.Property(e => e.PersonNumber).HasColumnName("personNumber");

                entity.Property(e => e.PresonName).HasColumnName("presonName");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.VerificationCode).HasColumnName("verificationCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
