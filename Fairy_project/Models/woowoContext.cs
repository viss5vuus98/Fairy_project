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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:wowoo.database.windows.net,1433;Initial Catalog=woowo;Persist Security Info=False;User ID=ispanwo;Password=P@ssw0rd-iii;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
