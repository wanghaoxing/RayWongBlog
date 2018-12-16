using Microsoft.Extensions.Logging;
using RayWongBlog.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayWongBlog.Infrastructure.DataBase
{
    public class BlogContextSeed
    {

        public static async Task SeedAsync(BlogContext context,
            ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                // myContext.Database.Migrate();

                if (!context.Articles.Any())
                {
                    context.Articles.AddRange(
                        new List<Article>{
                            new Article{
                                Title = "Title 1",
                                Content = "Content 1",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "PTitle 2",
                                Content = "Content 2",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 3",
                                Content = "Content 3",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 4",
                                Content = "Content 4",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 5",
                                Content = "Content 5",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 6",
                                 Content = "Content 6",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 8",
                                Content = "Content 8",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            },
                            new Article{
                                Title = "Title 8",
                                Content = "Content 8",
                                Author = "Ray",
                                LastModified = DateTime.Now,
                                Createdtime=DateTime.Now
                            }
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<BlogContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
