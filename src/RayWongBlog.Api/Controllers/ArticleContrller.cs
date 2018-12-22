using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RayWongBlog.Api.Helpers;
using RayWongBlog.Domain.Interfaces.Repositorys;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using RayWongBlog.Domain.Models.ViewModels.Enums;
using RayWongBlog.Infrastructure.DataBase;
using RayWongBlog.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleContrller : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ArticleContrller> _logger;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public ArticleContrller(IArticleRepository articleRepository,
            IUnitOfWork unitOfWork,
            ILogger<ArticleContrller> logger,
            IMapper mapper,
            IUrlHelper urlHelper
            )
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }
        [HttpGet(Name = "GetArticles")]
        [RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.raywongblog.hateoas+json" })]
        public async Task<IActionResult> Get([FromQuery]ArticleParameters request, [FromHeader(Name = "Accept")] string mediaType)
        {
            var list = await _articleRepository.GetAllArticlesAsync(request);
            var previoustlink = list.GetHasPrevious ? CreateUri(request, PaginationUriType.PreviousPage) : null;
            var nextlink = list.GetHasNext ? CreateUri(request, PaginationUriType.NextPage) : null;
            var metadata = new
            {
                PageSize = list.PageSize,
                PageIndex = list.PageIndex,
                TotalCount = list.TotalCount,
                PageCount = list.PageCount,
                Previoustlink = previoustlink,
                Nextlink = nextlink,
                //list.GetHasPrevious
            };
            //_logger.LogInformation("test");
            //throw new Exception("fsf");

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));//序列化时候变成驼峰规范
            var viewList = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleViewModel>>(list);
            var shaped = viewList.ToDynamicIEnumerable(request.Fields);
            var shapedLinks = shaped.Select(r =>
              {
                  var dic = r as IDictionary<string, object>;
                  var articlelinks = CreateLinks((int)dic["Id"], request.Fields);
                  dic.Add("links", articlelinks);
                  return dic;
              });
            var links = CreateLinks(request, list.GetHasPrevious, list.GetHasNext);
            return Ok(new
            {
                value = shapedLinks,
                links
            });
        }

        [HttpGet("{id}", Name = "GetArticle")]
        public async Task<IActionResult> Get(int id, string fields)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(article);
            var shaped = articleViewModel.ToDynamic(fields);
            var links = CreateLinks(id, fields);
            var result = shaped as IDictionary<string, object>;
            result.Add("links", links);
            return Ok(result);
        }
        [HttpPost(Name = "CreateArticle")]
        [RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.raywongblog.article.create+json" })]
        [RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.raywongblog.hateoas+json" })]
        public async Task<IActionResult> Post(ArticleAddViewModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }
            var article = _mapper.Map<ArticleAddViewModel, Article>(request);
            article.LastModified = DateTime.Now;
            article.Createdtime = DateTime.Now;
            await _articleRepository.AddArticeAsync(article);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Save Faild!");
            }
            var viewModel = _mapper.Map<Article, ArticleAddViewModel>(article);
            var links = CreateLinks(article.Id);
            var dic = viewModel.ToDynamic() as IDictionary<string, object>;
            dic.Add("links", links);
            return CreatedAtRoute("GetArticle", new { id = article.Id }, dic);
        }

        private string CreateUri(ArticleParameters parameters, PaginationUriType uriType)
        {
            switch (uriType)
            {
                case PaginationUriType.PreviousPage:
                    var previours = new
                    {
                        PageIndex = parameters.PageIndex - 1,
                        PageSize = parameters.PageSize,
                        OrderBy = parameters.OrderBy,
                        //fields=parameters
                    };
                    return _urlHelper.Link("GetArticles", previours);
                case PaginationUriType.NextPage:
                    var next = new
                    {
                        PageIndex = parameters.PageIndex + 1,
                        PageSize = parameters.PageSize,
                        OrderBy = parameters.OrderBy,
                        //fields=parameters
                    };
                    return _urlHelper.Link("GetArticles", next);
                default:
                    var current = new
                    {
                        PageIndex = parameters.PageIndex,
                        PageSize = parameters.PageSize,
                        OrderBy = parameters.OrderBy,
                        //fields=parameters
                    };
                    return _urlHelper.Link("GetArticles", current);
            }
        }

        private IEnumerable<LinkResource> CreateLinks(int id, string fields = null)
        {
            var links = new List<LinkResource>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkResource(
                    _urlHelper.Link("GetArticle", new { id }), "self", "GET"));

            }
            else
            {
                links.Add(new LinkResource(
                                    _urlHelper.Link("GetArticle", new { id, fields }), "self", "GET"));
            }
            links.Add(
                new LinkResource(
                    _urlHelper.Link("DeleteArticle", new { id }), "delete_article", "DELETE"));
            return links;

        }
        private IEnumerable<LinkResource> CreateLinks(ArticleParameters request,
     bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource(
                    CreateUri(request, PaginationUriType.CurrentPage),
                    "self", "GET")
            };

            if (hasPrevious)
            {
                links.Add(
                    new LinkResource(
                        CreateUri(request, PaginationUriType.PreviousPage),
                        "previous_page", "GET"));
            }

            if (hasNext)
            {
                links.Add(
                    new LinkResource(
                        CreateUri(request, PaginationUriType.NextPage),
                        "next_page", "GET"));
            }

            return links;
        }
    }
}
