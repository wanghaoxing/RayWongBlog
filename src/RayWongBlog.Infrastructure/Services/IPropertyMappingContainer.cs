using RayWongBlog.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.Services
{
    public interface IPropertyMappingContainer
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : Entity;

        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : Entity;
    }
}
