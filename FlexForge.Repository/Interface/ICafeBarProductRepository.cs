using FlexForge.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Repository.Interface
{
    public interface ICafeBarProductRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
    }
}
