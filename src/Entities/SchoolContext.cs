using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class SchoolContext : DbContext, ITenantAwareSchoolContext
    {
        private string _tenantId;
        private ITenantAwareDbContextOptionsProvider _dbContextOptionsProvider;

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        public SchoolContext(
            string tenantId,
            ITenantAwareDbContextOptionsProvider dbContextOptionsProvider)
        {
         

            _tenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            _dbContextOptionsProvider = dbContextOptionsProvider ?? throw new ArgumentNullException(nameof(dbContextOptionsProvider));
           
        }
        public DbContext DbContext => this;
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_tenantId))
            {
                base.OnConfiguring(optionsBuilder);
            }
            else
            {
                _dbContextOptionsProvider.OnConfiguring(_tenantId, optionsBuilder);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }

}
