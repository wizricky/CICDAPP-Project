using FlexForge.Domain.Domain;
using FlexForge.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace FlexForge.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.ProductsInOrder)
                .Include(z => z.Owner)
                .Include("ProductsInOrder.Product")
                .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.ProductsInOrder)
                .Include(z => z.Owner)
                .Include("ProductsInOrder.Product")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}

