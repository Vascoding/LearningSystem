using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Articles;
using LearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class ArticleSercice : IArticleService
    {
        private readonly LearningSystemDbContext db;

        public ArticleSercice(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(string title, string content, DateTime publishDate, string authorId)
        {
            using (this.db)
            {
                this.db.Articles.Add(new Article
                {
                    AuthorId = authorId,
                    Content = content,
                    PublishDate = publishDate,
                    Title = title
                });

                this.db.SaveChanges();
            }
        }

        public IEnumerable<ArticleModel> All()
        {
            using (this.db)
            {
                return this.db.Articles
                    .Select(a => new ArticleModel
                    {
                        Title = a.Title,
                        AuthorId = a.Author.Email,
                        Content = a.Content,
                        PublishDate = a.PublishDate
                    })
                    .ToList();
            }
        }
    }
}
