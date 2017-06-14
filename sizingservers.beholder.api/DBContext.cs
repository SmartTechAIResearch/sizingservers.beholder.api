/*
 * 2017 Sizing Servers Lab
 * University College of West-Flanders, Department GKG
 * 
 */

using Microsoft.EntityFrameworkCore;
using sizingservers.beholder.api.Models;

namespace sizingservers.beholder.api {
    /// <summary>
    /// Link to the sqlite3 db.
    /// </summary>
    public class DBContext : DbContext {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SystemInformation> SystemInformations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<APIKey> APIKeys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=beholder.db");
        }
    }
}
