using LearningSystem.Data.ViewModels.Courses;
using LearningSystem.Services.Contracts;
using LearningSystem.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    public class CoursesController : Controller
    {
        private readonly ICourseService courses;

        public CoursesController(ICourseService courses)
        {
            this.courses = courses;
        }

        [AllowAnonymous]
        [Authorize]
        public IActionResult Courses()
        {
            var model = this.courses.Courses();

            return this.View(model);
        }

        
        public IActionResult Create()
        {
            return this.View();
        }
        
        [HttpPost]
        public IActionResult Create(CreateCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            this.courses.Create(model);

            return this.RedirectToAction(nameof(Courses));
        }

        public IActionResult Edit(int id)
        {
            var model = this.courses.FindToEdit(id);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Edit));
            }

            this.courses.Edit(model);

            return this.RedirectToAction(nameof(Courses));
        }
    }
}
