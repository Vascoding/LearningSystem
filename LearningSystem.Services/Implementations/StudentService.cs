using LearningSystem.Data;
using LearningSystem.Data.Enumerations;
using LearningSystem.Data.Models;
using LearningSystem.Data.ViewModels.Users;
using LearningSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly LearningSystemDbContext db;
        

        public StudentService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<StudentModel> All()
        {
            using (this.db)
            {
                return this.db.Users
                    .Select(u => new StudentModel
                    {
                        Id = u.Id,
                        Email = u.Email
                    }).ToList();
            }
        }

        public bool SignUp(string studenId, int courseId)
        {
            using (this.db)
            {
                if (this.db.Courses.FirstOrDefault(c => c.Id == courseId).StartDate > DateTime.UtcNow)
                {
                    this.db.UsersCourses.Add(new UserCourse
                    {
                        UserId = studenId,
                        CourseId = courseId
                    });

                    this.db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool SignOut(string studenId, int courseId)
        {
            using (this.db)
            {
                var userCourse = this.db.UsersCourses
                    .Where(u => u.UserId == studenId && u.CourseId == courseId)
                    .FirstOrDefault();

                if (userCourse != null && this.db.Courses.FirstOrDefault(c => c.Id == courseId).StartDate > DateTime.UtcNow)
                {
                    this.db.UsersCourses.Remove(userCourse);
                    this.db.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public IEnumerable<StudentModel> AllByCourse(string trainerId, int courseId)
        {
            using (this.db)
            {
                if (this.db.UsersCourses.FirstOrDefault(t => t.UserId == trainerId && t.CourseId == courseId && t.IsTrainer == true) == null)
                {
                    return null;
                }

                return this.db.UsersCourses
                    .Where(c => c.CourseId == courseId && c.IsTrainer == false)
                    .Select(u => new StudentModel
                    {
                        Id = u.User.Id,
                        Email = u.User.Email,
                        CourseId = courseId,
                        Grade = u.Grade
                    }).ToList();
            }
        }

        public bool SetGrade(Grade grade, string studentId, int courseId)
        {
            using (this.db)
            {
                var studentInCourse = this.db.UsersCourses.FirstOrDefault(uc => uc.UserId == studentId && uc.CourseId == courseId);

                if (studentInCourse != null)
                {
                    studentInCourse.Grade = grade;
                    this.db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
