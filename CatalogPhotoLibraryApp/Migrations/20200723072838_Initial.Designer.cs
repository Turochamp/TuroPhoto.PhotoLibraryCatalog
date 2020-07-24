﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TuroPhoto.CatalogPhotoLibraryApp.Infrastructure.Repository;

namespace Turochamp.Photo.Migrations
{
    [DbContext(typeof(AlbumIndexContext))]
    [Migration("20200723072838_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Turochamp.Photo.Model.AlbumIndex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComputerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DirectoryPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AlbumIndex");
                });

            modelBuilder.Entity("Turochamp.Photo.Model.Directory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumIndexId")
                        .HasColumnType("int");

                    b.Property<string>("RelativePath")
                        .HasColumnName("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumIndexId");

                    b.ToTable("Directories");
                });

            modelBuilder.Entity("Turochamp.Photo.Model.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumIndexId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeFromFile")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DirectoryId")
                        .HasColumnType("int");

                    b.Property<string>("RelativeSourceFilePath")
                        .HasColumnName("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumIndexId");

                    b.HasIndex("DirectoryId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Turochamp.Photo.Model.Directory", b =>
                {
                    b.HasOne("Turochamp.Photo.Model.AlbumIndex", null)
                        .WithMany("Directories")
                        .HasForeignKey("AlbumIndexId");
                });

            modelBuilder.Entity("Turochamp.Photo.Model.Photo", b =>
                {
                    b.HasOne("Turochamp.Photo.Model.AlbumIndex", null)
                        .WithMany("Photos")
                        .HasForeignKey("AlbumIndexId");

                    b.HasOne("Turochamp.Photo.Model.Directory", null)
                        .WithMany("Photos")
                        .HasForeignKey("DirectoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
