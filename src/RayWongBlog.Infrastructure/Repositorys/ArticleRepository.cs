using Microsoft.EntityFrameworkCore;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
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

        public async Task AddArtice(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _context.Articles.ToListAsync();
        }
    }
}
