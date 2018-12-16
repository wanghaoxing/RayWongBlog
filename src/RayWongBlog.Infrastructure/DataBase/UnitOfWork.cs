using RayWongBlog.Domain.Interfaces.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;

        public UnitOfWork(BlogContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
