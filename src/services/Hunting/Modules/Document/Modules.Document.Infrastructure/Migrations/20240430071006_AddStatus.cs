using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raids_Lands_LandId",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_IssuedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_ReceivedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_IssuedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ReceivedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IssuedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ReceivedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Document",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "LandId",
                schema: "Document",
                table: "Raids",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Document",
                table: "Raids",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Document",
                table: "Feedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Raids_Lands_LandId",
                schema: "Document",
                table: "Raids",
                column: "LandId",
                principalSchema: "Document",
                principalTable: "Lands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raids_Lands_LandId",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Document",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Document",
                table: "Feedings");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                schema: "Document",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "IssuedId",
                schema: "Document",
                table: "Trips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedDate",
                schema: "Document",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivedId",
                schema: "Document",
                table: "Trips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LandId",
                schema: "Document",
                table: "Raids",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_IssuedId",
                schema: "Document",
                table: "Trips",
                column: "IssuedId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ReceivedId",
                schema: "Document",
                table: "Trips",
                column: "ReceivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Raids_Lands_LandId",
                schema: "Document",
                table: "Raids",
                column: "LandId",
                principalSchema: "Document",
                principalTable: "Lands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_IssuedId",
                schema: "Document",
                table: "Trips",
                column: "IssuedId",
                principalSchema: "Document",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_ReceivedId",
                schema: "Document",
                table: "Trips",
                column: "ReceivedId",
                principalSchema: "Document",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
