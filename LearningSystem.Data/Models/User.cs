using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LearningSystem.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<Article> Articles { get; set; }

        public List<UserCourse> Courses { get; set; } = new List<UserCourse>();
    }
}
