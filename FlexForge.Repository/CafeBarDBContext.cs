using FlexForge.Domain.Domain;
using FlexForge.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Repository
{
    public class CafeBarDBContext : IdentityDbContext<FlexForgeApplicationUser>
    {
        public virtual DbSet<CafeBarProduct> Products { get; set; }

        public CafeBarDBContext(DbContextOptions<CafeBarDBContext> options)
           : base(options)
        {
        }
    }
}
