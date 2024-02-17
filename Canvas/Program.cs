using System;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using Canvas.helpers;
using Canvas.Models;
using Canvas.Services;

namespace Canvas
{
internal class Program
{
    static void Main(string[] args)
    {
        bool menuOn = true;

        while (menuOn == true) 
        {
            Console.WriteLine("Welcome to Canvas!");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Students");
            Console.WriteLine("3. Assignments");
            Console.WriteLine("4. Submissions");
            Console.WriteLine("5. Exit Canvas");

            string? mainChoice = Console.ReadLine();
            var courseHlpr = new CourseHelper();
            var studentHlpr = new StudentHelper();
            var assignmentHlpr = new AssignmentHelper();
            var SubmissionHlpr = new SubmissionHelper();

            switch(mainChoice) {
                case "1":
                    Console.WriteLine("N. New Course");
                    Console.WriteLine("D. Delete Course");
                    Console.WriteLine("I: Course Info");
                    Console.WriteLine("S. Search Courses");
                    Console.WriteLine("U. Update Course");

                    string? courseChoice = Console.ReadLine();

                    if(courseChoice?.ToUpper() == "N" ) {
                        courseHlpr.NewCourse();
                    }
                            
                    if(courseChoice?.ToUpper() == "D" ) {
                        courseHlpr.DeleteCourse();
                    }       

                    if(courseChoice?.ToUpper() == "I" ) {
                        courseHlpr.PrintCourseInfo();
                    }     

                    if(courseChoice?.ToUpper() == "S" ) {
                        courseHlpr.SearchCourses();
                    }

                    if(courseChoice?.ToUpper() == "U" ) {
                        courseHlpr.UpdateCourse();
                        }        
                break;

                case "2":
                    Console.WriteLine("N. New Student");
                    Console.WriteLine("D. Delete Student");
                    Console.WriteLine("E. Enroll Student in Course");
                    Console.WriteLine("R. Remove Student from Course");
                    Console.WriteLine("I: Student Info");
                    Console.WriteLine("S. Search Students");
                    Console.WriteLine("U. Update Student");
                        
                    string? studentChoice = Console.ReadLine();

                    if (studentChoice?.ToUpper() == "N") {
                        studentHlpr.NewStudent();
                    }
                    if (studentChoice?.ToUpper() == "E") {
                        studentHlpr.Enroll();
                    }
                    if (studentChoice?.ToUpper() == "R") {
                        studentHlpr.RemoveStudent();
                    }
                    if (studentChoice?.ToUpper() == "S") {
                        studentHlpr.SearchStudents();
                    }
                    if (studentChoice?.ToUpper() == "I") {
                        studentHlpr.PrintStudentInfo();
                    }
                    if (studentChoice?.ToUpper() == "D") {
                        studentHlpr.DeleteStudent();
                    }
                    if(studentChoice?.ToUpper() == "U" ) {
                        studentHlpr.UpdateStudent();
                    }  
                break;

                case "3":
                    Console.WriteLine("N. New Assignment");
                    Console.WriteLine("D. Delete Assignment");
                    Console.WriteLine("I. Assignment Info");
                    Console.WriteLine("S. Search Assignments");
                    Console.WriteLine("U. Update Assignment");

                    string? assignmentChoice = Console.ReadLine();

                    if(assignmentChoice?.ToUpper() == "N" ) {
                        assignmentHlpr.NewAssignment();
                    }
                    if(assignmentChoice?.ToUpper() == "D" ) {
                        assignmentHlpr.DeleteAssignment();
                    }
                    if(assignmentChoice?.ToUpper() == "I" ) {
                        assignmentHlpr.PrintAssignmentInfo();
                    }
                    if(assignmentChoice?.ToUpper() == "S" ) {
                        assignmentHlpr.SearchAssignments();
                    }
                    if(assignmentChoice?.ToUpper() == "U" ) {
                        assignmentHlpr.Update();
                    }
                break;

                case "4":
                    Console.WriteLine("N. New Submission");
                    Console.WriteLine("D. Delete Submission");
                    Console.WriteLine("S. Search Submissions");
                    Console.WriteLine("U. Update Submission");

                    string? submissionChoice = Console.ReadLine();

                    if(submissionChoice?.ToUpper() == "N" ) {
                            SubmissionHlpr.NewSubmission();
                    }
                    if(submissionChoice?.ToUpper() == "D" ) {
                        SubmissionHlpr.DeleteSubmission();
                    }
                    if(submissionChoice?.ToUpper() == "S" ) {
                        SubmissionHlpr.Search();
                    }
                    if(submissionChoice?.ToUpper() == "U" ) {
                        SubmissionHlpr.Update();
                    }
                break;

                case "5":
                    menuOn = false;
                break;
            }   
        }
    }
}
}
