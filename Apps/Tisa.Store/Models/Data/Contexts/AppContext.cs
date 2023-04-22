using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using Tisa.Store.Models.Data.Configurations;
using Tisa.Store.Models.Entities;

namespace Tisa.Store.Models.Data.Contexts
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage.db");
            
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder()
            {
                DataSource = path
            };

            optionsBuilder.UseSqlite(builder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
        }
    }
}
