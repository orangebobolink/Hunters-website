using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11abb3dd-1bd0-42ec-9987-89988982669b"), null, "User", "USER" },
                    { new Guid("ac85eaaf-d147-4dfb-9783-dbbdb989e8d7"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarUrl", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("4cbdc4a7-d44f-4874-8dc5-0848b49f7720"), 0, "", "f8f6936b-0404-4ab4-b153-1e14ed46742c", new DateTime(2024, 3, 19, 13, 58, 42, 521, DateTimeKind.Local).AddTicks(8068), "user@gmail.com", false, "", "", false, null, "", "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEKbbxK8XPhMNTCDaqziBjlE2cA9UAbGZokpwR+X+/17xl6e6Wlg3kysKvgAwWB2xhQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "937767de-8ac2-42ae-9b89-a5b0e2936fa2", 0, false, "user" },
                    { new Guid("ea8f7f4b-ee1f-4b72-a0b5-36b5c32bb0c8"), 0, "", "f0c443e8-7a48-4e7a-a653-9dfa0cb1f974", new DateTime(2024, 3, 19, 13, 58, 42, 664, DateTimeKind.Local).AddTicks(6238), "admin@gmail.com", false, "", "", false, null, "", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMFfOMOhMzuw/z4N0Z9UnMED8BSj7N9jwu7vrwPOSKxNewBvlyT0R7KX/1adYcrUyg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7f2f3c68-3fc5-4797-8f4f-01d64c893ead", 0, false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11abb3dd-1bd0-42ec-9987-89988982669b"), new Guid("4cbdc4a7-d44f-4874-8dc5-0848b49f7720") },
                    { new Guid("ac85eaaf-d147-4dfb-9783-dbbdb989e8d7"), new Guid("ea8f7f4b-ee1f-4b72-a0b5-36b5c32bb0c8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("11abb3dd-1bd0-42ec-9987-89988982669b"), new Guid("4cbdc4a7-d44f-4874-8dc5-0848b49f7720") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ac85eaaf-d147-4dfb-9783-dbbdb989e8d7"), new Guid("ea8f7f4b-ee1f-4b72-a0b5-36b5c32bb0c8") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11abb3dd-1bd0-42ec-9987-89988982669b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac85eaaf-d147-4dfb-9783-dbbdb989e8d7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4cbdc4a7-d44f-4874-8dc5-0848b49f7720"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ea8f7f4b-ee1f-4b72-a0b5-36b5c32bb0c8"));

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

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
    }
}
