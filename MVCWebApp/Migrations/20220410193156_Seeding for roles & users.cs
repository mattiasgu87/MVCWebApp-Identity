using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCWebApp.Migrations
{
    public partial class Seedingforrolesusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "529088b5-2f82-4ef7-a872-e6de7e1c3f93", "9f6b8809-434d-43c3-b6a1-0ac827eb9b81", "admin", "ADMIN" },
                    { "7f5aacfb-3572-4e68-b2fc-b7d182581355", "a642b47c-220e-4b45-80ae-bb688b3bdc05", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "21bcf7d0-b1c0-45bd-9051-687ce821539f", 0, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f4e25922-3679-4cc7-ab78-81b8e3a61460", "admin@adminmvc.com", false, "Admin", "Adminsson", false, null, "ADMIN@ADMINMVC.COM", "ADMIN@ADMINMVC.COM", "AQAAAAEAACcQAAAAENHjezEJLBP+xqfHKzKSqPaLAncO/lfIwSIByQ0RgKARt8766Yvb6CK8ptS5wTZaOw==", null, false, "6a6035ca-d220-4b8a-98bb-98a978ad772f", false, "admin@adminmvc.com" },
                    { "8b4fc952-4ff9-4c58-94b1-90a913caae88", 0, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "df995307-ebda-4df8-8bd6-93d54c753b4c", "user@usermvc.com", false, "Adam", "Adamsson", false, null, "USER@USERMVC.COM", "USER@USERMVC.COM", "AQAAAAEAACcQAAAAELQpXlFSS7euWbja9bMhFOf/m9yfGdkr2H3yKjSJwi/FpB5Q4v5udCIrfNiV1r1DKg==", null, false, "0b7a4485-55a5-4197-b600-c3569a4808d8", false, "user@usermvc.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "21bcf7d0-b1c0-45bd-9051-687ce821539f", "529088b5-2f82-4ef7-a872-e6de7e1c3f93" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8b4fc952-4ff9-4c58-94b1-90a913caae88", "7f5aacfb-3572-4e68-b2fc-b7d182581355" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "21bcf7d0-b1c0-45bd-9051-687ce821539f", "529088b5-2f82-4ef7-a872-e6de7e1c3f93" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8b4fc952-4ff9-4c58-94b1-90a913caae88", "7f5aacfb-3572-4e68-b2fc-b7d182581355" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "529088b5-2f82-4ef7-a872-e6de7e1c3f93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f5aacfb-3572-4e68-b2fc-b7d182581355");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21bcf7d0-b1c0-45bd-9051-687ce821539f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b4fc952-4ff9-4c58-94b1-90a913caae88");
        }
    }
}
