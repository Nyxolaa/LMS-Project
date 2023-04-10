﻿// <auto-generated />
using LMS_Project_mvc.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS_Project_mvc.Migrations
{
    [DbContext(typeof(MediaDB))]
    [Migration("20230410124040_archive")]
    partial class archive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LMS_Project_mvc.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CategoryId");

                    b.ToTable("MediaCategories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Genel"
                        });
                });

            modelBuilder.Entity("LMS_Project_mvc.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("FileSize")
                        .HasColumnType("double");

                    b.Property<string>("FileSizeHuman")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MTypeId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("LMS_Project_mvc.Models.MType", b =>
                {
                    b.Property<int>("MTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MTypeId");

                    b.ToTable("MediaTypes");

                    b.HasData(
                        new
                        {
                            MTypeId = 1,
                            TypeName = "Video"
                        },
                        new
                        {
                            MTypeId = 2,
                            TypeName = "Podcast"
                        },
                        new
                        {
                            MTypeId = 3,
                            TypeName = "Scorm Paketi"
                        });
                });

            modelBuilder.Entity("LMS_Project_mvc.Models.Media", b =>
                {
                    b.HasOne("LMS_Project_mvc.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS_Project_mvc.Models.MType", "MType")
                        .WithMany()
                        .HasForeignKey("MTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("MType");
                });
#pragma warning restore 612, 618
        }
    }
}