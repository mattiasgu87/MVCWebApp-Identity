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
    [Migration("20220316142216_Added some seeded data")]
    partial class Addedsomeseededdata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCWebApp.Models.Person.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            City = "Stenstorp",
                            Name = "Sten Stensson",
                            PhoneNumber = "0743345431"
                        },
                        new
                        {
                            ID = 2,
                            City = "Arboga",
                            Name = "Anna Aronsson",
                            PhoneNumber = "0743345412"
                        },
                        new
                        {
                            ID = 3,
                            City = "Stockholm",
                            Name = "Jens Falk",
                            PhoneNumber = "0743345444"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
