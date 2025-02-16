using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Movie_Backend.Models
{
    public partial class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguracija konekcije s bazom podataka
            optionsBuilder.UseSqlServer("Server=DESKTOP-708U5M0\\SQLEXPRESS;Database=MovieDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Actors__3214EC075D679665");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Ostale konfiguracije svojstava
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Genres__3214EC07A6CF1B30");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Ostale konfiguracije svojstava
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Movies__3214EC07AFC4646A");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Konfiguracija veze sa žanrom
                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Movies_Genre");

                // Ostale konfiguracije svojstava
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07DD3FE6F7");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Konfiguracija veza s filmom i korisnikom
                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_Reviews_Movie");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reviews_User");

                // Ostale konfiguracije svojstava
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Series__3214EC07A59AB150");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Konfiguracija veze s žanrom i ostale konfiguracije svojstava
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0731164948");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Ostale konfiguracije svojstava
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
