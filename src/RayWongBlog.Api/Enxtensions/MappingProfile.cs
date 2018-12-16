using AutoMapper;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayWongBlog.Api.Enxtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.LastModifiedTime, opt => opt.MapFrom(src => src.LastModified));
            
            CreateMap<ArticleViewModel, Article>();
        }
    }
}
