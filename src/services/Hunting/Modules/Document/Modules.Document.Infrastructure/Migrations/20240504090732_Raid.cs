using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Raid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Raids_RaidId",
                schema: "Document",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RaidId",
                schema: "Document",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RaidId",
                schema: "Document",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "RaidUser",
                schema: "Document",
                columns: table => new
                {
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaidsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaidUser", x => new { x.ParticipantsId, x.RaidsId });
                    table.ForeignKey(
                        name: "FK_RaidUser_Raids_RaidsId",
                        column: x => x.RaidsId,
                        principalSchema: "Document",
                        principalTable: "Raids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaidUser_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalSchema: "Document",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaidUser_RaidsId",
                schema: "Document",
                table: "RaidUser",
                column: "RaidsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaidUser",
                schema: "Document");

            migrationBuilder.AddColumn<Guid>(
                name: "RaidId",
                schema: "Document",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RaidId",
                schema: "Document",
                table: "Users",
                column: "RaidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Raids_RaidId",
                schema: "Document",
                table: "Users",
                column: "RaidId",
                principalSchema: "Document",
                principalTable: "Raids",
                principalColumn: "Id");
        }
    }
}
