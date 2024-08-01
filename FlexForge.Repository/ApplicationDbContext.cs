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
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductInOrder> ProductInOrders { get; set; }
       // public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
