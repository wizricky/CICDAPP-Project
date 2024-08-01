using FlexForge.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<FlexForgeApplicationUser> GetAll();
        FlexForgeApplicationUser Get(string? id);
        void Insert(FlexForgeApplicationUser entity);
        void Update(FlexForgeApplicationUser entity);
        void Delete(FlexForgeApplicationUser entity);
    }
}
