using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<CourseTeacher> AssignedCourses { get; set; }

        public void TeacherLogin(string username, string password)
        {
            var context = new AttendanceDbContext();
            var teacherLogin = new Print();

            var teacherInfo = context.Teachers.Where(x => x.UserName.Equals(username)).FirstOrDefault();

            if (teacherInfo != null)
            {
                if(username == teacherInfo.UserName & password == teacherInfo.Password)
                {
                    Console.WriteLine($"\nHello!!!! {teacherInfo.FullName} :)");
                    showAttendance();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid Data!!!");
            }
        }

        public void showAttendance()
        {
            var print = new Print();
            var context = new AttendanceDbContext();
            var maxLength = 0;
            DateTime startDate = DateTime.Now;
            DateTime d = DateTime.Now;
            int count = 0, countdays = 0, datesCount = 1;
            string[] weekdays = new string[100];
            string[] dates = new string[100];

            Console.WriteLine("\nPermission: You can watch the students' attendance list of any course.... :)");
            Console.Write("\nEnter The Course Title:");
            var title = Console.ReadLine();

            var courseAttendance = context.StudentAttendances.Where(x => x.CourseTitle == title).ToList(); //who gave attendance
            var courseId = context.Courses.Where(x => x.Title == title).FirstOrDefault();
            var EnrolledStudents = context.AssingedCoursesToStudents.Where(x => x.CourseId == courseId.Id).Include(x => x.Student).ToList();//total enrolled students for a course
            var classSchedulesInfo = context.ClassSchedules.Where(x => x.Title == title).ToList();

            //calculatiing max length of a name
            foreach(var student in EnrolledStudents)
            {
                var currentLength = student.Student.FullName.Length;
                if(currentLength > maxLength)
                {
                    maxLength = currentLength;
                }
                count++;
                //Console.WriteLine($"\nTotal no. Students: {count}");
            }

            //collecting the week days
            foreach (var day in classSchedulesInfo)
            {
                startDate = day.StartDate.Date;

                weekdays[countdays] = day.Day;

                //Console.WriteLine($"\n {weekdays[countdays]}");

                countdays++;
            }

            Console.WriteLine("\n");

            //print all dates
            for(int j =0; ; j++)
            {
                int flag = 0;

                if (j == 0)
                {
                    int l = 0;
                    while (l != maxLength + 2)
                    {
                        Console.Write(" ");
                        l++;
                    }
                }

                else
                {
                    var temp = startDate;
                   

                    if (DateTime.Compare(startDate, d) < 0)
                    {
                        for (int k = 0; k < countdays; k++)
                        {
                            if (startDate.DayOfWeek.ToString() == weekdays[k])
                            {
                               
                                Console.Write($"{startDate.Date.ToShortDateString()}   ");
                                dates[j] = startDate.Date.ToShortDateString();
                                datesCount++;
                                startDate = startDate.AddDays(1).Date;
                                flag = 1;
                                break;
                            }
                        }
                        if (flag != 1)
                        {
                            startDate = startDate.AddDays(1).Date;
                        }
                    }

                    if (DateTime.Compare(startDate, d) == 0)
                    {
                        Console.Write($"{startDate.Date}   ");
                        dates[j] = startDate.Date.ToShortDateString();
                        datesCount++;
                        break;

                    }

                    if (DateTime.Compare(startDate, d) > 0)
                    {
                        break;
                    }

                    if (DateTime.Compare(temp, d) > 0)
                    {
                        Console.WriteLine("\n\n Class has not been started yet!!!");
                        break;
                    }
                }
            }

            Console.WriteLine("\n");


            

            //print attendance
            foreach (var enrolled in EnrolledStudents)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                string a = "\x221A", b = "\x0058";
                Console.Write($"{enrolled.Student.FullName}");

                int extraLength = maxLength - enrolled.Student.FullName.Length;
                while (extraLength > 0)
                {
                    Console.Write(" ");
                    extraLength--;

                }

                for (int l = 1; l < datesCount; l++)
                {
                    int space = startDate.ToShortDateString().Length;
                    if (courseAttendance != null)
                    {

                        foreach (var attendance in courseAttendance)
                        {
                            if (enrolled.StudentId == attendance.StudentId)
                            {
                                if (attendance.Date.ToShortDateString() == dates[l])
                                {


                                    while (space / 2 > 0)
                                    {
                                        Console.Write(" ");
                                        space--;
                                    }

                                    Console.Write($"{a}  ");
                                }
                                else
                                {
                                    while (space / 2 > 0)
                                    {
                                        Console.Write(" ");
                                        space--;
                                    }
                                    Console.Write($"{b}  ");
                                }
                            }
                            else
                            {
                                while (space / 2 > 0)
                                {
                                    Console.Write(" ");
                                    space--;
                                }
                                Console.Write($"{b}  ");
                            }
                        }
                    }

                    if(!courseAttendance.Any())
                    {
                        while (space / 2 > 0)
                        {
                            Console.Write(" ");
                            space--;
                        }
                        Console.Write($"{b}  ");

                    }

                }

                Console.WriteLine();
            }

            Console.WriteLine();
            print.printOptionsForLogin();
        }
    }
}
