using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMiddleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a18c4ae2-3b1c-4bce-9efc-93009a38d2a1"), new Guid("c51df0be-43dd-48ee-8729-96435aec4a72") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("99bd882f-b0c2-4cb3-93d9-9c2e0fde4efb"), new Guid("edde2ba8-d84c-4839-86f6-527bdd792ea3") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("99bd882f-b0c2-4cb3-93d9-9c2e0fde4efb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a18c4ae2-3b1c-4bce-9efc-93009a38d2a1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c51df0be-43dd-48ee-8729-96435aec4a72"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("edde2ba8-d84c-4839-86f6-527bdd792ea3"));

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("91c7328a-6540-4d50-877c-dc5bf3eacbaa"), null, "User", "USER" },
                    { new Guid("cbb0b182-3e9f-4e24-b649-f19cc671cee7"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("333ad5b9-0784-403d-998a-13751f398a31"), 0, "", "aeb4de5b-86b7-4434-90a5-3d6cf1eb931e", new DateTime(2024, 3, 19, 14, 38, 18, 316, DateTimeKind.Local).AddTicks(4312), "user@gmail.com", false, "", "", false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEGOwHjc/7ntqVqY57FQVCTRKypB0f9P2O/wKn49ZuIvww8RqvJv82UgRsp8Te5Mk+w==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5054cdd2-edd2-4bfb-81ad-1947abb48c84", 0, false, "user" },
                    { new Guid("4606c084-187b-49d2-8271-57a379f58f31"), 0, "", "c02cc213-66f0-4714-992a-c3781b4905fc", new DateTime(2024, 3, 19, 14, 38, 18, 403, DateTimeKind.Local).AddTicks(8504), "admin@gmail.com", false, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOkXM9GUekipOEGMpZX4E2gcp7gPaVDJoU8fG6mmJvPwwwlqor8k4HFEGY2p8XtJqg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "88a66ab5-70b0-4533-8c58-278731597cdc", 0, false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("91c7328a-6540-4d50-877c-dc5bf3eacbaa"), new Guid("333ad5b9-0784-403d-998a-13751f398a31") },
                    { new Guid("cbb0b182-3e9f-4e24-b649-f19cc671cee7"), new Guid("4606c084-187b-49d2-8271-57a379f58f31") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("91c7328a-6540-4d50-877c-dc5bf3eacbaa"), new Guid("333ad5b9-0784-403d-998a-13751f398a31") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cbb0b182-3e9f-4e24-b649-f19cc671cee7"), new Guid("4606c084-187b-49d2-8271-57a379f58f31") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("91c7328a-6540-4d50-877c-dc5bf3eacbaa"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbb0b182-3e9f-4e24-b649-f19cc671cee7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("333ad5b9-0784-403d-998a-13751f398a31"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4606c084-187b-49d2-8271-57a379f58f31"));

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("99bd882f-b0c2-4cb3-93d9-9c2e0fde4efb"), null, "Admin", "ADMIN" },
                    { new Guid("a18c4ae2-3b1c-4bce-9efc-93009a38d2a1"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c51df0be-43dd-48ee-8729-96435aec4a72"), 0, "", "ee0d8ea9-3854-41a8-8c7d-ba8c7e75ed8c", new DateTime(2024, 3, 19, 14, 0, 5, 369, DateTimeKind.Local).AddTicks(7392), "user@gmail.com", false, "", "", false, null, "", "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAELj68zdjMJ6w6VJGBSUNAp+9BqtWSKkXRt8w9XC/zaotydx+3La/n82Q2XPpR7ZPIg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "50378f3c-d04e-45ff-bb48-ccc9c24ce63c", 0, false, "user" },
                    { new Guid("edde2ba8-d84c-4839-86f6-527bdd792ea3"), 0, "", "f31a5587-3c71-4491-a52a-b30c2fc61309", new DateTime(2024, 3, 19, 14, 0, 5, 491, DateTimeKind.Local).AddTicks(7219), "admin@gmail.com", false, "", "", false, null, "", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEK5pGYA0sfNwCQPC4wSj01CviIlX1+GqsKpQMdqyXkWU4qoBozWM0/7SHK3X4DZBAQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "97efb458-142d-4bcc-89e3-beebf511ee94", 0, false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a18c4ae2-3b1c-4bce-9efc-93009a38d2a1"), new Guid("c51df0be-43dd-48ee-8729-96435aec4a72") },
                    { new Guid("99bd882f-b0c2-4cb3-93d9-9c2e0fde4efb"), new Guid("edde2ba8-d84c-4839-86f6-527bdd792ea3") }
                });
        }
    }
}
