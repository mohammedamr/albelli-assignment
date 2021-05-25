using Microsoft.EntityFrameworkCore;

namespace Albelli_Assignment.Entities
{
    public class DatabaseContext : DbContext
    {
        private string connectionString = null;
        public DatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
