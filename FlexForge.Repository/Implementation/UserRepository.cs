using FlexForge.Domain.Identity;
using FlexForge.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<FlexForgeApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<FlexForgeApplicationUser>();
        }
        public IEnumerable<FlexForgeApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public FlexForgeApplicationUser Get(string id)
        {
            return entities
               .Include(z => z.ShoppingCart)
               .Include("ShoppingCart.ProductInShoppingCarts")
               .Include("ShoppingCart.ProductInShoppingCarts.Product")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(FlexForgeApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(FlexForgeApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(FlexForgeApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
