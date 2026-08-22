using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsScrapingMonolithic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameHostToNewsPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailHost");

            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.CreateTable(
                name: "NewsPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Url = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeaderHost = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmailNewsPage",
                columns: table => new
                {
                    EmailsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNewsPage", x => new { x.EmailsId, x.HostsId });
                    table.ForeignKey(
                        name: "FK_EmailNewsPage_Emails_EmailsId",
                        column: x => x.EmailsId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailNewsPage_NewsPages_HostsId",
                        column: x => x.HostsId,
                        principalTable: "NewsPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNewsPage_HostsId",
                table: "EmailNewsPage",
                column: "HostsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPages_Url",
                table: "NewsPages",
                column: "Url",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailNewsPage");

            migrationBuilder.DropTable(
                name: "NewsPages");

            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Address = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_Address",
                table: "Hosts",
                column: "Address",
                unique: true);
        }
    }
}
