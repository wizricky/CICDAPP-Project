using FlexForge.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public FlexForgeApplicationUser? Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
    }
}
