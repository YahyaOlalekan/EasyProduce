using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.AppDbContext

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        // public DbSet<TransactionProduceType> TransactionProduceTypes { get; set; }
        public DbSet<ProduceType> ProduceTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderProductType> OrderProductTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItemForOrder> CartItemForOrders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FarmerProduceType> FarmerProduceTypes { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Request> Requests { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     var role = new Role
        //     {
        //        RoleName = "admin",
        //        RoleDescription = "AppOwner"
        //     };

        //     var user = new User
        //     {
        //         FirstName = "Ola",
        //         LastName = "Bisi",
        //         PhoneNumber = "08132759937",
        //         Email = "ola@gmail.com",
        //         Password = BCrypt.Net.BCrypt.HashPassword("123"),
        //         Address = "Abk",
        //         ProfilePicture = "admin.jpg",
        //         RoleId = role.Id
        //     };

        //      var admin = new Admin
        //     {
        //         UserId = user.Id,
        //         User = user,
        //         CreatedBy = "System",
        //         ModifiedBy = "System"
        //     };

            // modelBuilder.Entity<Admin>().HasData(
            //     role, user, admin,

            // Add(role),
            //  Users.Add(user),
            // Admin.Add(admin),
            // SaveChanges()
            // // SaveChangesAsync()
            // );

            // modelBuilder.Entity<Role>().HasData(role);
            // modelBuilder.Entity<User>().HasData(user);
            // modelBuilder.Entity<Admin>().HasData(admin);
            // base.OnModelCreating(modelBuilder);
        // }
    }
}