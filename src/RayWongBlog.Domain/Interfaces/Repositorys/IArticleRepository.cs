using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Domain.Interfaces.Repositorys
{
   public interface IArticleRepository
    {
        Task<PaginatedList<Article>> GetAllArticlesAsync(ArticleParameters request);

        Task AddArticeAsync(Article article);

        Task<Article> GetArticleByIdAsync(int id);

    }
}
