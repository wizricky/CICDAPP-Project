using FlexForge.Domain.Domain;
using FlexForge.Domain.DTO;


namespace FlexForge.Service.Interface
{
    public interface IFavoriteProductsService
    {
        bool deleteProductFromFavoriteProducts(string userId, Guid productId);
        bool AddToShoppingConfirmed(ProductInFavoriteProducts model, string userId);
        public FavoriteProductsDto getFavoriteProductsInfo(string userId);
        public bool IsFavorite(string userId, Guid productId);
    }
}
