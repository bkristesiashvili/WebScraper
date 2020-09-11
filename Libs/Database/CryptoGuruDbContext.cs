using CGCCrawler.Libs.Address;
using Microsoft.EntityFrameworkCore;

namespace CGCCrawler.Libs.Database
{
    public sealed class CryptoGuruDbContext: DbContext
    {
        /// <summary>
        /// Creates database connection object 
        /// </summary>
        /// <param name="options"></param>
        public CryptoGuruDbContext(DbContextOptions<CryptoGuruDbContext> options): base(options) { }

        /// <summary>
        /// set get bitcoin address collection
        /// </summary>
        public DbSet<BtcAddress> Addresses { get; set; }
    }
}
