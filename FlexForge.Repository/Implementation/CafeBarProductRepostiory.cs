using FlexForge.Domain.Domain;
using FlexForge.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Repository.Implementation
{
    public class CafeBarProductRepository<T> : ICafeBarProductRepository<T> where T : BaseEntity
    {
        private readonly CafeBarDBContext context;
        //private readonly CafeBarDBContext cafeBarDBContext;
        private DbSet<T> entities;

        public CafeBarProductRepository(CafeBarDBContext context)
        {
            this.context = context;
            entities = context.Set<T>();

        }
        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }
    }
}
