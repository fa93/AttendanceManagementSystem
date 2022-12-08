using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Fees { get; set; }
        public List<CourseTeacher> AssignedTeachers { get; set; }
        public List<CourseStudent> EnrolledStudents { get; set; }
        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
