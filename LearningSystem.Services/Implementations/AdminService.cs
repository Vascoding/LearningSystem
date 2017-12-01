using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Admin;
using LearningSystem.Data.ViewModels.Users;
using LearningSystem.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public AdminService(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public SetTrainerModel FindTrainers(int id)
        {
            using (this.db)
            {
                var model = new SetTrainerModel();
                var course = this.db.Courses.FirstOrDefault(c => c.Id == id);

                if (course != null)
                {
                    model.CourseId = course.Id;
                    model.CourseName = course.Name;
                }

                var students = this.db.Users
                    .Select(u => new StudentModel
                    {
                        Id = u.Id,
                        Email = u.Email
                    })
                    .ToList();

                if (students.Count() != 0)
                {
                    model.Students = students;
                }

                return model;
            }
        }

        public void SetBlogger(string id)
        {
            using (this.db)
            {
                var blogger = this.db.Users.FirstOrDefault(b => b.Id == id);

                if (blogger != null)
                {
                    Task
                  .Run(async () =>
                  {
                      var isInRole = await userManager.IsInRoleAsync(blogger, "BlogAuthor");
                      if (!isInRole)
                      {
                          await userManager.AddToRoleAsync(blogger, "BlogAuthor");

                          db.SaveChanges();
                      }
                  })
                  .Wait();
                }
            }
        }
    }
}
