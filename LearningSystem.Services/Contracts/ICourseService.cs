using LearningSystem.Data.ViewModels.Courses;
using LearningSystem.Data.ViewModels.Users;
using System.Collections.Generic;

namespace LearningSystem.Services.Contracts
{
    public interface ICourseService
    {
        void Create(CreateCourseModel courseModel);

        IEnumerable<CoursesModel> Courses();

        EditCourseModel FindToEdit(int id);

        void Edit(EditCourseModel model);

        void AddTrainer(TrainerModel model);

        IEnumerable<CoursesModel> AllBySearch(string searchTerm);
    }
}
