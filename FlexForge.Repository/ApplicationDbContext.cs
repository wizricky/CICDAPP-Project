//using FlexForge.DomainEntities;
using FlexForge.Domain.Domain;
using FlexForge.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FlexForge.Repository
{
    public class ApplicationDbContext : IdentityDbContext<FlexForgeApplicationUser>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<ProductsBySize> ProductBySizes { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<FavoriteProducts> FavoriteProducts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<ProductInFavoriteProducts> ProductInFavoriteProducts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductInOrder> ProductInOrders { get; set; }
       // public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.GenderType)
                .HasConversion<string>();

            modelBuilder.Entity<Product>()
           .Property(p => p.AgeGroup)
           .HasConversion<string>();

            modelBuilder.Entity<ProductsBySize>()
            .HasOne(pbs => pbs.Product)
            .WithMany(p => p.ProductBySizes)
            .HasForeignKey(pbs => pbs.ProductId);

            modelBuilder.Entity<ProductsBySize>()
                .HasOne(pbs => pbs.Size)
                .WithMany()
                .HasForeignKey(pbs => pbs.SizeId);
        }
    }
}
