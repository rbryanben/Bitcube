using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Model
{
    public class BitcubeContext : DbContext
    {
        /*
         *  DbContext for all Bitcube models 
         */
        public string dbPath { get; }

        public BitcubeContext()
        {
            // Use the current executing directory
            string executingDirectory = AppContext.BaseDirectory;
            // Join the paths 
            dbPath = System.IO.Path.Join(executingDirectory, "bitcube.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={dbPath}");
       
        // Dataset 
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
