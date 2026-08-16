using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsScrapingMonolithic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueEmailAndHostAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Hosts",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Emails",
                type: "varchar(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_Address",
                table: "Hosts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Address",
                table: "Emails",
                column: "Address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hosts_Address",
                table: "Hosts");

            migrationBuilder.DropIndex(
                name: "IX_Emails_Address",
                table: "Emails");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Hosts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Emails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(254)",
                oldMaxLength: 254)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
