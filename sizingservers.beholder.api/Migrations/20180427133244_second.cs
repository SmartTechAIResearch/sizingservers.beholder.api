using Microsoft.EntityFrameworkCore.Migrations;

namespace sizingservers.beholder.api.Migrations {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class second : Migration {
        /// <summary>
        /// Ups the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "esxiGuestIPs",
                table: "SystemInformations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "esxiPassword",
                table: "SystemInformations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "esxiUsername",
                table: "SystemInformations",
                type: "TEXT",
                nullable: true);
        }
        /// <summary>
        /// Downs the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "esxiGuestIPs",
                table: "SystemInformations");

            migrationBuilder.DropColumn(
                name: "esxiPassword",
                table: "SystemInformations");

            migrationBuilder.DropColumn(
                name: "esxiUsername",
                table: "SystemInformations");
        }
    }
}
