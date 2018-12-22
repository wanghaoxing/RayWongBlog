using FluentValidation;
using RayWongBlog.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.DataBase.Validators
{
    public class ArticleValidator : AbstractValidator<ArticleAddViewModel>
    {
        public ArticleValidator()
        {
            RuleFor(r => r.Author
            ).NotNull()
            .WithName("作者")
            .WithMessage("required|{PropertyName}是必须的");
        }
    }
}
