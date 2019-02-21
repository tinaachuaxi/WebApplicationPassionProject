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
                optionsBuilder.UseSqlite("Data Source=.\\wwwroot\\sql.db");
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

            modelBuilder.Entity<Cuisin>().HasData(
                new
                {
                    CuiName = "Chinese", Description="Cuisine of Chinese"
                },
                new
                {
                    CuiName = "Korean",
                    Description = "Korean food"
                },
                new
                {
                    CuiName = "Canadian",
                    Description = "Canadian food"
                },
                new
                {
                    CuiName = "Malaysian",
                    Description = "Malaysian food"
                }
                );

            modelBuilder.Entity<Resturant>().HasData(
                new
                {
                    RestId = 1,
                    RestName= "Peaceful Resturant",
                    RestAddress = "2222 Seymour Street",
                    PhoneNumber = "222-454-5345",
                    Email = "peaceful@resturant"
                },
                new
                {
                    RestId = 2,
                    RestName = "Sura",
                    RestAddress = "2222 Robson Street",
                    PhoneNumber = "222-454-5345",
                    Email = "peaceful@resturant"
                },
                new
                {
                    RestId = 3,
                    RestName = "A&W",
                    RestAddress = "2222 Richards Street",
                    PhoneNumber = "222-454-5345",
                    Email = "peaceful@resturant"
                },
                new
                {
                    RestId = 4,
                    RestName = "Banana Republic",
                    RestAddress = "2222 Homer Street",
                    PhoneNumber = "222-454-5345",
                    Email = "peaceful@resturant"
                },
                new
                {
                    RestId = 5,
                    RestName = "Dynasty",
                    RestAddress = "2222 Howe Street",
                    PhoneNumber = "222-454-5345",
                    Email = "peaceful@resturant"
                });

            modelBuilder.Entity<ResturantCuisin>().HasData(
                new
                {
                    CuiName = "Korean",
                    RestId = 2
                },
                new
                {
                    CuiName = "Chinese",
                    RestId = 1
                },
                new
                {
                    CuiName = "Malaysian",
                    RestId = 4
                },
                new
                {
                    CuiName = "Canadian",
                    RestId = 3
                },
                new
                {
                    CuiName = "Chinese",
                    RestId = 5
                },
                new
                {
                    CuiName = "Chinese",
                    RestId = 2
                });
        }
    }
}
