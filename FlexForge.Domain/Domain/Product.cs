using FlexForge.Domain.Enum;
using FlexForge.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexForge.Domain.Domain
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }

        [Required]
        public int Price { get; set; }

        public int Rating { get; set; }

        public Guid? CategoryId { get; set; } // Foreign key for Category
        public Guid? SubCategoryId { get; set; } // Foreign key for SubCategory

        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual SubCategory? SubCategory { get; set; }

        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
        public virtual IEnumerable<ProductInOrder>? ProductsInOrder { get; set; }

        [Required]
        public GenderType? GenderType { get; set; }

        [Required]
        public AgeGroup AgeGroup { get; set; }

        public virtual ICollection<ProductsBySize>? ProductBySizes { get; set; }
    }
}

