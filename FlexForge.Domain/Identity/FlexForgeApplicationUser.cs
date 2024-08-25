using FlexForge.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Identity
{
    public class FlexForgeApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual FavoriteProducts FavoriteProducts { get; set; } = new FavoriteProducts();
        public virtual ICollection<Order>? Order { get; set; }

    }
}
