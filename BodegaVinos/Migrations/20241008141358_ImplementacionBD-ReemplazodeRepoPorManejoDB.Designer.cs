﻿// <auto-generated />
using System;
using BodegaVinos.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BodegaVinos.Migrations
{
    [DbContext(typeof(WineDbContext))]
    [Migration("20241008141358_ImplementacionBD-ReemplazodeRepoPorManejoDB")]
    partial class ImplementacionBDReemplazodeRepoPorManejoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BodegaVinos.Entities.WineEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Variety")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Wines");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            CreatedAt = new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(174),
                            Name = "Luigi Bosca Malbec",
                            Region = "Mendoza",
                            Stock = 25,
                            Variety = "Malbec",
                            Year = 2021
                        },
                        new
                        {
                            Id = -2,
                            CreatedAt = new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(183),
                            Name = "Catena Zapata Cabernet Sauvignon",
                            Region = "Mendoza",
                            Stock = 5,
                            Variety = "Cabernet Sauvignon",
                            Year = 2000
                        },
                        new
                        {
                            Id = -3,
                            CreatedAt = new DateTime(2024, 10, 8, 14, 13, 57, 472, DateTimeKind.Utc).AddTicks(188),
                            Name = "El Enemigo Bonarda",
                            Region = "San Juan",
                            Stock = 0,
                            Variety = "Bonarda",
                            Year = 2010
                        });
                });
#pragma warning restore 612, 618
        }
    }
}