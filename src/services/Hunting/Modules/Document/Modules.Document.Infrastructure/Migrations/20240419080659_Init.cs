using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Document");

            migrationBuilder.CreateTable(
                name: "Animals",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedingProducts",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxState",
                schema: "Document",
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
                name: "Lands",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "Document",
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
                schema: "Document",
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

            migrationBuilder.CreateTable(
                name: "Raids",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaidId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Raids_RaidId",
                        column: x => x.RaidId,
                        principalSchema: "Document",
                        principalTable: "Raids",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedings",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedings_Users_IssuedId",
                        column: x => x.IssuedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Feedings_Users_ReceivedId",
                        column: x => x.ReceivedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HuntingLicenses",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingLicenses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionForExtractionOfHuntingAnimals",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionForExtractionOfHuntingAnimals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionForExtractionOfHuntingAnimals_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalSchema: "Document",
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PermissionForExtractionOfHuntingAnimals_Users_IssuedId",
                        column: x => x.IssuedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PermissionForExtractionOfHuntingAnimals_Users_ReceivedId",
                        column: x => x.ReceivedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FeedingFeedingProduct",
                schema: "Document",
                columns: table => new
                {
                    FeedingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingFeedingProduct", x => new { x.FeedingsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_FeedingFeedingProduct_FeedingProducts_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "Document",
                        principalTable: "FeedingProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FeedingFeedingProduct_Feedings_FeedingsId",
                        column: x => x.FeedingsId,
                        principalSchema: "Document",
                        principalTable: "Feedings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupons_PermissionForExtractionOfHuntingAnimals_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Document",
                        principalTable: "PermissionForExtractionOfHuntingAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    AcceptedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_PermissionForExtractionOfHuntingAnimals_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Document",
                        principalTable: "PermissionForExtractionOfHuntingAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trips_Users_AcceptedId",
                        column: x => x.AcceptedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trips_Users_IssuedId",
                        column: x => x.IssuedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trips_Users_ReceivedId",
                        column: x => x.ReceivedId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TripParticipants",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HuntingLicenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripParticipants_HuntingLicenses_HuntingLicenseId",
                        column: x => x.HuntingLicenseId,
                        principalSchema: "Document",
                        principalTable: "HuntingLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TripParticipants_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Document",
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TripParticipants_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_PermissionId",
                schema: "Document",
                table: "Coupons",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFeedingProduct_ProductsId",
                schema: "Document",
                table: "FeedingFeedingProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedings_IssuedId",
                schema: "Document",
                table: "Feedings",
                column: "IssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedings_ReceivedId",
                schema: "Document",
                table: "Feedings",
                column: "ReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingLicenses_UserId",
                schema: "Document",
                table: "HuntingLicenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxState_Delivered",
                schema: "Document",
                table: "InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                schema: "Document",
                table: "OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                schema: "Document",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber",
                schema: "Document",
                table: "OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true,
                filter: "[InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_OutboxId_SequenceNumber",
                schema: "Document",
                table: "OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true,
                filter: "[OutboxId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                schema: "Document",
                table: "OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForExtractionOfHuntingAnimals_AnimalId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForExtractionOfHuntingAnimals_IssuedId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                column: "IssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForExtractionOfHuntingAnimals_ReceivedId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                column: "ReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_TripParticipants_HuntingLicenseId",
                schema: "Document",
                table: "TripParticipants",
                column: "HuntingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_TripParticipants_ParticipantId",
                schema: "Document",
                table: "TripParticipants",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_TripParticipants_TripId",
                schema: "Document",
                table: "TripParticipants",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_AcceptedId",
                schema: "Document",
                table: "Trips",
                column: "AcceptedId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_IssuedId",
                schema: "Document",
                table: "Trips",
                column: "IssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PermissionId",
                schema: "Document",
                table: "Trips",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ReceivedId",
                schema: "Document",
                table: "Trips",
                column: "ReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RaidId",
                schema: "Document",
                table: "Users",
                column: "RaidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "FeedingFeedingProduct",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "InboxState",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Lands",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "OutboxState",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "TripParticipants",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "FeedingProducts",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Feedings",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "HuntingLicenses",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "PermissionForExtractionOfHuntingAnimals",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Animals",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Raids",
                schema: "Document");
        }
    }
}
