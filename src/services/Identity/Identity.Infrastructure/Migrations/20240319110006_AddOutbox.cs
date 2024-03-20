using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "InboxState",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveCount = table.Column<int>(type: "int", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Consumed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxState", x => x.Id);
                    table.UniqueConstraint("AK_InboxState_MessageId_ConsumerId", x => new { x.MessageId, x.ConsumerId });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnqueueTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboxMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InboxConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ResponseAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FaultAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.SequenceNumber);
                });

            migrationBuilder.CreateTable(
                name: "OutboxState",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxState", x => x.OutboxId);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_InboxState_Delivered",
                table: "InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                table: "OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true,
                filter: "[InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_OutboxId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true,
                filter: "[OutboxId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                table: "OutboxState",
                column: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxState");

            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.DropTable(
                name: "OutboxState");

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
    }
}
