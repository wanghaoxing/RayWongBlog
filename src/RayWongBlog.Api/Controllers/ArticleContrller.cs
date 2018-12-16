using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ArticleContrller> _logger;

        public ArticleContrller(IArticleRepository articleRepository,
            IUnitOfWork unitOfWork,
            ILogger<ArticleContrller> logger
            )
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _articleRepository.GetAllArticles();
            //_logger.LogInformation("test");
            //throw new Exception("fsf");
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
