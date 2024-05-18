using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_BuyerId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_BuyerId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FromDate",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ToDate",
                schema: "Document",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "AmountOfFee",
                schema: "Document",
                table: "Trips",
                newName: "Price");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Document",
                table: "Trips",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                schema: "Document",
                table: "TripParticipants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                schema: "Document",
                table: "Trips",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                schema: "Document",
                table: "Trips",
                column: "UserId",
                principalSchema: "Document",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Paid",
                schema: "Document",
                table: "TripParticipants");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Document",
                table: "Trips",
                newName: "AmountOfFee");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                schema: "Document",
                table: "Trips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                schema: "Document",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                schema: "Document",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BuyerId",
                schema: "Document",
                table: "Trips",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_BuyerId",
                schema: "Document",
                table: "Trips",
                column: "BuyerId",
                principalSchema: "Document",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
