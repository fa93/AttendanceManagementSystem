using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class AttendanceDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public AttendanceDbContext()
        {
            _connectionString = ""; ;
            _assemblyName = typeof(Program).Assembly.FullName;
        }

        /// <summary>
        /// //cofiguring the database with the project and set it to use SqlServer, assemblyName is a name of the generated file after compilation
        /// </summary>
        /// <param name="dbContextOptionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_assemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseTeacher>()
                .HasKey(ct => new { ct.CourseId, ct.TeacherId });

            builder.Entity<CourseTeacher>() //Teacher Table
                .HasOne(ct => ct.Course)
                .WithMany(t => t.AssignedTeachers)
                .HasForeignKey(ct => ct.CourseId);

            builder.Entity<CourseTeacher>() //Course Table
                .HasOne(ct => ct.Teacher)
                .WithMany(c => c.AssignedCourses)
                .HasForeignKey(ct => ct.TeacherId);

            builder.Entity<CourseStudent>()
               .HasKey(cs => new { cs.CourseId, cs.StudentId });


            builder.Entity<CourseStudent>() //Student Table
                .HasOne(pc => pc.Course)
                .WithMany(p => p.EnrolledStudents)
                .HasForeignKey(pc => pc.CourseId);

            builder.Entity<CourseStudent>() //Course Table
                .HasOne(pc => pc.Student)
                .WithMany(c => c.EnrolledCourses)
                .HasForeignKey(pc => pc.StudentId);

            base.OnModelCreating(builder);
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseTeacher> AssingedCoursesToTeachers { get; set; }
        public DbSet<CourseStudent> AssingedCoursesToStudents { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<StudentAttendance> StudentAttendances { get; set; }
    }
}
