using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sizingservers.beholder.api.Migrations {
    /// <summary>
    /// 
    /// </summary>
    [DbContext(typeof(DBContext))]
    [Migration("20170614073637_first")]
    partial class first {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder) {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("sizingservers.beholder.api.Models.APIKey", b => {
                b.Property<string>("emailAddress")
                    .ValueGeneratedOnAdd();

                b.Property<string>("key");

                b.Property<long>("timeStampInSecondsSinceEpochUtc");

                b.HasKey("emailAddress");

                b.ToTable("APIKeys");
            });

            modelBuilder.Entity("sizingservers.beholder.api.Models.SystemInformation", b => {
                b.Property<string>("hostname")
                    .ValueGeneratedOnAdd();

                b.Property<string>("baseboard");

                b.Property<string>("bios");

                b.Property<string>("disks");

                b.Property<string>("ips");

                b.Property<string>("memoryModules");

                b.Property<string>("nics");

                b.Property<string>("os");

                b.Property<string>("processors");

                b.Property<string>("system");

                b.Property<long>("timeStampInSecondsSinceEpochUtc");

                b.HasKey("hostname");

                b.ToTable("SystemInformations");
            });
        }
    }
}
