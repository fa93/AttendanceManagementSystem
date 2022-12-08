using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
    }
}
