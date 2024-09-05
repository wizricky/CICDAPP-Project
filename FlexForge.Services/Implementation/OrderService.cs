using FlexForge.Domain.Domain;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;

namespace FlexForge.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return _orderRepository.GetDetailsForOrder(id);
        }
        public List<Order> GetOrdersByUserId(string userId)
        {
            return _orderRepository.GetAllOrders()
                                    .Where(order => order.userId == userId) 
                                    .ToList();
        }
    }
}
