using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Course:BaseEntity
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}