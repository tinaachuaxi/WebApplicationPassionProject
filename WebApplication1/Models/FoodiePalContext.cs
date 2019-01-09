using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class FoodiePalContext : DbContext
    {
        public FoodiePalContext()
        {
        }

        public FoodiePalContext(DbContextOptions<FoodiePalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuisin> Cuisin { get; set; }
        public virtual DbSet<Resturant> Resturant { get; set; }
        public virtual DbSet<ResturantCuisin> ResturantCuisin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=FoodiePal;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuisin>(entity =>
            {
                entity.HasKey(e => e.CuiName);

                entity.ToTable("cuisin");

                entity.Property(e => e.CuiName)
                    .HasColumnName("cui_name")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descript)
                    .HasColumnName("descript")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resturant>(entity =>
            {
                entity.HasKey(e => e.RestId);

                entity.ToTable("resturant");

                entity.Property(e => e.RestId).HasColumnName("rest_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RestAddress)
                    .HasColumnName("rest_address")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RestName)
                    .HasColumnName("rest_name")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ResturantCuisin>(entity =>
            {
                entity.HasKey(e => new { e.RestId, e.CuiName });

                entity.ToTable("resturant_cuisin");

                entity.Property(e => e.RestId).HasColumnName("rest_id");

                entity.Property(e => e.CuiName)
                    .HasColumnName("cui_name")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.CuiNameNavigation)
                    .WithMany(p => p.ResturantCuisin)
                    .HasForeignKey(d => d.CuiName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__resturant__cui_n__534D60F1");

                entity.HasOne(d => d.Rest)
                    .WithMany(p => p.ResturantCuisin)
                    .HasForeignKey(d => d.RestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__resturant__rest___52593CB8");
            });
        }
    }
}
