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
        string menu = "on";

        while (menu == "on") 
        {
            Console.WriteLine("Welcome to Canvas!");
            Console.WriteLine("C. Courses");
            Console.WriteLine("S. Students");
            Console.WriteLine("A. Assignments");
            Console.WriteLine("E. Exit Canvas");

            string? mainChoice = Console.ReadLine();
            var courseHlpr = new CourseHelper();
            var studentHlpr = new StudentHelper();
            var assignmentHlpr = new AssignmentHelper();
            var SubmissionHlpr = new SubmissionHelper();

            switch(mainChoice) {
                case "C":
                case "c":
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

                    if(courseChoice?.ToUpper() == "E" ) {
                        menu = "off";
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

                case "S":
                case "s":
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

                case "A":
                case "a":
                    Console.WriteLine("N. New Assignment");
                    Console.WriteLine("D. Delete Assignment");
                    Console.WriteLine("I. Assignment Info");
                    Console.WriteLine("S. Submissions");

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
                    if(assignmentChoice?.ToUpper() == "S" ) 
                    {
                        Console.WriteLine("N. New Submission");

                        string? submissionChoice = Console.ReadLine();

                        if(submissionChoice?.ToUpper() == "N" ) {
                            SubmissionHlpr.NewSubmission();
                        }
                    }
                break;

                case "E":
                case "e":
                    menu = "off";
                break;
            }   
        }
    }
}
}
