using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public interface ITenantAwareSchoolContext : IDisposable
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        DbContext DbContext { get; }
        Task<int> SaveChangesAsync();

    }
}