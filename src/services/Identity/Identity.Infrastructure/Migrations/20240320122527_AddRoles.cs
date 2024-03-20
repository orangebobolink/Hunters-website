using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("19e829d8-1fec-46f9-b58b-abf47f19a5b0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("97aed454-fcd6-4e3d-92a9-f99235fabe06"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a386cb8b-0ca9-41d7-943c-3dd0512de998"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9bd4d599-3df0-484a-8b9c-8540127d7b19"), new Guid("505b7853-fa72-4333-a3c6-e42551a3ff34") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9ef7ae16-8a96-43fd-8665-157174bd9353"), new Guid("76f5341e-a3ed-4d51-8b6a-e14105046329") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9bd4d599-3df0-484a-8b9c-8540127d7b19"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ef7ae16-8a96-43fd-8665-157174bd9353"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("505b7853-fa72-4333-a3c6-e42551a3ff34"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("76f5341e-a3ed-4d51-8b6a-e14105046329"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2703de74-ce58-45d1-8216-3b58dc1d7fd9"), null, "Director", "DIRECTOR" },
                    { new Guid("51cfba0b-005f-422e-a355-6a7253c81d8f"), null, "Manager", "MANAGER" },
                    { new Guid("709afd80-0b6f-4017-b683-293676bde297"), null, "Admin", "ADMIN" },
                    { new Guid("71792062-3af3-4cb4-a020-f10641e11260"), null, "User", "USER" },
                    { new Guid("da6e59fe-8f04-42dc-839c-dffba914d7a6"), null, "Ranger", "RANGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("4a60a841-858a-49cc-a886-5892e23deae9"), 0, "", "598a7395-2016-4d43-86bd-04cae9ae9cdb", new DateTime(2024, 3, 20, 15, 25, 26, 768, DateTimeKind.Local).AddTicks(3128), "admin@gmail.com", false, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOh2MVj+iT77i+mXOq5zQrU0YlzkHxL/3murh2CCE8YsFwx3I0XkGvY9TkDvduGHyA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8938e320-6cb7-4c83-a37e-b70ce0ca55f2", 0, false, "admin" },
                    { new Guid("7876810d-5aa4-44c5-be14-0950178d7c24"), 0, "", "f6a87075-8d7a-4beb-a46b-eca964c561a1", new DateTime(2024, 3, 20, 15, 25, 26, 700, DateTimeKind.Local).AddTicks(8637), "user@gmail.com", false, "", "", false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEA99uELp7XNebIqLJwFYkKjsc980wAbocJvwpfvTxajT9ejRQuymEJe8OzCL1u1OLQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bde40745-ec66-4b70-92ee-6a668a4f7058", 0, false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("709afd80-0b6f-4017-b683-293676bde297"), new Guid("4a60a841-858a-49cc-a886-5892e23deae9") },
                    { new Guid("71792062-3af3-4cb4-a020-f10641e11260"), new Guid("7876810d-5aa4-44c5-be14-0950178d7c24") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2703de74-ce58-45d1-8216-3b58dc1d7fd9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("51cfba0b-005f-422e-a355-6a7253c81d8f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("da6e59fe-8f04-42dc-839c-dffba914d7a6"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("709afd80-0b6f-4017-b683-293676bde297"), new Guid("4a60a841-858a-49cc-a886-5892e23deae9") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("71792062-3af3-4cb4-a020-f10641e11260"), new Guid("7876810d-5aa4-44c5-be14-0950178d7c24") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("709afd80-0b6f-4017-b683-293676bde297"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("71792062-3af3-4cb4-a020-f10641e11260"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4a60a841-858a-49cc-a886-5892e23deae9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7876810d-5aa4-44c5-be14-0950178d7c24"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("19e829d8-1fec-46f9-b58b-abf47f19a5b0"), null, "Manager", "USER" },
                    { new Guid("97aed454-fcd6-4e3d-92a9-f99235fabe06"), null, "Ranger", "USER" },
                    { new Guid("9bd4d599-3df0-484a-8b9c-8540127d7b19"), null, "User", "USER" },
                    { new Guid("9ef7ae16-8a96-43fd-8665-157174bd9353"), null, "Admin", "USER" },
                    { new Guid("a386cb8b-0ca9-41d7-943c-3dd0512de998"), null, "Director", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("505b7853-fa72-4333-a3c6-e42551a3ff34"), 0, "", "bac3346b-f7f4-47cb-90ab-f75af4958c87", new DateTime(2024, 3, 20, 15, 23, 32, 993, DateTimeKind.Local).AddTicks(7431), "user@gmail.com", false, "", "", false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEOZBOsp7DA7zHEoDRsR3HwBXHywlsF1u7KGoD44nvjCYNyZQGXJEtf9ramjdacidPQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b8f471d7-4724-4fc3-a059-c5f5155e85aa", 0, false, "user" },
                    { new Guid("76f5341e-a3ed-4d51-8b6a-e14105046329"), 0, "", "3eb069ab-5450-4ff5-8d35-f3c5a39db5e0", new DateTime(2024, 3, 20, 15, 23, 33, 61, DateTimeKind.Local).AddTicks(5923), "admin@gmail.com", false, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFIQNR7qMwwKYGB3gbGdvzW0VWRqP2xy8s/gwI/Auzt61+Q3uxLcNTFQPQ5g7ooz1Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "71df38ed-6fdc-4ec5-9c6f-7ceea9604cb1", 0, false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9bd4d599-3df0-484a-8b9c-8540127d7b19"), new Guid("505b7853-fa72-4333-a3c6-e42551a3ff34") },
                    { new Guid("9ef7ae16-8a96-43fd-8665-157174bd9353"), new Guid("76f5341e-a3ed-4d51-8b6a-e14105046329") }
                });
        }
    }
}
