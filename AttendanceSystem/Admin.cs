using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class Admin : IUser
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
       
        public Admin() //default set
        {
            UserName = "unknown";
            Password = "12345";
        }
       
        //admin login check
        public void Login(string username, string password)
        {
            var printOptions = new Print();
            if (username == UserName & password == Password)
            {
                Console.WriteLine("SuccessFul Login!!!");
                printOptions.adminOptions();
            }
            else
            {
                printOptions.printOptionsForLogin();
            }
        }

        /*********************************
        *create teacher
        *********************************/
        public void AddDataToTeacher()
        {
            var context = new AttendanceDbContext();
            

            var print = new Print();
            var teacherInfo = print.SetTeacherInfo();

            var exists = context.Teachers.Where(x => x.UserName.Equals(teacherInfo.UserName)).FirstOrDefault();
            var exists1 = context.Teachers.Where(y => y.Password.Equals(teacherInfo.Password)).FirstOrDefault();
            /* Console.WriteLine(exists1.Password);
             Console.WriteLine(exists.UserName);*/
            if (exists != null)
            {
                if (teacherInfo.UserName == exists.UserName & teacherInfo.Password == exists1.Password)
                {
                    Console.WriteLine("UserName and Password must be unique!!!");
                    print.adminOptions();
                }
                else
                {
                    context.Teachers.Add(teacherInfo);
                    context.SaveChanges();
                    Console.WriteLine("Insert Successfully!!!");
                    print.adminOptions();
                }
            }

            else 
            {
                context.Teachers.Add(teacherInfo);
                context.SaveChanges();
                Console.WriteLine("Insert Successfully!!!");
                print.adminOptions();
            }


        } 

        //delete teacher
        public void DeleteTeacher()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            Console.Write("Enter Teacher's User Name:");
            var username = Console.ReadLine();
            var exists = context.Teachers.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            if(exists != null)
            {
                if (username == exists.UserName)
                {
                    var findTeachers = context.Teachers.Where(x => x.UserName == username).ToList();

                    foreach (var teacher in findTeachers)
                    {
                        context.Teachers.Remove(teacher);
                    }

                    context.SaveChanges();
                    Console.WriteLine("Delete Successful!!");
                    print.adminOptions();
                }
                else
                {
                    Console.WriteLine("Wrong UserName or UserName does not exist!!!");
                    print.adminOptions();
                }
            }
            else
            {
                Console.WriteLine("Empty Table!!! Insert data.");
                print.adminOptions();
            }
            
            
        }

        //create course
        //adding data to the database
        public void AddDataToCourse()
        {
            var context = new AttendanceDbContext();
            
            var print = new Print();
            var courseInfo = print.SetCourseInfo();

            var exists = context.Courses.Where(x => x.Title.Equals(courseInfo.Title)).FirstOrDefault();

            if (exists != null)
            {
                if (courseInfo.Title.ToUpper() == exists.Title.ToUpper())
                {
                    Console.WriteLine("Title must be unique!!!");
                    print.adminOptions();
                }
                else
                {
                    context.Courses.Add(courseInfo);
                    context.SaveChanges();
                    Console.WriteLine("Insert Successfully!!!");
                    print.adminOptions();
                }
            }

            else
            {
                context.Courses.Add(courseInfo);
                context.SaveChanges();
                Console.WriteLine("Insert Successfully!!!");
                print.adminOptions();
            }
               
        }

        //delete course
        public void DeleteCourse()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            Console.Write("Enter Course Title:");
            var title = Console.ReadLine();
            var exists = context.Courses.Where(x => x.Title == title).ToList();

            //Console.WriteLine(exists.Title);
            if (exists != null)
            {
                    foreach (var course in exists)
                    {
                        if(course.Title == title)
                        {
                            context.Courses.Remove(course);
                        }

                    }

                    context.SaveChanges();
                    Console.WriteLine("Delete Successful!!");
                    print.adminOptions();   
            }
            else
            {
                Console.WriteLine("There is no such course.");
                print.adminOptions();
            }
             

        }

        //assign a teacher to a course
        public void AssignTeacher()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            print.ShowCourseList();
            print.ShowTeacherList();
            print.ShowAssignedCoursesToTeachers();

            Console.Write("Enter Course Title:");
            var title = Console.ReadLine();
            Console.Write("Enter Teacher UserName:");
            var teacherUserName = Console.ReadLine();

            var existTeacher = context.Teachers.Where(x => x.UserName.Equals(teacherUserName)).FirstOrDefault();
            var existCourse = context.Courses.Where(y => y.Title.Equals(title)).FirstOrDefault();
            

            /* Console.WriteLine(existCourse.Title);
             Console.WriteLine(existTeacher.UserName);*/
            if (existTeacher != null & existCourse != null)
            {
                var isCourseIdExistinTeacherCourse = context.AssingedCoursesToTeachers.Where(z => z.CourseId.Equals(existCourse.Id)).FirstOrDefault();
                if(isCourseIdExistinTeacherCourse == null)
                {
                    if (existTeacher.UserName == teacherUserName & existCourse.Title == title)
                    {
                        var courseEnrollment = new CourseTeacher();
                        courseEnrollment.TeacherId = existTeacher.Id;
                        courseEnrollment.CourseId = existCourse.Id;
                        courseEnrollment.EnrollmentDate = DateTime.Now;

                        context.AssingedCoursesToTeachers.Add(courseEnrollment);
                        context.SaveChanges();
                        Console.WriteLine();
                        Console.WriteLine("Assigned Sucessfully!!!");
                        Console.WriteLine();
                        print.adminOptions();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Data!!!");
                        print.adminOptions();
                    }
                }
                else
                {
                    Console.WriteLine("Course already assigned to a Teacher!!");
                    print.adminOptions();
                }
            }
            else
            {
                Console.WriteLine("Invalid Data!!!");
                print.adminOptions();
            }
            
        }

        //create student
        public void AddDataToStudent()
        {
            var context = new AttendanceDbContext();


            var print = new Print();
            var studentInfo = print.SetStudentInfo();

            var exists = context.Students.Where(x => x.UserName.Equals(studentInfo.UserName)).FirstOrDefault();
            var exists1 = context.Students.Where(y => y.Password.Equals(studentInfo.Password)).FirstOrDefault();
            /* Console.WriteLine(exists1.Password);
             Console.WriteLine(exists.UserName);*/
            if (exists != null)
            {
                if (studentInfo.UserName == exists.UserName & studentInfo.Password == exists1.Password)
                {
                    Console.WriteLine("UserName and Password must be unique!!!");
                    print.adminOptions();
                }
                else
                {
                    context.Students.Add(studentInfo);
                    context.SaveChanges();
                    Console.WriteLine("Insert Successfully!!!");
                    print.adminOptions();
                }
            }

            else
            {
                context.Students.Add(studentInfo);
                context.SaveChanges();
                Console.WriteLine("Insert Successfully!!!");
                print.adminOptions();
            }



        }

        //delete student
        public void DeleteStudent()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            Console.Write("Enter Student's User Name:");
            var username = Console.ReadLine();
            var exists = context.Students.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            if (exists != null)
            {
                if (username == exists.UserName)
                {
                    var findStudents = context.Students.Where(x => x.UserName == username).ToList();

                    foreach (var student in findStudents)
                    {
                        context.Students.Remove(student);
                    }

                    context.SaveChanges();
                    Console.WriteLine("Delete Successful!!");
                    print.adminOptions();
                }
                else
                {
                    Console.WriteLine("Wrong UserName or UserName does not exist!!!");
                    print.adminOptions();
                }
            }
            else
            {
                Console.WriteLine("Empty Table!!! Insert data.");
                print.adminOptions();
            }

        }

        //assign a student to a  course
        public void AssignStudent()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            print.ShowCourseList();
            print.ShowStudentList();

            Console.Write("Enter Course Title:");
            var title = Console.ReadLine();
            Console.Write("Enter Student UserName:");
            var studentUserName = Console.ReadLine();

            var existStudent = context.Students.Where(x => x.UserName.Equals(studentUserName)).FirstOrDefault();
            var existCourse = context.Courses.Where(y => y.Title.Equals(title)).FirstOrDefault();


            /* Console.WriteLine(existCourse.Title);
             Console.WriteLine(existTeacher.UserName);*/
            if (existStudent != null & existCourse != null)
            {
                var isStudentIdExistinStudentCourse = context.AssingedCoursesToStudents.Where(z => z.StudentId == existStudent.Id && z.CourseId == existCourse.Id).FirstOrDefault();
                if (isStudentIdExistinStudentCourse == null)
                {
                    if (existStudent.UserName == studentUserName & existCourse.Title == title)
                    {
                        var courseEnrollment = new CourseStudent();
                        courseEnrollment.StudentId = existStudent.Id;
                        courseEnrollment.CourseId = existCourse.Id;
                        courseEnrollment.EnrollmentDate = DateTime.Now;

                        context.AssingedCoursesToStudents.Add(courseEnrollment);
                        context.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine("Enrolled Successfully!!!");
                        Console.WriteLine();
                        print.adminOptions();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid Data!!!");
                        Console.WriteLine();
                        print.adminOptions();
                    }
                }
                else
                {
                    Console.WriteLine("This student has already enrolled for this course!!");
                    print.adminOptions();
                }
            }
            else
            {
                Console.WriteLine("Invalid Data!!!");
                print.adminOptions();
            }

        }

        //set the class schedule 
        public void AddClassSchedule()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            Console.WriteLine("\nFor which course do you want to set a Schedule?\n");
            print.ShowCourseList();
            Console.Write("\nEnter course Name:");
            var title = Console.ReadLine();

            
            var isCourseExist = context.Courses.Where(x => x.Title.Equals(title)).FirstOrDefault();
            var ScheduleCheck = context.ClassSchedules.Where(c => c.Title.Equals(title)).FirstOrDefault();

            if(isCourseExist != null)
            {
                if(ScheduleCheck == null)
                {
                    
                    var classSchedule = print.SetClassSchedule();
                   // var InputClassSchedule = new ClassSchedule();
                        

                    foreach(var day in classSchedule)
                    {
                        
                        day.Title = title.ToUpper();
                        day.CourseId = isCourseExist.Id;

                        context.ClassSchedules.Add(day);
                        context.SaveChanges();   
                          
                    }

                    Console.WriteLine("\nInsert Successfully!!!\n");
                    print.adminOptions();

                }
                else
                {
                     Console.WriteLine($"Class Schedule has already been set for {title}");
                }
                
                   /* classSchedule.CourseId = isCourseExist.Id;
                    context.ClassSchedules.Add(classSchedule);
                    context.SaveChanges();
                    Console.WriteLine("");
                    Console.WriteLine("Insert Successfully!!!");
                    Console.WriteLine("");
                    print.ShowClassScheduleToAdmin();
                    Console.WriteLine("");
                    print.adminOptions();*/
              
            }
            else
            {
                Console.WriteLine("\nThere is no such Course!!!\n");
                print.adminOptions();
            }

            
            /* DateTime d = DateTime.Now;

             Console.WriteLine($"Using Now: {d.Hour}");

             Console.WriteLine();
             Console.WriteLine("-----Taking input from student---");
             Console.WriteLine();
             if (d.TimeOfDay >= classSchedule.StartTime.TimeOfDay  & d.TimeOfDay <= classSchedule.EndTime.TimeOfDay & d.DayOfWeek.ToString().ToUpper() == classSchedule.Day.ToUpper())
             {
                 Console.WriteLine($"The time {d.TimeOfDay} is in betwween start time {classSchedule.StartTime.TimeOfDay} and end time {classSchedule.EndTime.TimeOfDay} and Week day is {classSchedule.Day}");
             }
             else
             {
                 Console.WriteLine($"The time {d.TimeOfDay} Hour is not in betwween and the week day is {d.DayOfWeek}");
             }*/


        }

        //delete class schedule
        public void DeleteClassSchedule()
        {
            var context = new AttendanceDbContext();
            var print = new Print();

            Console.WriteLine();
            Console.Write("Enter Course Title: ");
            var Ctitle = Console.ReadLine();
            var isCourseExist = context.ClassSchedules.Where(x => x.Title.Equals(Ctitle)).FirstOrDefault();
            var isCourseExistInAttedance = context.StudentAttendances.Where(x => x.CourseTitle.Equals(Ctitle)).FirstOrDefault();

            if (isCourseExist != null)
            {
                
                    var findClassSchedule = context.ClassSchedules.Where(x => x.Title == Ctitle).ToList();

                    foreach (var Schedule in findClassSchedule)
                    {
                        context.ClassSchedules.Remove(Schedule);
                    }

                    context.SaveChanges();

                if (isCourseExistInAttedance != null & isCourseExistInAttedance.CourseTitle == isCourseExist.Title)
                {
                    var findCourse = context.StudentAttendances.Where(x => x.CourseTitle == Ctitle).ToList();

                    foreach (var Course in findCourse)
                    {
                        context.StudentAttendances.Remove(Course);
                    }

                    context.SaveChanges();
                }
                
                    Console.WriteLine("Delete Successful!!");
                    print.adminOptions();
                
            }
            else
            {
                Console.WriteLine("There is no such Course or Empty Table.\n");
                print.adminOptions();
            }

           
        }
    }
}
