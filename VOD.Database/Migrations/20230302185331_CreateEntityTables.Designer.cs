﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VOD.Database.Contexts;

#nullable disable

namespace VOD.Database.Migrations
{
    [DbContext(typeof(VODContext))]
    [Migration("20230302185331_CreateEntityTables")]
    partial class CreateEntityTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VOD.Database.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("VOD.Database.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("FilmUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Free")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("VOD.Database.Entities.FilmGenre", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenres", (string)null);
                });

            modelBuilder.Entity("VOD.Database.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("VOD.Database.Entities.SimilarFilm", b =>
                {
                    b.Property<int>("ParentFilmId")
                        .HasColumnType("int");

                    b.Property<int>("SimilarFilmId")
                        .HasColumnType("int");

                    b.HasKey("ParentFilmId", "SimilarFilmId");

                    b.HasIndex("SimilarFilmId");

                    b.ToTable("SimilarFilms");
                });

            modelBuilder.Entity("VOD.Database.Entities.Film", b =>
                {
                    b.HasOne("VOD.Database.Entities.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("VOD.Database.Entities.FilmGenre", b =>
                {
                    b.HasOne("VOD.Database.Entities.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VOD.Database.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("VOD.Database.Entities.SimilarFilm", b =>
                {
                    b.HasOne("VOD.Database.Entities.Film", "Film")
                        .WithMany("SimilarFilms")
                        .HasForeignKey("ParentFilmId")
                        .IsRequired();

                    b.HasOne("VOD.Database.Entities.Film", "Similar")
                        .WithMany()
                        .HasForeignKey("SimilarFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Similar");
                });

            modelBuilder.Entity("VOD.Database.Entities.Film", b =>
                {
                    b.Navigation("SimilarFilms");
                });
#pragma warning restore 612, 618
        }
    }
}
