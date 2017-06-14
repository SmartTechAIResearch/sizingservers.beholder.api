using Microsoft.EntityFrameworkCore.Migrations;

namespace sizingservers.beholder.api.Migrations {
    /// <summary>
    /// 
    /// </summary>
    public partial class first : Migration {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "APIKeys",
                columns: table => new {
                    emailAddress = table.Column<string>(nullable: false),
                    key = table.Column<string>(nullable: true),
                    timeStampInSecondsSinceEpochUtc = table.Column<long>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_APIKeys", x => x.emailAddress);
                });

            migrationBuilder.CreateTable(
                name: "SystemInformations",
                columns: table => new {
                    hostname = table.Column<string>(nullable: false),
                    baseboard = table.Column<string>(nullable: true),
                    bios = table.Column<string>(nullable: true),
                    disks = table.Column<string>(nullable: true),
                    ips = table.Column<string>(nullable: true),
                    memoryModules = table.Column<string>(nullable: true),
                    nics = table.Column<string>(nullable: true),
                    os = table.Column<string>(nullable: true),
                    processors = table.Column<string>(nullable: true),
                    system = table.Column<string>(nullable: true),
                    timeStampInSecondsSinceEpochUtc = table.Column<long>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SystemInformations", x => x.hostname);
                });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "APIKeys");

            migrationBuilder.DropTable(
                name: "SystemInformations");
        }
    }
}
