using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsScrapingMonolithic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailsListToHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Emails_EmailId",
                table: "Hosts");

            migrationBuilder.DropIndex(
                name: "IX_Hosts_EmailId",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Hosts");

            migrationBuilder.CreateTable(
                name: "EmailHost",
                columns: table => new
                {
                    EmailsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHost", x => new { x.EmailsId, x.HostsId });
                    table.ForeignKey(
                        name: "FK_EmailHost_Emails_EmailsId",
                        column: x => x.EmailsId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailHost_Hosts_HostsId",
                        column: x => x.HostsId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHost_HostsId",
                table: "EmailHost",
                column: "HostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailHost");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "Hosts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_EmailId",
                table: "Hosts",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_Emails_EmailId",
                table: "Hosts",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id");
        }
    }
}
