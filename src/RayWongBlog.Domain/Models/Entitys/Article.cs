using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Domain.Models.Entitys
{
   public  class Article: Entity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }
        public string Remark { get; set; }

        public DateTime Createdtime { get; set; }

        public DateTime LastModified { get; set; }

    }
}
