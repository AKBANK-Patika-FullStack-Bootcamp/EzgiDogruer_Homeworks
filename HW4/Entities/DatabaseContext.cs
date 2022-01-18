using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DatabaseContext :DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source = DESKTOP-HTBKIF7\\SQLEXPRESS; Database = DryCleaner; integrated security = True;");
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<DBLog> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Clothes>().ToTable("Clothes");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<DBLog>().ToTable("Log");
        }
        
    }
}
