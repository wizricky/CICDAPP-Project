using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Domain
{
    public class ProductsBySize : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid SizeId { get; set; }
        public Size Size { get; set; }

        public int Quantity { get; set; }
    }
}
