using LearningSystem.Data.Models;
using LearningSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    public class StudentsController : Controller
    {
        private const string SignOutError = "The course you want sign out for is allready started.";
        private const string SignUpError = "The course you want sign in for is allready started.";

        private readonly IStudentService students;
        private readonly UserManager<User> userManager;

        public StudentsController(IStudentService students, UserManager<User> userManager)
        {
            this.students = students;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Signup(int id)
        {
            var userId = this.userManager.GetUserId(User);

            if (!this.students.SignUp(userId, id))
            {
                TempData["error"] = SignUpError;
                this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
            }

            return this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
        }

        [Authorize]
        public IActionResult Signout(int id)
        {
            var userId = this.userManager.GetUserId(User);

            if (!this.students.SignOut(userId, id))
            {
                TempData["error"] = SignOutError;
                this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
            }
           
            return this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
        }
    }
}
