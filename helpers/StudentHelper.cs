using Canvas.Services;
using Canvas.Models;
using System.Transactions;

namespace Canvas.helpers
{
    public class StudentHelper
    {
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;
        public void NewStudent()
        {   
            Console.WriteLine("Student Name:");
            var nameChoice = Console.ReadLine();

            Console.WriteLine("Student Year:");
            Console.WriteLine("1: Freshman");
            Console.WriteLine("2: Sophomore");
            Console.WriteLine("3: Junior");
            Console.WriteLine("4: Senior");
            Console.WriteLine("5: Graduate Student");

            string? yearChoice = Console.ReadLine();
            var year = "Freshman";

            switch(yearChoice) 
            {
                case "1": 
                    year = "Freshman";
                break;

                case "2": 
                    year = "Sophomore";
                break;

                case "3": 
                    year = "Junior";
                break;

                case "4": 
                    year = "Senior";
                break;

                case "5": 
                    year = "Graduate Student";
                break;
            }

            Console.WriteLine("Student created successfully!");

            Student myStudent = new Student{Name = nameChoice, Year = year}; 
            studentSvc.Add(myStudent); 
            PrintStudent(myStudent);
        }

        public void PrintStudentList()
        {
            int studentCount = 0;
            StudentService.Current.Students.ToList().ForEach(
                s => Console.WriteLine($"{++studentCount}, {s.Name} - {s.Year}")
            ); 
        }

        public void PrintStudent(Student student)
        {
            Console.WriteLine(student);
       
        }

        public void Enroll()    
        {
            Console.WriteLine("Select Student to Enroll:");

            PrintStudentList();
            var studentChoice = Console.ReadLine();
            
            Student studentToEnroll = null;

            if(int.TryParse(studentChoice, out int studentIntChoice)) 
            {
                studentToEnroll = StudentService.Current.Students.ElementAt(studentIntChoice - 1);

                if (studentToEnroll.Schedule == null){
                    studentToEnroll.Schedule = new List<Course>();
                }
            } else {
                Console.WriteLine("Error: Unable to Enroll Student.");
            }   

            Console.WriteLine($"Select Course to Enroll Student in:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();

            if(int.TryParse(courseChoice, out int intChoice)) 
            {
                var courseToEnroll = CourseService.Current.Courses.ElementAt(intChoice - 1);

                if (courseToEnroll.Roster == null){
                    courseToEnroll.Roster = new List<Student>();
                }
                courseToEnroll.Roster.Add(studentToEnroll);
                studentToEnroll.Schedule.Add(courseToEnroll);
                Console.WriteLine($"{studentToEnroll.Name} successfully Enrolled in {courseToEnroll.Name}");
                
            } else {
                Console.WriteLine("Error: Unable to Enroll Student.");
            }   
        }

        public void RemoveStudent()
        {
            Console.WriteLine("Select Student to Remove from Course:");
             PrintStudentList();
            var studentChoice = Console.ReadLine();
            
            Student studentToRemove = null;

            if(int.TryParse(studentChoice, out int studentIntChoice)) 
            {
                studentToRemove = StudentService.Current.Students.ElementAt(studentIntChoice - 1);
            } else {
                Console.WriteLine("Error: Unable to find Student.");
            }   

            Console.WriteLine($"Select Course to Remove Student from:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();

            if(int.TryParse(courseChoice, out int intChoice)) 
            {
                var courseToRemove = CourseService.Current.Courses.ElementAt(intChoice - 1);

                if (courseToRemove.Roster == null){
                    courseToRemove.Roster = new List<Student>();
                }
                courseToRemove.Roster.Remove(studentToRemove);
                Console.WriteLine($"{studentToRemove.Name} successfully Removed from {courseToRemove}");
                
            } else {
                Console.WriteLine("Error: Unable to Remove Student.");
            }   
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            StudentService.Search(query).ToList().ForEach(Console.WriteLine);
        }

        public void PrintStudentInfo()
        {   
            Console.WriteLine("Select Student:");
            PrintStudentList();
            var studentChoice = Console.ReadLine();
            Student student = null;

            if(int.TryParse(studentChoice, out int studentIntChoice)) 
            {
                student = StudentService.Current.Students.ElementAt(studentIntChoice - 1);
            } else {
                Console.WriteLine("Error: Unable to find Student.");
            }   

            PrintStudent(student);            
        }

        public void DeleteStudent() 
        {
            Console.WriteLine("Select Student to Delete");
            PrintStudentList();
            
            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice)) 
            {
                var studentToDelete = StudentService.Current.Students.ElementAt(intChoice - 1);
                
                studentSvc.Delete(studentToDelete);
            } else {
                Console.WriteLine("Error: Unable to Delete Student.");
            }   
        }

        public void UpdateStudent() 
        {
            Console.WriteLine("Choose Student to Update:");
            PrintStudentList();
            
            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice)) 
            {
                var studentToUpdate = StudentService.Current.Students.ElementAt(intChoice - 1);

                Console.WriteLine("Updated Student Name:");
                studentToUpdate.Name = Console.ReadLine();

                Console.WriteLine("Updated Student Year:");
                Console.WriteLine("1: Freshman");
                Console.WriteLine("2: Sophomore");
                Console.WriteLine("3: Junior");
                Console.WriteLine("4: Senior");
                Console.WriteLine("5: Graduate Student");

                string? yearChoice = Console.ReadLine();
                var year = "Freshman";

                switch(yearChoice) 
                {
                    case "1": 
                        year = "Freshman";
                    break;

                    case "2": 
                        year = "Sophomore";
                    break;

                    case "3": 
                        year = "Junior";
                    break;

                    case "4": 
                        year = "Senior";
                    break;

                    case "5": 
                        year = "Graduate Student";
                    break;
                }

                studentToUpdate.Year = year;

                Console.WriteLine("Student successfully Updated!");
                PrintStudent(studentToUpdate);
                } 
            
            else 
            {
                Console.WriteLine("Error, unable to Update Student");
            }

        }

        public void PrintCourseList()
        {
         int courseCount = 0;
            foreach (var course in CourseService.Current.Courses)
            {
                Console.WriteLine($"{++courseCount}, {course.Name}");
            }
        }
    }
}