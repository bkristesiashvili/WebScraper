using CGCCrawler.Libs.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace CGCCrawler.Libs.Address
{
    public sealed class AddressDbProcessor : IDisposable
    {
        /// <summary>
        /// database connection object
        /// </summary>
        private CryptoGuruDbContext Database { get; set; }

        /// <summary>
        /// Creates database processor object
        /// </summary>
        public AddressDbProcessor()
        {
            DbContextOptionsBuilder<CryptoGuruDbContext> option = new DbContextOptionsBuilder<CryptoGuruDbContext>();
            option.UseSqlServer(GVars.CONNECTION_STRING);
            Database = new CryptoGuruDbContext(option.Options);
        }

        /// <summary>
        /// insert address to database
        /// </summary>
        /// <param name="address"></param>
        public void Insert(BtcAddress address)
        {
            Database.Addresses.Add(address);
            Database.SaveChanges();
        }

        /// <summary>
        /// disposes object
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
