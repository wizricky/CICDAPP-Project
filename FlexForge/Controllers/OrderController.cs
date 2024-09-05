using FlexForge.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlexForge.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index(string userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            return View(orders);
        }
        public IActionResult OrderProducts(Guid orderId)
        {
            var order = _orderService.GetAllOrders().FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
