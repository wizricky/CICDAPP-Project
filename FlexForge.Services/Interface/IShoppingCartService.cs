using FlexForge.Domain.Domain;
using FlexForge.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
