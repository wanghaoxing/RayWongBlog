using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Domain.Interfaces.Repositorys
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
