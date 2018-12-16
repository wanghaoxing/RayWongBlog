using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleContrller:Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleContrller(IArticleRepository articleRepository,
            IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _articleRepository.GetAllArticles();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _articleRepository.AddArtice(new Article
            {
                Author = "admin",
                Content = "test",
                Title = "test title",
                Createdtime = DateTime.Now,
                LastModified = DateTime.Now

            });
           await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
