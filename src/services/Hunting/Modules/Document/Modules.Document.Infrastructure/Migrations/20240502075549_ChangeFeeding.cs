using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedingFeedingProduct",
                schema: "Document");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedingId",
                schema: "Document",
                table: "FeedingProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FeedingProducts_FeedingId",
                schema: "Document",
                table: "FeedingProducts",
                column: "FeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedingProducts_Feedings_FeedingId",
                schema: "Document",
                table: "FeedingProducts",
                column: "FeedingId",
                principalSchema: "Document",
                principalTable: "Feedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedingProducts_Feedings_FeedingId",
                schema: "Document",
                table: "FeedingProducts");

            migrationBuilder.DropIndex(
                name: "IX_FeedingProducts_FeedingId",
                schema: "Document",
                table: "FeedingProducts");

            migrationBuilder.DropColumn(
                name: "FeedingId",
                schema: "Document",
                table: "FeedingProducts");

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedingFeedingProduct_Feedings_FeedingsId",
                        column: x => x.FeedingsId,
                        principalSchema: "Document",
                        principalTable: "Feedings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedingFeedingProduct_ProductsId",
                schema: "Document",
                table: "FeedingFeedingProduct",
                column: "ProductsId");
        }
    }
}
