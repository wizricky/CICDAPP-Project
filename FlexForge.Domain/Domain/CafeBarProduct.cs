using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Domain
{
    public class CafeBarProduct : BaseEntity
    {
        public CafeBarProduct()
        {
            ProductInOrders = [];
            Quantity = 0;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Category { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
