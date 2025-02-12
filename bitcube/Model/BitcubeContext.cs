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
        public string DbPath { get; }

        public BitcubeContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
       
        public DbSet<User> users { get; set; }
    }
}
