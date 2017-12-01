using System;

namespace LearningSystem.Data.ViewModels.Articles
{
    public class ArticleModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }
    }
}
