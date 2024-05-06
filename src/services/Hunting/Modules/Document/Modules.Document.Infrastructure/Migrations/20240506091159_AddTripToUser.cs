using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTripToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_AcceptedId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "AcceptedId",
                schema: "Document",
                table: "Trips",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_AcceptedId",
                schema: "Document",
                table: "Trips",
                newName: "IX_Trips_BuyerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_BuyerId",
                schema: "Document",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                schema: "Document",
                table: "Trips",
                newName: "AcceptedId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_BuyerId",
                schema: "Document",
                table: "Trips",
                newName: "IX_Trips_AcceptedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_AcceptedId",
                schema: "Document",
                table: "Trips",
                column: "AcceptedId",
                principalSchema: "Document",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
