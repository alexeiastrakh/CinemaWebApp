using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaWebApplication
{
    public partial class DBCinemaContext : DbContext
    {
        public DBCinemaContext()
        {
        }

        public DBCinemaContext(DbContextOptions<DBCinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<ActrorsRole> ActrorsRoles { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MoviesActorsRole> MoviesActorsRoles { get; set; } = null!;
        public virtual DbSet<MoviesGenre> MoviesGenres { get; set; } = null!;
        public virtual DbSet<MoviesProducer> MoviesProducers { get; set; } = null!;
        public virtual DbSet<Producer> Producers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-DMI7F36; Database=DBCinema; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ActrorsRole>(entity =>
            {
                entity.HasOne(d => d.Actors)
                    .WithMany(p => p.ActrorsRoles)
                    .HasForeignKey(d => d.ActorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActrorsRoles_Actors");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.ActrorsRoles)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActrorsRoles_Roles");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.DateofRealese).HasColumnType("date");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_Countries");
            });

            modelBuilder.Entity<MoviesActorsRole>(entity =>
            {
                entity.HasOne(d => d.ActorsRoles)
                    .WithMany(p => p.MoviesActorsRoles)
                    .HasForeignKey(d => d.ActorsRolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesActorsRoles_ActrorsRoles");

                entity.HasOne(d => d.Movies)
                    .WithMany(p => p.MoviesActorsRoles)
                    .HasForeignKey(d => d.MoviesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesActorsRoles_Movies");
            });

            modelBuilder.Entity<MoviesGenre>(entity =>
            {
                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_MoviesGenres_Genres");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesGenres_Movies");
            });

            modelBuilder.Entity<MoviesProducer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MoviesId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Movies)
                    .WithMany(p => p.MoviesProducers)
                    .HasForeignKey(d => d.MoviesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesProducers_Movies");

                entity.HasOne(d => d.Producers)
                    .WithMany(p => p.MoviesProducers)
                    .HasForeignKey(d => d.ProducersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesProducers_Producers");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
