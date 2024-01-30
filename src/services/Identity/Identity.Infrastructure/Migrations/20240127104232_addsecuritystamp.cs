using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsecuritystamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6c3aec08-65d9-43ed-95f5-2881601b675b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("992cf0b0-f79e-4c2e-9796-1bb5fda95105"));

            migrationBuilder.DeleteData(
                table: "IdentityUserRole<string>",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b8633e2d-a33b-45e6-8329-1958b3252bbd", "6c3aec08-65d9-43ed-95f5-2881601b675b" });

            migrationBuilder.DeleteData(
                table: "IdentityUserRole<string>",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b8633e2d-a33b-45e6-8329-1958b3252asc", "992cf0b0-f79e-4c2e-9796-1bb5fda95105" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0377a745-686e-4d8a-8dfe-165bad31801c"), 0, "80c9c32c-45b2-4e72-8b32-38f358113256", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEO3AtU7e0piULfNKF8IMzKJGfP4M5BlftKsmSyNixO7YmRwGj0iXraNCxgvNfi7UWQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8d2f5e93-8992-4f51-8f4f-d54cfb16c5fa", false, "user" },
                    { new Guid("b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f"), 0, "022f6d02-7e35-4612-ab1b-2c93be19a3ef", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENokIW/JaR+yezuHR4ycdG3Dww31/81sOkj8OaGNsxsWyhm+MhrLEKorbShI+TOj9A==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1879047d-a4fe-4034-96c3-f06673136585", false, "admin" }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0377a745-686e-4d8a-8dfe-165bad31801c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f"));

            migrationBuilder.DeleteData(
                table: "IdentityUserRole<string>",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b8633e2d-a33b-45e6-8329-1958b3252asc", "0377a745-686e-4d8a-8dfe-165bad31801c" });

            migrationBuilder.DeleteData(
                table: "IdentityUserRole<string>",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b8633e2d-a33b-45e6-8329-1958b3252bbd", "b30352d9-d0a2-4d06-9fe4-a1e7df8fa16f" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("6c3aec08-65d9-43ed-95f5-2881601b675b"), 0, "a6f5bf43-a593-4e0c-88eb-e9d861528b56", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEC0mzxgB35gVztSH6L/yCldXZbgiqoXe6hEkOwaenaI7+HmhaTvJMWOB4U7TVZbh3Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin" },
                    { new Guid("992cf0b0-f79e-4c2e-9796-1bb5fda95105"), 0, "ca706602-5ac6-466b-816d-0241308d65ca", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEOQoYL3E7yI7stZ7mmvloiC7qLIILQUIx/6cqXhRsZHDMdmi/0O/qYG29xyVtf8muw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "user" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<string>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b8633e2d-a33b-45e6-8329-1958b3252bbd", "6c3aec08-65d9-43ed-95f5-2881601b675b" },
                    { "b8633e2d-a33b-45e6-8329-1958b3252asc", "992cf0b0-f79e-4c2e-9796-1bb5fda95105" }
                });
        }
    }
}
