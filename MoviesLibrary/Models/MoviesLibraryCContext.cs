using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesLibrary.Models;

public partial class MoviesLibraryCContext : DbContext
{
    public MoviesLibraryCContext()
    {
    }

    public MoviesLibraryCContext(DbContextOptions<MoviesLibraryCContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=MoviesLibraryC;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actors_pkey");

            entity.ToTable("actors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender").HasConversion(
                v => v.ToString(),
                v => (Gender)Enum.Parse(typeof(Gender), v)); 
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(255)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movies_pkey");

            entity.ToTable("movies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(255)
                .HasColumnName("imageurl");
            entity.Property(e => e.Platformid).HasColumnName("platformid");
            entity.Property(e => e.Premierdate).HasColumnName("premierdate");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Seendate).HasColumnName("seendate");
            entity.Property(e => e.Seenstatus)
                .HasDefaultValue(false)
                .HasColumnName("seenstatus");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Platform).WithMany(p => p.Movies)
                .HasForeignKey(d => d.Platformid)
                .HasConstraintName("movies_platformid_fkey");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Movieactor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("Actorid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("movieactor_actorid_fkey"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("Movieid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("movieactor_movieid_fkey"),
                    j =>
                    {
                        j.HasKey("Movieid", "Actorid").HasName("movieactor_pkey");
                        j.ToTable("movieactor");
                        j.IndexerProperty<int>("Movieid").HasColumnName("movieid");
                        j.IndexerProperty<int>("Actorid").HasColumnName("actorid");
                    });

            entity.HasMany(d => d.Categories).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Moviecategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("Categoryid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("moviecategory_categoryid_fkey"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("Movieid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("moviecategory_movieid_fkey"),
                    j =>
                    {
                        j.HasKey("Movieid", "Categoryid").HasName("moviecategory_pkey");
                        j.ToTable("moviecategory");
                        j.IndexerProperty<int>("Movieid").HasColumnName("movieid");
                        j.IndexerProperty<int>("Categoryid").HasColumnName("categoryid");
                    });
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("platforms_pkey");

            entity.ToTable("platforms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Platformname)
                .HasMaxLength(255)
                .HasColumnName("platformname");      
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
