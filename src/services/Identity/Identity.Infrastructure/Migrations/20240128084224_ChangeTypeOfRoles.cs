using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0377a745-686e-4d8a-8dfe-165bad31801c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0377a745-686e-4d8a-8dfe-165bad31801c"), 0, "80c9c32c-45b2-4e72-8b32-38f358113256", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEO3AtU7e0piULfNKF8IMzKJGfP4M5BlftKsmSyNixO7YmRwGj0iXraNCxgvNfi7UWQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8d2f5e93-8992-4f51-8f4f-d54cfb16c5fa", false, "user" },
                    { new Guid("b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f"), 0, "022f6d02-7e35-4612-ab1b-2c93be19a3ef", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENokIW/JaR+yezuHR4ycdG3Dww31/81sOkj8OaGNsxsWyhm+MhrLEKorbShI+TOj9A==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1879047d-a4fe-4034-96c3-f06673136585", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b8633e2d-a33b-45e6-8329-1958b3252asc", null, "User", "USER" },
                    { "b8633e2d-a33b-45e6-8329-1958b3252bbd", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<string>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b8633e2d-a33b-45e6-8329-1958b3252asc", "0377a745-686e-4d8a-8dfe-165bad31801c" },
                    { "b8633e2d-a33b-45e6-8329-1958b3252bbd", "b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f" }
                });
        }
    }
}
