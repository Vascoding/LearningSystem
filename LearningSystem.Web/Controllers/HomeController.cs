using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningSystem.Web.Models;
using LearningSystem.Services.Contracts;
using System.Collections.Generic;
using LearningSystem.Data.ViewModels.Courses;
using System.Linq;

namespace LearningSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courses;

        public HomeController(ICourseService courses)
        {
            this.courses = courses;
        }

        public IActionResult Index(string searchTerm)
        {
            IEnumerable<CoursesModel> model = Enumerable.Empty<CoursesModel>();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                model = this.courses.AllBySearch(searchTerm);
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
