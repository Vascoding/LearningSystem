using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningSystem.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }  

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
