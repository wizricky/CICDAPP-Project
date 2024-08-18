using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.Domain
{
    public class SubCategory : BaseEntity
    {
        [Required]
        public string SubCategoryName { get; set; }
        [Required]
        public Guid? CategoryId { get; set; }
        [Required]
        public Category? Category { get; set; }

    }
}
