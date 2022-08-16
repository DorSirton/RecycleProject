using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RecyclingProject.Data.Model;

namespace RecyclingProject.Data.Contexts
{
    public partial class RecycleProjectDataContext : DbContext
    {
        public RecycleProjectDataContext()
        {
        }

        public RecycleProjectDataContext(DbContextOptions<RecycleProjectDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Bottle> Bottles { get; set; } = null!;
        public virtual DbSet<BottleCategory> BottleCategories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Collector> Collectors { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Recycler> Recyclers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RecycleProjectDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Addresses__City___4222D4EF");
            });

            modelBuilder.Entity<Bottle>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Bottles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bottles__Categor__4316F928");

                entity.HasOne(d => d.Collector)
                    .WithMany(p => p.Bottles)
                    .HasForeignKey(d => d.CollectorId)
                    .HasConstraintName("FK__Bottles__Collect__440B1D61");

                entity.HasOne(d => d.Recycler)
                    .WithMany(p => p.Bottles)
                    .HasForeignKey(d => d.RecyclerId)
                    .HasConstraintName("FK__Bottles__Recycle__44FF419A");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Cities__Country___45F365D3");
            });

            modelBuilder.Entity<Collector>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Collectors)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Collector__Addre__46E78A0C");
            });

            modelBuilder.Entity<Recycler>(entity =>
            {
                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Recyclers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recyclers__Addre__47DBAE45");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
