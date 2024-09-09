using FlexForge.Domain.Domain;

namespace FlexForge.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetDetailsForOrder(BaseEntity id);
        List<Order> GetOrdersByUserId(string userId);
        byte[] ExportOrderPdf(string userId);
    }
}
