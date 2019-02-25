using FluentValidation;
using RayWongBlog.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayWongBlog.Infrastructure.DataBase.Validators
{
    public class ArticleValidator<T> : AbstractValidator<T> where T:ArticleAddOrUpdateViewModel
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
