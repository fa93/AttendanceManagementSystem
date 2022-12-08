using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class Print
    {
        public int Choose { get; set; }

        /// <summary>
        /// /options for login
        /// </summary>
        public void printOptionsForLogin()
        {
            
            Console.WriteLine("--------------------Login as-------------------");
            Console.WriteLine("1.Admin");
            Console.WriteLine("2.Teacher");
            Console.WriteLine("3.Student");
            Console.WriteLine("4.Exit");

            Console.WriteLine("-----------------------------------------------");
            Console.Write("Choose(Press 1/2/3):");
            Choose = int.Parse(Console.ReadLine());
            Console.WriteLine();

            
            if (Choose >= 1 && Choose <= 4)
            {
                switch (Choose)
                {
                    case 1:
                        Console.Write("User Name:");
                        var userId = Console.ReadLine();
                        Console.Write("Password :");
                        var password = Console.ReadLine();
                        var admin = new Admin();
                        admin.Login(userId, password);
                        break;

                    case 2:
                        TeacherLoginInfo();
                        break;

                    case 3:
                        StudentLoginInfo();
                        break;

                    case 4:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid Number!!!");
                printOptionsForLogin();
            }
        }

        public void StudentLoginInfo()
        {
            Console.Write("User Name:");
            var SuserId = Console.ReadLine();
            Console.Write("Password :");
            var Spassword = Console.ReadLine();
            var student = new Student();
            student.StudentLogin(SuserId, Spassword);
        }

        public void TeacherLoginInfo()
        {
            Console.Write("User Name:");
            var TuserId = Console.ReadLine();
            Console.Write("Password :");
            var Tpassword = Console.ReadLine();
            var teacher = new Teacher();
            teacher.TeacherLogin(TuserId, Tpassword);
        }


        /// <summary>
        /// admin options
        /// </summary>
        public void adminOptions()
        {
            Console.WriteLine("--------------------Admin Panel-------------------");
            Console.WriteLine("1.Create Teacher");
            Console.WriteLine("2.Create Course");
            Console.WriteLine("3.Create Student");
            Console.WriteLine("4.Assign a Teacher in a Course");
            Console.WriteLine("5.Assign a Student in a Course");
            Console.WriteLine("6.Set Class Schedule for a Course");
            Console.WriteLine("7.Remove Teacher");
            Console.WriteLine("8.Remove Course");
            Console.WriteLine("9.Remove Student");
            Console.WriteLine("10.Delete Class Schedule");
            Console.WriteLine("11.Show Students' List");
            Console.WriteLine("12.Show Courses' List");
            Console.WriteLine("13.Show Teachers' List");
            Console.WriteLine("14.Logout");
            Console.WriteLine("-----------------------------------------------");
            Console.Write("Choose(Press 1/2/3....):");
            Choose = int.Parse(Console.ReadLine());
            Console.WriteLine();
            var admin = new Admin();
            if (Choose >= 1 && Choose <= 14)
            {
                switch (Choose)
                {
                    case 1:
                        admin.AddDataToTeacher();
                        break;

                    case 2:
                        admin.AddDataToCourse();
                        break;

                    case 3:
                        admin.AddDataToStudent();
                        break;
                    case 4:
                        admin.AssignTeacher();
                        break;

                    case 5:
                        admin.AssignStudent();
                        break;

                    case 6:
                        admin.AddClassSchedule();
                        break;
                    case 7:
                        admin.DeleteTeacher();
                        break;

                    case 8:
                        admin.DeleteCourse();
                        break;

                    case 9:
                        admin.DeleteStudent();
                        break;
                    case 10:
                        ShowScheduledCourses();
                        admin.DeleteClassSchedule();
                        break;
                    case 11:
                        ShowStudentList();
                        adminOptions();
                        break;
                    case 12:
                        ShowCourseList();
                        adminOptions();
                        break;
                    case 13:
                        ShowTeacherList();
                        adminOptions();
                        break;
                    case 14:
                        Console.WriteLine("Logout!!!");
                        printOptionsForLogin();
                        break;


                }
            }
            else
            {
                Console.WriteLine("Invalid Number!!!");
                adminOptions();
            }

        }


        /// <summary>
        /// Set Teacher
        /// </summary> 
        public Teacher SetTeacherInfo()
        {
            var teacher = new Teacher();
            Console.WriteLine("~~~~~~~~~~Enter Teacher Information~~~~~~~~~");
            Console.Write("Full Name:");
            teacher.FullName = Console.ReadLine();
            Console.Write("User Name:");
            teacher.UserName = Console.ReadLine();
            Console.Write("Password:");
            teacher.Password = Console.ReadLine();
            Console.Write("Address:");
            teacher.Address = Console.ReadLine();
            Console.WriteLine("");
            
            return teacher;
        }

        /// <summary>
        /// Set Course
        /// </summary>
        public Course SetCourseInfo()
        {
            var course = new Course();
            Console.WriteLine("~~~~~~~~~~Enter Course Information~~~~~~~~~");
            Console.Write("Title:");
            course.Title = Console.ReadLine();
            Console.Write("Fees:");
            course.Fees = decimal.Parse(Console.ReadLine());
            Console.WriteLine("");

            return course;
        }

        /// <summary>
        /// Set student
        /// </summary>
        public Student SetStudentInfo()
        {
            var student = new Student();
            Console.WriteLine("~~~~~~~~~~Enter Student Information~~~~~~~~~");
            Console.Write("Full Name:");
            student.FullName = Console.ReadLine();
            Console.Write("User Name:");
            student.UserName = Console.ReadLine();
            Console.Write("Password:");
            student.Password = Console.ReadLine();
            Console.Write("Address:");
            student.Address = Console.ReadLine();
            Console.WriteLine("");

            return student;
        }

        /// <summary>
        /// Set Class Schedule
        /// </summary>    
        public List<ClassSchedule> SetClassSchedule()
        {
            var classSchedule = new List<ClassSchedule>();
            
            ShowClassScheduleToAdmin();
            

            Console.Write("How many days in a week?(Atleast 2 and Atmost 5) Ans: ");
            var days = int.Parse(Console.ReadLine());
            if (days > 5 || days < 2)
            {
                Console.WriteLine("Days' Limit: Atmost 5 days and atleast 2 days in a week .\n");
               
                SetClassSchedule();

            }

            var dayTime = SetDayTimeForCS(days);

            var startDate = SetStartDate(dayTime, days);

            Console.Write("Enter Total Number of classes:");
            var noOfCalsses = int.Parse(Console.ReadLine());

            foreach (var day in dayTime)
            {
                classSchedule.Add(new ClassSchedule() {Day = day.WeekDay, StartTime = day.StartTimeOfTheDay, 
                    EndTime = day.EndTimeOfTheDay, StartDate = startDate, NoOfCalsses = noOfCalsses });
            }

            return classSchedule;

        }

        enum WeekDays
        { 
            Saturday,
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday
        }

        public List<SetDayAndTime> SetDayTimeForCS(int count)
        {
            var daysTime = new List<SetDayAndTime>();
            for (int i = 0; i < count; i++)
            {
                Console.Write($"\nEnter Day{i + 1} of Week: ");
                var weekDay = weekkDay();
                Console.Write($"Start Time for {weekDay} as Like 8PM: ");
                var startTime = DateTime.Parse(Console.ReadLine());
                Console.Write($"End Time for {weekDay} as Like 10PM: ");
                var endTime = DateTime.Parse(Console.ReadLine());

                daysTime.Add(new SetDayAndTime() { WeekDay = weekDay, StartTimeOfTheDay = startTime, EndTimeOfTheDay = endTime });
            }

            return daysTime;
        }

        public string weekkDay()
        {
            var weekday = Console.ReadLine();
            var day = "";
            if(weekday == WeekDays.Sunday.ToString() 
                || weekday == WeekDays.Monday.ToString() 
                || weekday == WeekDays.Tuesday.ToString()
                || weekday == WeekDays.Wednesday.ToString() 
                || weekday == WeekDays.Thursday.ToString() 
                || weekday == WeekDays.Friday.ToString()
                || weekday == WeekDays.Saturday.ToString()) 
            {
                day = weekday;      
            }

            else
            {
                Console.WriteLine("\nWrong Spelling!!! or Please Start with a capital letter.\n");
                Console.Write("Enter Again: ");
                return weekkDay();  
            }
            Console.WriteLine();

            return day;
        }

        public DateTime SetStartDate(List<SetDayAndTime> daysTime, int days)
        {

            int flag = 0;
            
            Console.Write("\nStart date for the Course (MM-dd-yyyy): ");
            var startDate = Console.ReadLine();

            DateTime d;

            bool checkFormat = DateTime.TryParseExact(startDate,"MM-dd-yyyy",null,System.Globalization.DateTimeStyles.None, out d );

            if(checkFormat == false)
            {
                Console.WriteLine("\nWrong Format!!! Follow this pattern -> MM-dd-yyyy(12-30-2021)\n");

                return SetStartDate(daysTime, days); //if we don't assign the value to d otherwise it will return the bottom value
            }

            else
            {
               

                foreach (var day in daysTime)
                {
                    if (d.DayOfWeek.ToString() == day.WeekDay)
                    {
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    Console.WriteLine("\nStart day is not matching with the given days of class Schedule.\n");

                    return SetStartDate(daysTime, days);
                }
                else
                {
                    return d;
                }

            }
            
        }

        public void ShowScheduledCourses()
        {
            var context = new AttendanceDbContext();
            var ScheduledCourses = context.ClassSchedules.Select(x => x.Title).ToList();

            if (ScheduledCourses != null)
            {
                Console.Write("Scheduled Courses' Title: ");
                foreach (var course in ScheduledCourses)
                {
                    Console.Write($" {course} ");
                }
            }
            else
            {
                Console.WriteLine("Any Courses Has not Scheduled Yet!!!");
            }
           
        } 

        public void ShowCourseList()
        {
            var context = new AttendanceDbContext();
            var courses = context.Courses.Select(c => c.Title).ToList();

            if (courses != null)
            {
                Console.Write("List of Courses: ");
                foreach (var course in courses)
                {
                    Console.Write($" {course} ");
                }
            }
            else
            {
                Console.WriteLine("There is no such course!!!");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public void ShowStudentList()
        {
            var context = new AttendanceDbContext();
            var students = context.Students.Select(s => new  {s.FullName,s.UserName}).ToList();

            if (students != null)
            {
                Console.WriteLine("List of students: ");
                foreach (var student in students)
                {
                    Console.WriteLine($" {student.FullName} - {student.UserName}");
                }
            }
            else
            {
                Console.WriteLine("You have not created any student yet!!!");
            }
            Console.WriteLine();
        }

        public void ShowTeacherList()
        {
            var context = new AttendanceDbContext();
            var teachers = context.Teachers.Select(t => new { t.FullName, t.UserName }).ToList();

            if (teachers != null)
            {
                Console.WriteLine("List of teachers: ");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($" {teacher.FullName} - {teacher.UserName}");
                }
            }
            else
            {
                Console.WriteLine("You have not created any teacher yet!!!");
            }
            Console.WriteLine();
        }

        public void ShowAssignedCoursesToTeachers()
        {
            var context = new AttendanceDbContext();
            var AssignedTeacherList = context.AssingedCoursesToTeachers.ToList();

            Console.WriteLine("Assigned Courses' List:");

            foreach(var teacher in AssignedTeacherList)
            {
                var teacherName = context.Teachers.Where(n => n.Id == teacher.TeacherId).Select(n => n.FullName).FirstOrDefault();
                var CourseTitle = context.Courses.Where(c => c.Id == teacher.CourseId).Select(t => t.Title).FirstOrDefault();
                Console.WriteLine($"{teacherName} -> {CourseTitle}");
            }

            Console.WriteLine(" ");
        }
        public void ShowClassScheduleToAdmin()
        {
            var context = new AttendanceDbContext();
            var CourseScheduleList = context.ClassSchedules.Select(c => new {c.Title, c.StartTime, c.EndTime, c.Day, c.NoOfCalsses})
                .ToList();

            if(CourseScheduleList == null)
            {
                Console.WriteLine("No class schedule has set yet!!!/n");
               
            }
            else
            {
                Console.WriteLine("Title           Start Time           End time           Week Day           Total Classes");
                foreach (var course in CourseScheduleList)
                {
                    Console.WriteLine($"{course.Title}          {course.StartTime.TimeOfDay}          {course.EndTime.TimeOfDay}          {course.Day}           {course.NoOfCalsses}");
                }
                Console.WriteLine("");
            }
            
        }

    }
}