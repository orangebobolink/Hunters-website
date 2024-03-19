using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("24a4b57f-fd8d-4ff9-b745-3b075616a824"), new Guid("194a828a-0142-42e8-81e0-61c816b69b96") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("474c5e71-52fa-4aea-be45-b69b672d6d52"), new Guid("f45f4a5c-1ad2-4911-82ba-6f60f0ae21c8") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("24a4b57f-fd8d-4ff9-b745-3b075616a824"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("474c5e71-52fa-4aea-be45-b69b672d6d52"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194a828a-0142-42e8-81e0-61c816b69b96"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45f4a5c-1ad2-4911-82ba-6f60f0ae21c8"));

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "AspNetUsers",
                newName: "MiddleName");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1ed7c31a-8016-44ee-9ca6-23119b6ee306"), null, "User", "USER" },
                    { new Guid("5a740672-07fe-4291-807d-dd7352749913"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("8544b914-5535-4425-96cb-e3391f4d6835"), 0, "3b4f6294-42ff-4cc9-9cef-4dbae17cc3ee", new DateTime(2024, 3, 17, 11, 12, 54, 333, DateTimeKind.Local).AddTicks(5531), "admin@gmail.com", false, "", "", false, null, "", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEHK99jikzuD0knxh6cbB2yZ8ErOFF+6ZM7BO8uiKlwx5lxeAW9mKyssJbVWFgiqXMw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d9189164-9f02-4036-b467-735e2b03a4cf", 0, false, "admin" },
                    { new Guid("a7097e01-42a8-4ed9-a072-7895407bf10e"), 0, "614966d0-6d80-4ae8-8a09-24a0c69843df", new DateTime(2024, 3, 17, 11, 12, 54, 263, DateTimeKind.Local).AddTicks(4046), "user@gmail.com", false, "", "", false, null, "", "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEOWYHwlDVvKOubp1nLETpIXyiJ2kLkRoqMeRNWPJyQBUdVg/lJoxjhp4x3zRKyE6+Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ab3f916e-c57b-49e8-adc5-15d18ea67936", 0, false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("5a740672-07fe-4291-807d-dd7352749913"), new Guid("8544b914-5535-4425-96cb-e3391f4d6835") },
                    { new Guid("1ed7c31a-8016-44ee-9ca6-23119b6ee306"), new Guid("a7097e01-42a8-4ed9-a072-7895407bf10e") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5a740672-07fe-4291-807d-dd7352749913"), new Guid("8544b914-5535-4425-96cb-e3391f4d6835") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1ed7c31a-8016-44ee-9ca6-23119b6ee306"), new Guid("a7097e01-42a8-4ed9-a072-7895407bf10e") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ed7c31a-8016-44ee-9ca6-23119b6ee306"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a740672-07fe-4291-807d-dd7352749913"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8544b914-5535-4425-96cb-e3391f4d6835"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7097e01-42a8-4ed9-a072-7895407bf10e"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "AspNetUsers",
                newName: "Phone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("24a4b57f-fd8d-4ff9-b745-3b075616a824"), null, "User", "USER" },
                    { new Guid("474c5e71-52fa-4aea-be45-b69b672d6d52"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("194a828a-0142-42e8-81e0-61c816b69b96"), 0, "54642f3a-1384-4c5c-a204-3e395841cfb9", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEEv8jpCv9U6iKpr4oraCunAyOsMe6kqnRndqikNw+ICOH4CYpKIiIqoC1BxZzjUQdw==", "", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "554906fa-0c2e-4323-ba90-61bbabe302c2", false, "user" },
                    { new Guid("f45f4a5c-1ad2-4911-82ba-6f60f0ae21c8"), 0, "adce07b2-37f5-4564-8637-5adbf890ad85", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAED5hjbpN8oVnIP+dVJk5XwcEczDbBhgIaHtUPehrCyeTEPo88Nd3v7mKKI8Ejs9kOg==", "", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c144e800-d37b-41a3-b4a7-dabd37837e0f", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("24a4b57f-fd8d-4ff9-b745-3b075616a824"), new Guid("194a828a-0142-42e8-81e0-61c816b69b96") },
                    { new Guid("474c5e71-52fa-4aea-be45-b69b672d6d52"), new Guid("f45f4a5c-1ad2-4911-82ba-6f60f0ae21c8") }
                });
        }
    }
}
