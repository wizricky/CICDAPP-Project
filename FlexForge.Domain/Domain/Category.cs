using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Domain
{
    public class Category : BaseEntity
    {
        [Required]
        public string? CategoryName { get; set; }
        public string? CategoryImage {  get; set; } 
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
