using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Models;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Language;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PersonLanguage> PersonLanguages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Foreign key shadow properties
            modelBuilder.Entity<City>()
                .Property<string>("CountryForeignKey");
            modelBuilder.Entity<Person>()
                .Property<int>("CityForeignKey");

            //Combined primary key for PersonLanguage
            modelBuilder.Entity<PersonLanguage>().HasKey(pl => new { pl.PersonId, pl.LanguageName });

            //Configures one-to-many relationship City-Country
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
            .HasForeignKey("CountryForeignKey");

            //Configures one-to-many relationship Person-City
            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.People)
            .HasForeignKey("CityForeignKey");

            //Configures many-to-many relationship Person-Language
            modelBuilder.Entity<PersonLanguage>()
                .HasOne(pl => pl.Person)
                .WithMany(p => p.PersonLanguages)
                .HasForeignKey(pl => pl.PersonId);
            modelBuilder.Entity<PersonLanguage>()
                .HasOne(pl => pl.Language)
                .WithMany(c => c.PersonLanguages)
                .HasForeignKey(pl => pl.LanguageName);         

            #region seeding
            //seeding countries
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryName = "Sverige" },
                new Country { CountryName = "Norge" },
                new Country { CountryName = "Danmark" });

            //seeding cities
            modelBuilder.Entity<City>().HasData(
            new { ID = 1, CityName = "Stockholm", CountryForeignKey = "Sverige" },
            new { ID = 2, CityName = "Oslo", CountryForeignKey = "Norge" },
            new { ID = 3, CityName = "Köpenhamn", CountryForeignKey = "Danmark" });

            //seeding languages
            modelBuilder.Entity<Language>().HasData(
                new Language { LanguageName = "Franska" },
                new Language { LanguageName = "Polska" },
                new Language { LanguageName = "Italienska" });
            
            //seeding people
            modelBuilder.Entity<Person>().HasData(
                new { ID = 1, Name = "Jens Jensson", PhoneNumber = "123456789", CityForeignKey = 1 },
                new { ID = 2, Name = "Anna Andersson", PhoneNumber = "987654321", CityForeignKey = 2 },
                new { ID = 3, Name = "Sven Svensson", PhoneNumber = "123123123", CityForeignKey = 3 });

            //seeding peoplelanguages
            modelBuilder.Entity<PersonLanguage>().HasData(
                new PersonLanguage { PersonId = 1, LanguageName = "Franska" },
                new PersonLanguage { PersonId = 2, LanguageName = "Polska" },
                new PersonLanguage { PersonId = 3, LanguageName = "Italienska" });

            //seeding roles
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = userRoleId,
                Name = "user",
                NormalizedName = "USER"
            });

            //seeding users
            string adminId = Guid.NewGuid().ToString();
            DateTime adminBirthdate = new DateTime(1980, 1, 1);

            string userId = Guid.NewGuid().ToString();           
            DateTime userBirthdate = new DateTime(1990, 12, 12);

            PasswordHasher<ApplicationUser> pwHasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminId, 
                    Email = "admin@adminmvc.com",
                    NormalizedEmail = "ADMIN@ADMINMVC.COM",
                    UserName = "admin@adminmvc.com",
                    NormalizedUserName = "ADMIN@ADMINMVC.COM",
                    FirstName = "Admin",
                    LastName = "Adminsson",
                    PasswordHash = pwHasher.HashPassword(null, "password"),
                    BirthDate = adminBirthdate
                },
                new ApplicationUser
                {
                    Id = userId,
                    Email = "user@usermvc.com",
                    NormalizedEmail = "USER@USERMVC.COM", 
                    UserName = "user@usermvc.com",
                    NormalizedUserName = "USER@USERMVC.COM",
                    FirstName = "Adam", 
                    LastName = "Adamsson", 
                    PasswordHash = pwHasher.HashPassword(null, "PASSWORD"), 
                    BirthDate = userBirthdate
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                { 
                    RoleId = adminRoleId, 
                    UserId = adminId 
                }, 
                new IdentityUserRole<string> 
                {
                    RoleId = userRoleId, 
                    UserId = userId 
                });
            #endregion
        }
    }
}
