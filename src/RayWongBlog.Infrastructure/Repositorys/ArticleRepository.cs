using Microsoft.EntityFrameworkCore;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using RayWongBlog.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Infrastructure.Repositorys
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddArticeAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task<PaginatedList<Article>> GetAllArticlesAsync(ArticleParameters request)
        {
            var query = _context.Articles.OrderBy(x => x.Id);
            var count = await query.CountAsync();
            var data= await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PaginatedList<Article>(request.PageIndex,request.PageSize,count,data);
        }


        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
    }
}

