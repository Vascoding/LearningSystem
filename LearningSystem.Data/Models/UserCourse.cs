using LearningSystem.Data.Enumerations;

namespace LearningSystem.Data.Models
{
    public class UserCourse
    {
        public bool IsTrainer { get; set; } = false;

        public string UserId { get; set; }

        public User User { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public Grade? Grade { get; set; }
    }
}
