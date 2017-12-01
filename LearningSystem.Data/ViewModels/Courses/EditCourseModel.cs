using System;
using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Data.ViewModels.Courses
{
    public class EditCourseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
