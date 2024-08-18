
namespace FlexForge.Domain.Domain
{
    public class ProductInFavoriteProducts : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid FavoriteProductsId { get; set; }
        public Product? Product { get; set; }
        public FavoriteProducts? FavoriteProducts { get; set; }
    }
}
