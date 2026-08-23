using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsScrapingMonolithic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsPageToNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewsPageId",
                table: "News",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "News",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsPageId",
                table: "News",
                column: "NewsPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsPages_NewsPageId",
                table: "News",
                column: "NewsPageId",
                principalTable: "NewsPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsPages_NewsPageId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_NewsPageId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsPageId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "News");
        }
    }
}
