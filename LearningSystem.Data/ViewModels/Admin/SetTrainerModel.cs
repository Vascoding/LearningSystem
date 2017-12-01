using LearningSystem.Data.ViewModels.Users;
using System.Collections.Generic;

namespace LearningSystem.Data.ViewModels.Admin
{
    public class SetTrainerModel
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public IEnumerable<StudentModel> Students { get; set; }
    }
}
