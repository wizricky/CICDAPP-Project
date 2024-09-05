using FlexForge.Domain.Domain;
using FlexForge.Domain.DTO;

namespace FlexForge.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid productId);
        bool order(string userId);
        bool AddToShoppingConfirmed(ProductInShoppingCart model, string userId);
    }
}
