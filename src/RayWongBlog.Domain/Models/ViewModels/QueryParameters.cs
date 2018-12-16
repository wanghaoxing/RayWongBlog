using RayWongBlog.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Domain.Models.ViewModels
{
   public abstract class QueryParameters
    {
        public int PageIndex { get; set; }= 0;

        public int PageSize { get; set; } = 20;
        public string OrderBy { get; set; } = nameof(Entity.Id);
    }
}
