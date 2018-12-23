using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Domain.Models.ViewModels
{
   public  class ArticleAddOrUpdateViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }
        public string Remark { get; set; }
    }
}
