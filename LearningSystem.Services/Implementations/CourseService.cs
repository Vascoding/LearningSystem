using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Courses;
using LearningSystem.Data.ViewModels.Users;
using LearningSystem.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public CourseService(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public void Create(CreateCourseModel courseModel)
        {
            using (this.db)
            {
                this.db.Courses.Add(new Course
                {
                    Name = courseModel.Name,
                    Description = courseModel.Description,
                    StartDate = courseModel.StartDate,
                    EndDate = courseModel.EndDate,
                });

                this.db.SaveChanges();
            }
        }

        public IEnumerable<CoursesModel> Courses()
        {
            using (this.db)
            {
                return this.db.Courses
                    .Select(c => new CoursesModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Students = c.Users.Count(),
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        StudentsEmails = c.Users.Select(s => s.User.Email)
                    }).ToList();
            }
        }

        public EditCourseModel FindToEdit(int id)
        {
            using (this.db)
            {
                return this.db.Courses
                    .Where(c => c.Id == id)
                    .Select(c => new EditCourseModel
                    {
                        Id = c.Id,
                        EndDate = c.EndDate,
                        Name = c.Name,
                        StartDate = c.StartDate
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(EditCourseModel model)
        {
            using (this.db)
            {
                var course = this.db.Courses
                    .FirstOrDefault(c => c.Id == model.Id);

                if (course != null)
                {
                    course.Name = model.Name;
                    course.StartDate = model.StartDate;
                    course.EndDate = model.EndDate;
                    course.Description = model.Description;

                    this.db.SaveChanges();
                }
            }
        }

        public void AddTrainer(TrainerModel model)
        {
            using (this.db)
            {
                var trainer = this.db.Users
                    .FirstOrDefault(u => u.Id == model.TrainerId);

                var course = this.db.Courses.
                    FirstOrDefault(c => c.Id == model.CourseId);

                if (trainer != null && course != null && !this.db.UsersCourses.Any(uc => uc.UserId == trainer.Id && uc.CourseId == course.Id))
                {
                    this.db.UsersCourses.Add(new UserCourse
                    {
                        CourseId = course.Id,
                        UserId = trainer.Id,
                        IsTrainer = true
                    });

                    Task
                   .Run(async () =>
                   {
                       var isInRole = await userManager.IsInRoleAsync(trainer, "Trainer");
                       if (!isInRole)
                       {
                           await userManager.AddToRoleAsync(trainer, "Trainer");

                           db.SaveChanges();
                       }
                   })
                   .Wait();

                    this.db.SaveChanges();
                }
            }
        }

        public IEnumerable<CoursesModel> AllBySearch(string searchTerm)
        {
            using (this.db)
            {
                return this.db.Courses
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => new CoursesModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Students = c.Users.Count(),
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        StudentsEmails = c.Users.Select(s => s.User.Email)
                    }).ToList();
            }
        }
    }
}
