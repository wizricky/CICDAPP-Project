using FlexForge.Domain.Domain;
using FlexForge.Domain.DTO;


namespace FlexForge.Service.Interface
{
    public interface IFavoriteProductsService
    {
        bool deleteProductFromFavoriteProducts(string userId, Guid productId);
        bool AddToFavoriteProductsConfirmed(ProductInFavoriteProducts model, string userId);
        public List<Product> getFavoriteProductsInfo(string userId);
        public bool IsFavorite(string userId, Guid? productId);
    }
}
