namespace Entities
{
    public class Enrollment: BaseEntity
    {
        public string EnrollmentId { get; set; }
        
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public Grade? Grade { get; set; }

        public string Description { get; set; }

    }
}