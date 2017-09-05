using Microsoft.EntityFrameworkCore.Migrations;

namespace sizingservers.beholder.api.Migrations {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class first : Migration {
        /// <summary>
        /// Ups the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "APIKeys",
                columns: table => new {
                    emailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    key = table.Column<string>(type: "TEXT", nullable: true),
                    timeStampInSecondsSinceEpochUtc = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_APIKeys", x => x.emailAddress);
                });

            migrationBuilder.CreateTable(
                name: "SystemInformations",
                columns: table => new {
                    hostname = table.Column<string>(type: "TEXT", nullable: false),
                    baseboard = table.Column<string>(type: "TEXT", nullable: true),
                    bios = table.Column<string>(type: "TEXT", nullable: true),
                    disks = table.Column<string>(type: "TEXT", nullable: true),
                    ips = table.Column<string>(type: "TEXT", nullable: true),
                    memoryModules = table.Column<string>(type: "TEXT", nullable: true),
                    nics = table.Column<string>(type: "TEXT", nullable: true),
                    os = table.Column<string>(type: "TEXT", nullable: true),
                    processors = table.Column<string>(type: "TEXT", nullable: true),
                    system = table.Column<string>(type: "TEXT", nullable: true),
                    timeStampInSecondsSinceEpochUtc = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SystemInformations", x => x.hostname);
                });
        }
        /// <summary>
        /// Downs the specified migration builder.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "APIKeys");

            migrationBuilder.DropTable(
                name: "SystemInformations");
        }
    }
}
