using Microsoft.EntityFrameworkCore;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using RayWongBlog.Infrastructure.DataBase;
using RayWongBlog.Infrastructure.Extensions;
using RayWongBlog.Infrastructure.Services;
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
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        public ArticleRepository(BlogContext context,
             IPropertyMappingContainer propertyMappingContainer)
        {
            _context = context;
            _propertyMappingContainer = propertyMappingContainer;
        }

        public async Task AddArticeAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task<PaginatedList<Article>> GetAllArticlesAsync(ArticleParameters request)
        {
            var query = _context.Articles.AsQueryable();
            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(r => r.Title.ToLowerInvariant().Contains(request.Title.ToLowerInvariant()));
            }
            query = query.ApplySort(request.OrderBy, _propertyMappingContainer.Resolve<ArticleViewModel, Article>());
            var count = await query.CountAsync();
            var data = await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PaginatedList<Article>(request.PageIndex, request.PageSize, count, data);
        }


        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
    }
}

