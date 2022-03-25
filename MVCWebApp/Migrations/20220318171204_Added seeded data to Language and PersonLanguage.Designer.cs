﻿// <auto-generated />
using MVCWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220318171204_Added seeded data to Language and PersonLanguage")]
    partial class AddedseededdatatoLanguageandPersonLanguage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCWebApp.Models.City.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryForeignKey")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("CountryForeignKey");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CityName = "Stockholm",
                            CountryForeignKey = "Sverige"
                        },
                        new
                        {
                            ID = 2,
                            CityName = "Oslo",
                            CountryForeignKey = "Norge"
                        },
                        new
                        {
                            ID = 3,
                            CityName = "Köpenhamn",
                            CountryForeignKey = "Danmark"
                        });
                });

            modelBuilder.Entity("MVCWebApp.Models.Country.Country", b =>
                {
                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CountryName");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryName = "Sverige"
                        },
                        new
                        {
                            CountryName = "Norge"
                        },
                        new
                        {
                            CountryName = "Danmark"
                        });
                });

            modelBuilder.Entity("MVCWebApp.Models.Language.Language", b =>
                {
                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("LanguageName");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageName = "Franska"
                        },
                        new
                        {
                            LanguageName = "Polska"
                        },
                        new
                        {
                            LanguageName = "Italienska"
                        });
                });

            modelBuilder.Entity("MVCWebApp.Models.Person.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityForeignKey")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CityForeignKey");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CityForeignKey = 1,
                            Name = "Jens Jensson",
                            PhoneNumber = "123456789"
                        },
                        new
                        {
                            ID = 2,
                            CityForeignKey = 2,
                            Name = "Anna Andersson",
                            PhoneNumber = "987654321"
                        },
                        new
                        {
                            ID = 3,
                            CityForeignKey = 3,
                            Name = "Sven Svensson",
                            PhoneNumber = "123123123"
                        });
                });

            modelBuilder.Entity("MVCWebApp.Models.PersonLanguage", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PersonId", "LanguageName");

                    b.HasIndex("LanguageName");

                    b.ToTable("PersonLanguages");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            LanguageName = "Franska"
                        });
                });

            modelBuilder.Entity("MVCWebApp.Models.City.City", b =>
                {
                    b.HasOne("MVCWebApp.Models.Country.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCWebApp.Models.Person.Person", b =>
                {
                    b.HasOne("MVCWebApp.Models.City.City", "City")
                        .WithMany("People")
                        .HasForeignKey("CityForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCWebApp.Models.PersonLanguage", b =>
                {
                    b.HasOne("MVCWebApp.Models.Language.Language", "Language")
                        .WithMany("PersonLanguages")
                        .HasForeignKey("LanguageName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCWebApp.Models.Person.Person", "Person")
                        .WithMany("PersonLanguages")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
