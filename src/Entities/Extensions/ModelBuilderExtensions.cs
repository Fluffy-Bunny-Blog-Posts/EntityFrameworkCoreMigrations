using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Extensions
{
    /// <summary>
    /// Extension methods to define the database schema for the configuration and operational data stores.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder,
            TableConfiguration configuration)
            where TEntity : class
        {
            return string.IsNullOrWhiteSpace(configuration.Schema)
                ? entityTypeBuilder.ToTable(configuration.Name)
                : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }

        /// <summary>
        /// Configures the client context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
 
        public static void ConfigureStudentContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(student =>
            {
                student.ToTable("student");
                student.HasKey(x => x.Id);
                student.Property(x => x.StudentId).HasMaxLength(200).IsRequired();
                student.HasIndex(x => x.StudentId).IsUnique();
                student.HasMany(x => x.Enrollments).WithOne(x => x.Student).HasForeignKey(x => x.StudentId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Course>(course =>
            {
                course.ToTable("course");
                course.HasKey(x => x.Id);
                course.Property(x => x.CourseId).HasMaxLength(200).IsRequired();
                course.HasIndex(x => x.CourseId).IsUnique();
                course.HasMany(x => x.Enrollments).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Enrollment>(enrollment =>
            {
                enrollment.HasKey(x => x.Id);
                enrollment.ToTable("enrollment");
                enrollment.Property(x => x.EnrollmentId).HasMaxLength(200).IsRequired();
                enrollment.HasIndex(x => x.EnrollmentId).IsUnique();

            });
        }
        /// <summary>
        /// Configures the client context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
 
        public static void ConfigureExternalServicesContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExternalService>(externalService =>
            {
                externalService.ToTable("ExternalService");
                externalService.HasKey(x => x.Id);
                externalService.Property(x => x.Name).HasMaxLength(200).IsRequired();
                externalService.HasIndex(x => x.Name).IsUnique();

            });
           
        }
    }
}
