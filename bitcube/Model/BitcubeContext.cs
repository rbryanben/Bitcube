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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply the productId to be unique
            modelBuilder.Entity<Product>()
                .HasIndex(product => new { product.productId })
                .IsUnique(true);
        }

        // Dataset 
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<CartProduct> cartProducts { get; set; }
    }
}
