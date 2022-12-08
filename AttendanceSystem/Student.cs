using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get;  set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<CourseStudent> EnrolledCourses { get; set; }
        public List<StudentAttendance> AttendanceList { get; set; }

        public void StudentLogin(string username, string password)
        {
            var context = new AttendanceDbContext();
            var studentLogin = new Print();

            var studentInfo = context.Students.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            //var studentId = studentInfo.Id;

            if(studentInfo != null)
            {
                if(username == studentInfo.UserName & password == studentInfo.Password)
                {
                    Console.WriteLine($"\nHello!!!! {studentInfo.FullName} :)");
                    Console.WriteLine("Wellcome to your Attendance Profile.");
                    Console.WriteLine("------------------------------------");

                    var isEnrolled = context.AssingedCoursesToStudents.Where(sid => sid.StudentId.Equals(studentInfo.Id)).FirstOrDefault();
                    var enrolledCourses = context.AssingedCoursesToStudents.Where(sid => sid.StudentId.Equals(studentInfo.Id)).ToList();

                    //Console.WriteLine(enrolledCourses.StudnetId);
                    if (isEnrolled != null)
                    {
                        
                        Console.Write("\nEnrolled Courses: ");
                        foreach (var course in enrolledCourses)
                        {
                            var courseTitle = context.Courses.Where(cid => cid.Id.Equals(course.CourseId)).Select(title => title.Title).FirstOrDefault();


                            Console.Write($" {courseTitle}");


                        }

                        StudentAttendance(studentInfo.Password, studentInfo.Id);  //Method for attendance
                    }
                    else
                    {
                        Console.WriteLine("\nYou are not enrolled in any course.");
                        Console.WriteLine("Contact the admin!!!!\n");
                    }


                }
                else
                {
                    Console.WriteLine("Wrong user and password!!!\n");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("User doesn't exist or wrong Password or wrong Username\n");
            }
        }
        
        public void StudentAttendance(string studentPass,int studentId)
        {
            var print = new Print();
            int flag = 0, temp = 0;
            var context = new AttendanceDbContext();
            var LoginAgain = new Print();
            DateTime d = DateTime.Now;

            Console.WriteLine();
            Console.WriteLine("\nSelect The Course for attendance ->");
            Console.Write("Course Title: ");
            var title = Console.ReadLine();

            var CourseScheduleList = context.ClassSchedules
                .Where(c => c.Title.Equals(title))
                .ToList();
            

            if(CourseScheduleList.Any())
            {
                
                foreach (var course in CourseScheduleList)
                {

                    if (d.TimeOfDay >= course.StartTime.TimeOfDay & d.TimeOfDay <= course.EndTime.TimeOfDay & d.DayOfWeek.ToString() == course.Day)
                    {
                        Console.WriteLine($"Course Title         Start Time       EndTime        Week Day");
                        Console.WriteLine("");
                        Console.WriteLine($"{course.Title}         {course.StartTime.TimeOfDay}       {course.EndTime.TimeOfDay}        {course.Day}");
                        Console.WriteLine("");
                        Console.Write("Enter your password to Confirm:");
                        var password = Console.ReadLine();
                        if(password == studentPass)
                        {
                            StudentAttendance attendanceInfo = new StudentAttendance();
                            //attendanceInfo.Mark = 1;
                            attendanceInfo.CourseTitle = course.Title;
                            attendanceInfo.Date = DateTime.Now;
                            attendanceInfo.StudentId = studentId;
                            context.StudentAttendances.Add(attendanceInfo);
                            context.SaveChanges();
                            flag = 1;
                            Console.WriteLine("\nDone Successfully!!!\n");

                        }
                        else
                        {
                            Console.WriteLine("\nWrong Password!!!!!!\n");
                            Console.WriteLine("");
                            LoginAgain.StudentLoginInfo();
                        }
                    }
    
                }
            }
            else 
            {
                temp = 1;
                Console.WriteLine("\nThere is no Schedule for this Course Today / A wrong Title / No Schedule has been set yet.....\n");
            }

            if(flag != 1 & temp == 0)
            {
                Console.WriteLine("\nOut of Time!!!!\n");
            }

            print.printOptionsForLogin();
            //var studentInfo = context.Students.Where(y => y.Password.Equals(pass)).FirstOrDefault();



            /*if (pass == studentPass)
            {
                *//*Console.OutputEncoding = System.Text.Encoding.Unicode;
                string a = "\x221A", b = "\x0058";
                Console.WriteLine(a);
                Console.WriteLine(b);*//*

               

            }*/

        }
    }
}
