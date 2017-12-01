using System;
using System.Collections.Generic;

namespace LearningSystem.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<UserCourse> Users { get; set; } = new List<UserCourse>();
    }
}
