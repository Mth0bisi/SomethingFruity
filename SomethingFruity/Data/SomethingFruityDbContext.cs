using SomethingFruity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SomethingFruity.Data
{
    public class SomethingFruityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public SomethingFruityDbContext(DbContextOptions<SomethingFruityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
            .HasAlternateKey(r => r.CategoryCode);

            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasMany(u => u.Products)
                 .WithOne(ur => ur.User)
                 .HasForeignKey(ur => ur.UserId)
                 .IsRequired();
            });

            builder.Entity<Category>(c =>
            {
                c.ToTable("Categories");
                c.HasMany(u => u.Products)
                 .WithOne(ur => ur.Category)
                 .HasForeignKey(ur => ur.CategoryId)
                 .IsRequired();
            });




    //        builder.Entity<User>()
    //            .HasData(
    //                new User { Id = "4da80dd1-bcf3-47db-982a-d000f5b14583", Email = "user@gmail.com", UserName = "user@gmail.com", Password = "Passw0rd!", NormalizedEmail = "USER@GMAIL.COM", NormalizedUserName = "USER@GMAIL.COM" }
    //            );
    //        builder.Entity<Category>()
    //            .HasData(
    //                new Category { CategoryId = 1, Name = "Red", CategoryCode = "RED001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now },
    //                new Category { CategoryId = 2, Name = "Green", CategoryCode = "GRE001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now },
    //                new Category { CategoryId = 3, Name = "Yellow", CategoryCode = "YEL001", CreatedBy = "user@gmail.com", DateCreated = DateTime.Now }
    //);
    //        builder.Entity<Product>()
    //            .HasData(
    //                new Product { ProductId = 1, Name = "Apple", Description = "", Price = 34, Code = "202409-912", CategoryId = 1, UserId = "UserId", DateCreated = DateTime.Now },
    //                new Product { ProductId = 3, Name = "Avocado", Price = 45, Code = "202409-193", CategoryId = 2, UserId = "UserId", DateCreated = DateTime.Now },
    //                new Product { ProductId = 4, Name = "Pear", Price = 28, Code = "202409-007", CategoryId = 2, UserId = "UserId", DateCreated = DateTime.Now },
    //                new Product { ProductId = 2, Name = "Banana", Price = 32, Code = "202409-314", CategoryId = 3, UserId = "UserId", DateCreated = DateTime.Now }
    //);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
