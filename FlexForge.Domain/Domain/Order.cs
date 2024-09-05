using FlexForge.Domain.Identity;

namespace FlexForge.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string userId { get; set; }
        public FlexForgeApplicationUser Owner { get; set; }
        public IEnumerable<ProductInOrder> ProductsInOrder { get; set; }
        public DateTime orderDate { get; set; }
    }
}
