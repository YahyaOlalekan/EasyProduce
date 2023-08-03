using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.AppDbContext

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        // public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionProduceType> TransactionProduceTypes { get; set; }
        public DbSet<ProduceType> ProduceTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderProductType> OrderProductTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItemForOrder> CartItemForOrders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}