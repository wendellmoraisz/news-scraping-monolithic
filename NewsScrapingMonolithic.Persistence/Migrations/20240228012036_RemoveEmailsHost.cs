using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsScrapingMonolithic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmailsHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailHosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Host",
                table: "Host");

            migrationBuilder.RenameTable(
                name: "Host",
                newName: "Hosts");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "Hosts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Emails_EmailId",
                table: "Hosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts");

            migrationBuilder.DropIndex(
                name: "IX_Hosts_EmailId",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Hosts");

            migrationBuilder.RenameTable(
                name: "Hosts",
                newName: "Host");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Host",
                table: "Host",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmailHosts",
                columns: table => new
                {
                    EmailId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHosts", x => new { x.EmailId, x.HostId });
                    table.ForeignKey(
                        name: "FK_EmailHosts_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailHosts_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHosts_HostId",
                table: "EmailHosts",
                column: "HostId");
        }
    }
}
