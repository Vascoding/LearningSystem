using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Articles;
using LearningSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = "BlogAuthor, Administrator")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;
        private readonly UserManager<User> userManager;

        public ArticlesController(IArticleService articles, UserManager<User> userManager)
        {
            this.articles = articles;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult All(string searchTerm)
        {
            var model = this.articles.All();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = model.Where(c => c.Title.ToLower().Contains(searchTerm.ToLower()) || c.Content.ToLower().Contains(searchTerm.ToLower()));
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ArticleModel model)
        {
            var authorId = this.userManager.GetUserId(HttpContext.User);

            this.articles.Create(model.Title, model.Content, model.PublishDate, authorId);

            return this.RedirectToAction(nameof(All));
        }
    }
}
