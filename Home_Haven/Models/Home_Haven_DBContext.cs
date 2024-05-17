using Microsoft.EntityFrameworkCore;

namespace Home_Haven.Models
{
    public class Home_Haven_DBContext : DbContext
    {
        public Home_Haven_DBContext(DbContextOptions<Home_Haven_DBContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between User and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            // Configure many-to-many relationship between Order and Product via OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Name = "Zaid Alam",
                    Email = "zaid@example.com",
                    Password = "Zaid1234",
                    Address = "123 Main St",
                    PhoneNumber = "1234567890",                  
                },
                new User
                {
                    UserID = 2,
                    Name = "Kazi Aniketh",
                    Email = "aniketh@example.com",
                    Password = "Aniketh1234",
                    Address = "456 Elm St",
                    PhoneNumber = "0987654321",             
                }
            );

            // Seed data for Products
                        modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     ProductID = 1,
                     Name = "Vintage Chair",
                     Description = "A beautiful vintage chair made of oak.",
                     Price = 120.99m,
                     Quantity = 10,
                     ImageURL = "/images/vintage_chair.jpg"
                 },
                 new Product
                 {
                     ProductID = 2,
                     Name = "Modern Table",
                     Description = "A sleek modern table made of glass and steel.",
                     Price = 350.50m,
                     Quantity = 5,
                     ImageURL = "/images/modern_table.jpg"
                 },
                 new Product
                 {
                     ProductID = 3,
                     Name = "Classic Bed",
                     Description = "A classic wooden bed with elegant carvings.",
                     Price = 499.99m,
                     Quantity = 7,
                     ImageURL = "/images/bed1.jpg"
                 },
                 new Product
                 {
                     ProductID = 4,
                     Name = "Contemporary Sofa",
                     Description = "A comfortable contemporary sofa with plush cushions.",
                     Price = 899.99m,
                     Quantity = 3,
                     ImageURL = "/images/easysofa2.jpg"
                 },
                 new Product
                 {
                     ProductID = 5,
                     Name = "Rustic Table",
                     Description = "A rustic table made from reclaimed wood.",
                     Price = 249.99m,
                     Quantity = 4,
                     ImageURL = "/images/table2.jpg"
                 },
                 new Product
                 {
                     ProductID = 6,
                     Name = "Leather Chair",
                     Description = "A stylish leather chair with a modern design.",
                     Price = 199.99m,
                     Quantity = 8,
                     ImageURL = "/images/chair2.jpg"
                 },
                 new Product
                 {
                     ProductID = 7,
                     Name = "Elegant Bed",
                     Description = "An elegant bed with a plush headboard.",
                     Price = 699.99m,
                     Quantity = 5,
                     ImageURL = "/images/bed4.jpg"
                 },
                 new Product
                 {
                     ProductID = 8,
                     Name = "Minimalist Sofa",
                     Description = "A minimalist sofa with clean lines and a modern look.",
                     Price = 799.99m,
                     Quantity = 4,
                     ImageURL = "/images/easysofa3.jpg"
                 },
                 new Product
                 {
                     ProductID = 9,
                     Name = "Industrial Table",
                     Description = "An industrial-style table with metal and wood elements.",
                     Price = 299.99m,
                     Quantity = 6,
                     ImageURL = "/images/table3.jpg"
                 },
                 new Product
                 {
                     ProductID = 10,
                     Name = "Comfy Chair",
                     Description = "A comfy chair with soft upholstery and a cozy design.",
                     Price = 149.99m,
                     Quantity = 12,
                     ImageURL = "/images/comchair2.jpg"
                 },
                 new Product
                 {
                     ProductID = 11,
                     Name = "Modern Bed",
                     Description = "A modern bed with a sleek design and a low profile.",
                     Price = 549.99m,
                     Quantity = 5,
                     ImageURL = "/images/bed3.jpg"
                 },
                 new Product
                 {
                     ProductID = 12,
                     Name = "Reclining Sofa",
                     Description = "A reclining sofa with luxurious padding for maximum comfort.",
                     Price = 1099.99m,
                     Quantity = 2,
                     ImageURL = "/images/sofa4.jpg"
                 }
             );


            // Seed data for Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderID = 1,
                    UserID = 1,
                    OrderDate = DateTime.Now
                },
                new Order
                {
                    OrderID = 2,
                    UserID = 2,
                    OrderDate = DateTime.Now
                }
            );

            // Seed data for OrderDetail
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderID = 1,
                    ProductID = 1,
                    Quantity = 2,
                    TotalPrice = 241.98m
                },
                new OrderDetail
                {
                    OrderID = 1,
                    ProductID = 2,
                    Quantity = 1,
                    TotalPrice = 350.50m
                },
                new OrderDetail
                {
                    OrderID = 2,
                    ProductID = 2,
                    Quantity = 3,
                    TotalPrice = 1051.50m
                }
             );
        }
    }
}
