using FlexForge.Domain.Domain;
using FlexForge.Repository.Implementation;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace FlexForge.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public byte[] ExportOrderPdf(string userId)
        {

            var loggedInUser = _userRepository.Get(userId);
            var orders = _orderRepository.GetAllOrders().Where(o => o.Owner.Id == userId).ToList();

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"Order Summary for {loggedInUser.Email}")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        foreach (var order in orders)
                        {
                            column.Item().Text($"Order ID: {order.Id}").FontSize(14).Bold();

                            // Create a horizontal line
                            column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).PaddingVertical(5);

                            column.Item().PaddingVertical(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(30);  // Adjust as needed
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Product Name").Bold();
                                    header.Cell().Text("Quantity").Bold();
                                    header.Cell().Text("Price").Bold();
                                    header.Cell().Text("Total").Bold();
                                });

                                int index = 1;
                                foreach (var productInOrder in order.ProductsInOrder)
                                {
                                    table.Cell().Text(index++.ToString());
                                    table.Cell().Text(productInOrder.Product.ProductName);
                                    table.Cell().Text(productInOrder.Quantity.ToString());
                                    table.Cell().Text(productInOrder.Product.Price.ToString("C"));
                                    table.Cell().Text((productInOrder.Quantity * productInOrder.Product.Price).ToString("C"));
                                }
                            });

                            // Add padding between sections
                            column.Item().PaddingVertical(10).BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                        });
                });
            });

            using (var ms = new MemoryStream())
            {
                document.GeneratePdf(ms);
                return ms.ToArray();
            }
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
