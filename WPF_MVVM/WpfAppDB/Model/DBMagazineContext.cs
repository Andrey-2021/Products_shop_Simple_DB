using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using System.Threading.Tasks;
using WpfAppDB.Classes;

namespace WpfAppDB.Model
{
    /// <summary>
    /// Класс контекста
    /// </summary>
    public partial class DBMagazineContext : DbContext
    {

        public DBMagazineContext()
        {
        }

        public DBMagazineContext(DbContextOptions<DBMagazineContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=KOMP2021;Initial Catalog=DBMagazine;Trusted_Connection=True;");
            //}

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ShopsId, "IX_Products_ProductID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Shops)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Products_To_Shops");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasIndex(e => e.Name, "IX_Shops_Name");

                entity.HasIndex(e => e.Email, "UQ_Client_Email")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
