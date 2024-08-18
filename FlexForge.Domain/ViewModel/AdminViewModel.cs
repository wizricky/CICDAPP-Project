using FlexForge.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Domain.ViewModel
{
    public class AdminViewModel
    {
        public Category? Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Product? Product { get; set; }
    }
}
