using Microsoft.EntityFrameworkCore;
using RayWongBlog.Domain.Models.Entitys;
using RayWongBlog.Infrastructure.DataBase.EntityConfigruation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.DataBase
{
   public  class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        }

        public DbSet<Article> Articles { get; set; }
    }
}
