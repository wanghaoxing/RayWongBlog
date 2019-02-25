using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Domain.Models.ViewModels
{
   public class LinkResource
    {
        public LinkResource(string href,string rel,string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
        public string Href { get; set; }

        public string Rel { get; set; }

        public string Method { get; set; }
        public string Type { get; set; } = "application/vnd.raywongblog.hateoas+json";
    }
}
