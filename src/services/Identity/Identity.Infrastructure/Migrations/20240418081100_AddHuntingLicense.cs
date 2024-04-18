using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHuntingLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HuntingLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingLicenses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HuntingLicenses_UserId",
                table: "HuntingLicenses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuntingLicenses");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

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
    }
}
