﻿// <auto-generated />
using EnumApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnumApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240618163123_InitToDb")]
    partial class InitToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("EnumApp.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Notifications");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Level = 1
                        },
                        new
                        {
                            Id = 2,
                            Level = 2
                        },
                        new
                        {
                            Id = 3,
                            Level = 3
                        },
                        new
                        {
                            Id = 4,
                            Level = 4
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
