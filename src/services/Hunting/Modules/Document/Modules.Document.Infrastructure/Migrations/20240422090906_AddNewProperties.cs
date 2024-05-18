using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LandId",
                schema: "Document",
                table: "Raids",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LandId",
                schema: "Document",
                table: "Feedings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedDate",
                schema: "Document",
                table: "Feedings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Raids_LandId",
                schema: "Document",
                table: "Raids",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionForExtractionOfHuntingAnimals_LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedings_LandId",
                schema: "Document",
                table: "Feedings",
                column: "LandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedings_Lands_LandId",
                schema: "Document",
                table: "Feedings",
                column: "LandId",
                principalSchema: "Document",
                principalTable: "Lands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionForExtractionOfHuntingAnimals_Lands_LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals",
                column: "LandId",
                principalSchema: "Document",
                principalTable: "Lands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Feedings_Lands_LandId",
                schema: "Document",
                table: "Feedings");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionForExtractionOfHuntingAnimals_Lands_LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_Raids_Lands_LandId",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropIndex(
                name: "IX_Raids_LandId",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropIndex(
                name: "IX_PermissionForExtractionOfHuntingAnimals_LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals");

            migrationBuilder.DropIndex(
                name: "IX_Feedings_LandId",
                schema: "Document",
                table: "Feedings");

            migrationBuilder.DropColumn(
                name: "LandId",
                schema: "Document",
                table: "Raids");

            migrationBuilder.DropColumn(
                name: "LandId",
                schema: "Document",
                table: "PermissionForExtractionOfHuntingAnimals");

            migrationBuilder.DropColumn(
                name: "LandId",
                schema: "Document",
                table: "Feedings");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                schema: "Document",
                table: "Feedings");
        }
    }
}
