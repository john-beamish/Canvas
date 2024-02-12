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
            
            Student student = new Student{Name = nameChoice}; 
            studentSvc.Add(student); 
            
            student.Year = GetStudentYear();

            if (student.Year != null)
            {
                Console.WriteLine("Student created successfully!\n");
                PrintStudent(student);
            }
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
            Student student = SelectStudent();

            if (student == null)
            {
                return;
            }

            Course course = SelectCourse();

            if (course == null)
            {
                return;
            }
                   
            if (student?.Schedule?.Count > 0) 
            {
                foreach (Course enrolledCourse in student.Schedule.ToList())
                {
                    if (enrolledCourse.Id == course.Id) 
                    {
                        Console.WriteLine($"Error: {student.Name} is already Enrolled in {enrolledCourse.Name}.\n");
                        return;
                    }
                }
            }      
                      
            if (course.Roster == null){
                course.Roster = new List<Student>();
            }
                                        
            course.Roster.Add(student);

            if (student?.Schedule == null){
                student.Schedule = new List<Course>();
            }
                            
            student?.Schedule?.Add(course);
            Console.WriteLine($"{student?.Name} successfully Enrolled in {course.Name}.\n");
        }

        public void RemoveStudent()
        {
            Student student = SelectStudent();

            if (student == null)
            {
                return;
            }
            
            Course course = SelectStudentCourse(student);

            if (course == null)
            {
                return;
            }
                    
            course.Roster?.Remove(student);
            student.Schedule?.Remove(course);
            
            Console.WriteLine($"{student.Name} successfully Removed from {course.Name}\n");
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            StudentService.Search(query).ToList().ForEach(Console.WriteLine);
        }

        public void PrintStudentInfo()
        {   
            Student student = SelectStudent();

            if (student == null)
            {
                return;
            }
                
            PrintStudent(student);
        }

        public void DeleteStudent() 
        {
            Student student = SelectStudent();

            if (student == null)
            {
                return;
            }
                
            studentSvc.Delete(student);
            Console.WriteLine("Student successfully Deleted.\n");
        }

        public void UpdateStudent() 
        {
            Student student = SelectStudent();

            if (student == null)
            {
                Console.WriteLine("Student not updated. Please try again.\n");
                return;
            }
                
            Console.WriteLine("Updated Student Name:");
            var name = Console.ReadLine();

            var year = GetStudentYear();

            if (year == null) 
            {
                Console.WriteLine("Student not updated. Please try again.\n");
                return;
            }
            
            student.Name = name;
            student.Year = year;

            Console.WriteLine("Student successfully Updated!\n");
            PrintStudent(student);
        }
        
        public void PrintCourseList()
        {
         int courseCount = 0;
            foreach (var course in CourseService.Current.Courses)
            {
                Console.WriteLine($"{++courseCount}, {course.Name}");
            }
        }

        public void PrintAssignmentList()
        {
         int assignmentCount = 0;
            foreach (var assignment in AssignmentService.Current.Assignments)
            {
                Console.WriteLine($"{++assignmentCount}, {assignment.Name}");
            }
        }

        public void PrintStudentCourseList(Student student)
        {
            int courseCount = 0;
            foreach (Course course in student.Schedule)
            {
            Console.WriteLine($"{++courseCount}, {course.Name}");
            }
        }

        public Student SelectStudent()
        {
            if (studentSvc.Students.Count() <= 0) 
            {
                Console.WriteLine("Error, please create a Student.\n");
                return null;
            }
                
            Console.WriteLine("Select Student:");
            PrintStudentList();
            var studentChoice = Console.ReadLine();
                
            if (int.TryParse(studentChoice, out int studentIntChoice) == false) 
            {
                Console.WriteLine("Error: Please select a valid Student.\n");
                return null;
            }
            
            if (studentIntChoice <= 0 || studentIntChoice > studentSvc.Students.Count())
            {
                Console.WriteLine("Error: Please select a valid Student.\n");
                return null;
            }
                
            Student student = StudentService.Current.Students.ElementAt(studentIntChoice - 1);
            
            return student;  
        }

        public Course SelectCourse()
        {
            Console.WriteLine($"Select Course:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();

            if (courseSvc.Courses.Count() <= 0)
            {
                Console.WriteLine("Error, please create a Course.\n");
                return null;
            }
        
            if (int.TryParse(courseChoice, out int courseIntChoice) == false) 
            {
                Console.WriteLine("Error: Please select a valid Course.\n");
                return null;
            }
                    
            if (courseIntChoice <= 0 || courseIntChoice > courseSvc.Courses.Count())
            {
                Console.WriteLine("Error: Please select a valid Course.\n");
                return null;
            }
                        
            Course course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
                        
            return course;
        }

        public Course SelectStudentCourse(Student student)
        {
            Console.WriteLine($"Select Course:");
            PrintStudentCourseList(student);  // Only print Courses the Student is enrolled in. 
            var courseChoice = Console.ReadLine();

            if (student.Schedule?.Count() <= 0)
            {
                Console.WriteLine("Error, Student must be Enrolled in a Course.\n");
                return null;
            }

            if (int.TryParse(courseChoice, out int courseIntChoice) == false) 
            {
                Console.WriteLine("Error: Please select a valid Course.\n");
                return null;
            }
            
            if (courseIntChoice <= 0 || courseIntChoice > student.Schedule.Count())
            {
                Console.WriteLine("Error: Please select a valid Course.\n");
                return null;
            }
            
            Course course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
                        
            return course;
        }
        public string GetStudentYear()
        {
            Console.WriteLine("Student Year:");
            Console.WriteLine("1: Freshman");
            Console.WriteLine("2: Sophomore");
            Console.WriteLine("3: Junior");
            Console.WriteLine("4: Senior");
            Console.WriteLine("5: Graduate Student");

            string? yearChoice = Console.ReadLine();
                        
            if (int.TryParse(yearChoice, out int yearIntChoice) == false)
            {
                Console.WriteLine("Error, please select a valid Year.\n");
                return null;
            }

            int maxYear = 5;

            if (yearIntChoice <= 0 || yearIntChoice > maxYear) 
            {
                Console.WriteLine("Error, please select a valid Year.\n");
                return null;
            }

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
                    
            return year;
        }
    }   
}
