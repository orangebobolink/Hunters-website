using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("66fb5a03-9dbc-43bf-9c6d-ae988b957b12"), new Guid("7393fd24-1e9a-415c-8a14-501dfbaf6c95") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f3fd182d-fb65-4cb9-98f3-238f87c35a61"), new Guid("f1080441-c719-4d44-a643-9ecc9f76c7d7") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("66fb5a03-9dbc-43bf-9c6d-ae988b957b12"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3fd182d-fb65-4cb9-98f3-238f87c35a61"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7393fd24-1e9a-415c-8a14-501dfbaf6c95"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f1080441-c719-4d44-a643-9ecc9f76c7d7"));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("66fb5a03-9dbc-43bf-9c6d-ae988b957b12"), null, "Admin", "ADMIN" },
                    { new Guid("f3fd182d-fb65-4cb9-98f3-238f87c35a61"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("7393fd24-1e9a-415c-8a14-501dfbaf6c95"), 0, "05b6b611-2fce-4615-a980-a154bfe644e5", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPy9E8rSWza3S/6OiQmXblDxq1pcNC56HTqivYRpUlu044wi4knlA9g/WYNo86VuZA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a57593bc-1274-4630-bfcd-54581442daa8", false, "admin" },
                    { new Guid("f1080441-c719-4d44-a643-9ecc9f76c7d7"), 0, "6ee35c59-f2fe-4016-bd5c-1c1b0cb101d2", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAENnyiTU0aYN00FHBbxJIj3OT6rpMWujX99yiBbk1JtMdPoKHLlV9aAsKRqBdq4k2cg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "731d1257-505d-443e-82ed-3fdcc80508c4", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("66fb5a03-9dbc-43bf-9c6d-ae988b957b12"), new Guid("7393fd24-1e9a-415c-8a14-501dfbaf6c95") },
                    { new Guid("f3fd182d-fb65-4cb9-98f3-238f87c35a61"), new Guid("f1080441-c719-4d44-a643-9ecc9f76c7d7") }
                });
        }
    }
}
