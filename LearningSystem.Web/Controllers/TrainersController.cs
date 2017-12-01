using LearningSystem.Data.Enumerations;
using LearningSystem.Data.Models;
using LearningSystem.Services.Contracts;
using LearningSystem.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Trainer)]
    public class TrainersController : Controller
    {
        private const string SuccessGrade = "successfully set grade {0} to this student.";
        private const string TrainerError = "You are not trainer of this course!";

        private readonly IStudentService students;
        private readonly UserManager<User> userManager;

        public TrainersController(IStudentService students, UserManager<User> userManager)
        {
            this.students = students;
            this.userManager = userManager;
        }

        public IActionResult Students(int id)
        {
            var trainerId = this.userManager.GetUserId(HttpContext.User);

            var model = this.students.AllByCourse(trainerId, id);
            if (model == null)
            {
                TempData["error"] = TrainerError;
                return this.RedirectToAction(nameof(CoursesController.Courses), "Courses");
            }
            return this.View(model);
        }

        [HttpPost]
        public IActionResult SetGrade(Grade grade, string studentId, int courseId)
        {
            var success = this.students.SetGrade(grade, studentId, courseId);
            if (success)
            {
                TempData["Success"] = string.Format(SuccessGrade, grade);
                return this.Redirect($"/trainers/students/{courseId}");
            }

            return this.Redirect($"/trainers/students/{courseId}");
        }
    }
}
