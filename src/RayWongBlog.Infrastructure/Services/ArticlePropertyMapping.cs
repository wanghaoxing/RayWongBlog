using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.Services
{
    public class ArticlePropertyMapping : PropertyMapping<ArticleViewModel, Article>
    {
        public ArticlePropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(ArticleViewModel.Title)] = new List<MappedProperty>
            {
                new MappedProperty{Name=nameof(Article.Title),Revert=false}
            },
                [nameof(ArticleViewModel.Content)] = new List<MappedProperty>
            {
                new MappedProperty{Name=nameof(Article.Content),Revert=false}
            },
                [nameof(ArticleViewModel.Author)] = new List<MappedProperty>
            {
                new MappedProperty{Name=nameof(Article.Author),Revert=false}
            }
            })
        {

        }
    }
}
