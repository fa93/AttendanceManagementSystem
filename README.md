# Attendance Management System
A console project to create an Attendance System which is managed by an Admin.
## Installation

1) Clone the project
```bash
  https://github.com/fa93/AttendanceManagementSystem.git
```
2) Run the file with extension ` .Sln ` on Visual Studio

3) Set the `DefaultConnection` in `AttendanceDbContext` file 
```bash
 _connectionString = "";
```

4) Now, Create and update the migrations by running the following commands on ``` Package Manager Console ```
```bash
dotnet-ef migrations add Give_A_Name --project AttendanceSystem
dotnet ef database update --project AttendanceSystem
```
⚠️ Must install ` Microsoft Visual Studio `, ` Microsoft SQL Server` and `SQL Server Management Studio` on your device

## Features
- Admin can create and remove teachers, students and courses.
- Can assign courses to the teachers and the students and set the schedule for the courses. 
- Students can give attendance in between the course schedule. 
- Teachers can see the attendance list for a particular course.

## Tech Stack

**Backend:** ASP.NET Core 6, Entity Framework core

**Server:**  Microsoft SQL Server

