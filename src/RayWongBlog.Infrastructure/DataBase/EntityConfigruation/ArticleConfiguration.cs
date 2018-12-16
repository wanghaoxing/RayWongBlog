using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayWongBlog.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.DataBase.EntityConfigruation
{
  public   class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //sqlite alter column 不支持
            //builder.Property(r => r.Author).IsRequired().HasMaxLength(50);
            //builder.Property(r => r.Title).IsRequired().HasMaxLength(50);

            builder.Property(r => r.Remark).HasMaxLength(200);
        }
    }
}
