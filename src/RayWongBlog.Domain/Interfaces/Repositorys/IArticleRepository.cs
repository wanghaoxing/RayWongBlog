using RayWongBlog.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Domain.Interfaces.Repositorys
{
   public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();

        Task AddArtice(Article article);

    }
}
