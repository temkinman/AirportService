﻿// <auto-generated />
using System;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Airport.Infrastructure.Migrations
{
    [DbContext(typeof(AirportDbContext))]
    partial class AirportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Airport.Domain.Entities.Airport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Hubs")
                        .HasColumnType("integer");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Icao")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("TimezoneRegionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Iata")
                        .IsUnique();

                    b.HasIndex("Icao")
                        .IsUnique();

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Airport.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Iata")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Airport.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Iata")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Airport.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<double>("Lon")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Lat", "Lon")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Airport.Domain.Entities.Airport", b =>
                {
                    b.HasOne("Airport.Domain.Entities.City", "City")
                        .WithMany("Airports")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airport.Domain.Entities.Location", "Location")
                        .WithOne("Airport")
                        .HasForeignKey("Airport.Domain.Entities.Airport", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Airport.Domain.Entities.City", b =>
                {
                    b.HasOne("Airport.Domain.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Airport.Domain.Entities.City", b =>
                {
                    b.Navigation("Airports");
                });

            modelBuilder.Entity("Airport.Domain.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Airport.Domain.Entities.Location", b =>
                {
                    b.Navigation("Airport")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
