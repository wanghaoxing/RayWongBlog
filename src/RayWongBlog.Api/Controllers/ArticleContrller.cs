using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
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
        private readonly IMapper _mapper;

        public ArticleContrller(IArticleRepository articleRepository,
            IUnitOfWork unitOfWork,
            ILogger<ArticleContrller> logger,
            IMapper mapper
            )
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _articleRepository.GetAllArticlesAsync();
            //_logger.LogInformation("test");
            //throw new Exception("fsf");
            var viewList = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleViewModel>>(list);
            return Ok(viewList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(article);
            return Ok(articleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _articleRepository.AddArticeAsync(new Article
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
