﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RollsApi.Data;

#nullable disable

namespace RollsApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230404052911_name")]
    partial class name
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RollsApi.Models.Bank", b =>
                {
                    b.Property<long>("bank_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("bank_id"));

                    b.Property<string>("bank_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("record_status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("bank_id");

                    b.HasIndex("bank_name")
                        .IsUnique();

                    b.ToTable("banks");

                    b.HasData(
                        new
                        {
                            bank_id = 1L,
                            bank_name = "STATE BANK OF INDIA",
                            record_status = "ACTIVE"
                        },
                        new
                        {
                            bank_id = 2L,
                            bank_name = "ICICI",
                            record_status = "ACTIVE"
                        },
                        new
                        {
                            bank_id = 3L,
                            bank_name = "HDFC BANK",
                            record_status = "ACTIVE"
                        },
                        new
                        {
                            bank_id = 4L,
                            bank_name = "AXIS BANK",
                            record_status = "ACTIVE"
                        },
                        new
                        {
                            bank_id = 5L,
                            bank_name = "UNION BANK OF INDIA",
                            record_status = "ACTIVE"
                        });
                });

            modelBuilder.Entity("RollsApi.Models.Country", b =>
                {
                    b.Property<long>("country_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("country_id"));

                    b.Property<string>("country_code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("country_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("record_status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("country_id");

                    b.HasIndex("country_code")
                        .IsUnique();

                    b.HasIndex("country_name")
                        .IsUnique();

                    b.ToTable("countries");

                    b.HasData(
                        new
                        {
                            country_id = 1L,
                            country_code = "IND",
                            country_name = "INDIA",
                            record_status = "ACTIVE"
                        });
                });

            modelBuilder.Entity("RollsApi.Models.District", b =>
                {
                    b.Property<long>("district_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("district_id"));

                    b.Property<long>("created_by")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("timestamp");

                    b.Property<string>("district_code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("district_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("record_status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("state_id")
                        .HasColumnType("bigint");

                    b.Property<long>("updated_by")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updated_on")
                        .HasColumnType("timestamp");

                    b.HasKey("district_id");

                    b.HasIndex("district_code")
                        .IsUnique();

                    b.HasIndex("district_name")
                        .IsUnique();

                    b.HasIndex("state_id");

                    b.ToTable("districts");
                });

            modelBuilder.Entity("RollsApi.Models.State", b =>
                {
                    b.Property<long>("state_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("state_id"));

                    b.Property<long>("country_id")
                        .HasColumnType("bigint");

                    b.Property<string>("record_status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("state_code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("state_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("state_id");

                    b.HasIndex("country_id");

                    b.HasIndex("state_code")
                        .IsUnique();

                    b.HasIndex("state_name")
                        .IsUnique();

                    b.ToTable("states");

                    b.HasData(
                        new
                        {
                            state_id = 1L,
                            country_id = 1L,
                            record_status = "ACTIVE",
                            state_code = "KRL",
                            state_name = "KERALA"
                        });
                });

            modelBuilder.Entity("RollsApi.Models.District", b =>
                {
                    b.HasOne("RollsApi.Models.State", "state")
                        .WithMany("districts")
                        .HasForeignKey("state_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("state");
                });

            modelBuilder.Entity("RollsApi.Models.State", b =>
                {
                    b.HasOne("RollsApi.Models.Country", "country")
                        .WithMany("states")
                        .HasForeignKey("country_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("country");
                });

            modelBuilder.Entity("RollsApi.Models.Country", b =>
                {
                    b.Navigation("states");
                });

            modelBuilder.Entity("RollsApi.Models.State", b =>
                {
                    b.Navigation("districts");
                });
#pragma warning restore 612, 618
        }
    }
}
