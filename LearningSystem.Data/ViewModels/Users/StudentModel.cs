using LearningSystem.Data.Enumerations;

namespace LearningSystem.Data.ViewModels.Users
{
    public class StudentModel
    {
        public string Id { get; set; }
        
        public string Email { get; set; }

        public int CourseId { get; set; }

        public Grade? Grade { get; set; }
    }
}
