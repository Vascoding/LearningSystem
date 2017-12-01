using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Users;
using LearningSystem.Services.Contracts;
using LearningSystem.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class AdminController : Controller
    {
        private const string TrainerSet = "Trainer has been set";
        private const string BloggerSet = "Blogger has been set";

        private readonly IAdminService admins;
        private readonly IStudentService students;
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public AdminController(IAdminService admins, IStudentService students, ICourseService courses, UserManager<User> userManager)
        {
            this.admins = admins;
            this.students = students;
            this.userManager = userManager;
            this.courses = courses;
        }

        public IActionResult SetTrainer(int id)
        {
            var model = this.admins.FindTrainers(id);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult SetTrainer(TrainerModel model)
        {
            this.courses.AddTrainer(model);

            TempData["TrainerSet"] = TrainerSet;
            return this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
        }

        public IActionResult SetBlogger()
        {
            var model = this.students.All();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult SetBlogger(string id)
        {
            this.admins.SetBlogger(id);

            TempData["BloggerSet"] = BloggerSet;
            return this.Redirect("/articles/all");
        }
    }
}
