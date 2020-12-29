using System;
using System.Collections.Generic;

namespace Entities
{
    public class Student:BaseEntity
    {
     
        public string StudentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}