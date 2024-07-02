﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesLibrary.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoviesLibrary.Migrations
{
    [DbContext(typeof(MoviesLibraryCContext))]
    partial class MoviesLibraryCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Movieactor", b =>
                {
                    b.Property<int>("Movieid")
                        .HasColumnType("integer")
                        .HasColumnName("movieid");

                    b.Property<int>("Actorid")
                        .HasColumnType("integer")
                        .HasColumnName("actorid");

                    b.HasKey("Movieid", "Actorid")
                        .HasName("movieactor_pkey");

                    b.HasIndex("Actorid");

                    b.ToTable("movieactor", (string)null);
                });

            modelBuilder.Entity("Moviecategory", b =>
                {
                    b.Property<int>("Movieid")
                        .HasColumnType("integer")
                        .HasColumnName("movieid");

                    b.Property<int>("Categoryid")
                        .HasColumnType("integer")
                        .HasColumnName("categoryid");

                    b.HasKey("Movieid", "Categoryid")
                        .HasName("moviecategory_pkey");

                    b.HasIndex("Categoryid");

                    b.ToTable("moviecategory", (string)null);
                });

            modelBuilder.Entity("MoviesLibrary.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("firstname");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("gender");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("lastname");

                    b.HasKey("Id")
                        .HasName("actors_pkey");

                    b.ToTable("actors", (string)null);
                });

            modelBuilder.Entity("MoviesLibrary.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Categoryname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("categoryname");

                    b.HasKey("Id")
                        .HasName("categories_pkey");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("MoviesLibrary.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Imageurl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("imageurl");

                    b.Property<int?>("Platformid")
                        .HasColumnType("integer")
                        .HasColumnName("platformid");

                    b.Property<DateOnly>("Premierdate")
                        .HasColumnType("date")
                        .HasColumnName("premierdate");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<DateOnly?>("Seendate")
                        .HasColumnType("date")
                        .HasColumnName("seendate");

                    b.Property<bool?>("Seenstatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("seenstatus");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("movies_pkey");

                    b.HasIndex("Platformid");

                    b.ToTable("movies", (string)null);
                });

            modelBuilder.Entity("MoviesLibrary.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Platformname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("platformname");

                    b.HasKey("Id")
                        .HasName("platforms_pkey");

                    b.ToTable("platforms", (string)null);
                });

            modelBuilder.Entity("Movieactor", b =>
                {
                    b.HasOne("MoviesLibrary.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("Actorid")
                        .IsRequired()
                        .HasConstraintName("movieactor_actorid_fkey");

                    b.HasOne("MoviesLibrary.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("Movieid")
                        .IsRequired()
                        .HasConstraintName("movieactor_movieid_fkey");
                });

            modelBuilder.Entity("Moviecategory", b =>
                {
                    b.HasOne("MoviesLibrary.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("Categoryid")
                        .IsRequired()
                        .HasConstraintName("moviecategory_categoryid_fkey");

                    b.HasOne("MoviesLibrary.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("Movieid")
                        .IsRequired()
                        .HasConstraintName("moviecategory_movieid_fkey");
                });

            modelBuilder.Entity("MoviesLibrary.Models.Movie", b =>
                {
                    b.HasOne("MoviesLibrary.Models.Platform", "Platform")
                        .WithMany("Movies")
                        .HasForeignKey("Platformid")
                        .HasConstraintName("movies_platformid_fkey");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("MoviesLibrary.Models.Platform", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
