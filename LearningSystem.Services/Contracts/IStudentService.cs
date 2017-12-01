using LearningSystem.Data.Enumerations;
using LearningSystem.Data.ViewModels.Users;
using System.Collections.Generic;

namespace LearningSystem.Services.Contracts
{
    public interface IStudentService
    {
        bool SignUp(string studenId, int courseId);

        bool SignOut(string studenId, int courseId);

        IEnumerable<StudentModel> All();

        IEnumerable<StudentModel> AllByCourse(string trainerId, int courseId);

        bool SetGrade(Grade grade, string studentId, int courseId);
    }
}
