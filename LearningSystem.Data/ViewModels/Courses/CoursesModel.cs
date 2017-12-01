using System;
using System.Collections.Generic;

namespace LearningSystem.Data.ViewModels.Courses
{
    public class CoursesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Students { get; set; }

        public IEnumerable<string> StudentsEmails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
