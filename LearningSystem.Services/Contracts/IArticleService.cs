using LearningSystem.Data.ViewModels.Articles;
using System;
using System.Collections.Generic;

namespace LearningSystem.Services.Contracts
{
    public interface IArticleService
    {
        void Create(string title, string content, DateTime publishDate, string authorId);

        IEnumerable<ArticleModel> All();
    }
}
