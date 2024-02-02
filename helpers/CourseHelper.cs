using Canvas.Models;
using Canvas.Services;

namespace Canvas.helpers {
    public class CourseHelper {
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;
        private AssignmentService assignmentSvc = AssignmentService.Current;

        public void NewCourse() 
        {
            Console.WriteLine("Course Name:");
            var name = Console.ReadLine();

            Console.WriteLine("Course Code:");
            var code = Console.ReadLine();

            Console.WriteLine("Course Credits:");
            var credits = Console.ReadLine();

            if(double.TryParse(credits, out double creditsDbl) == false)
            {
                Console.WriteLine("Error: Please enter a number for Course Credits.\n");
                return;
            }

            Console.WriteLine("Course Description:");
            var description = Console.ReadLine();

            Course course = new Course{Name = name, Code = code, Credits = creditsDbl, Description = description};
            courseSvc.Add(course);

            Console.WriteLine("Course created successfully!\n");
            PrintCourse(course);
            Console.WriteLine();
        }

        public void UpdateCourse() 
        {
            Course course = SelectCourse();
            if (course == null)
            {
                return;
            }

            Console.WriteLine("Updated Course Name:");
            var name = Console.ReadLine();

            Console.WriteLine("Updated Course Code:");
            var code = Console.ReadLine();

            Console.WriteLine("Updated Course Credits:");
            var credits = Console.ReadLine();
            
            if (double.TryParse (credits, out double creditsDbl) == false)
            {
                Console.WriteLine("Error: Please enter a number for Course Credits.\n");
                return;
            }

            Console.WriteLine("Updated Course Description:");
            var description = Console.ReadLine();

            course.Name = name;
            course.Code = code;
            course.Credits = creditsDbl;
            course.Description = description;
                        
            Console.WriteLine("Course Updated successfully!\n");
            PrintCourse(course);
            Console.WriteLine();      
        }

        public void DeleteCourse() // Removes Course from Schedule of Students enrolled in the course, & Deletes assignments associated with the course (assignments will delete their submissions themselves)
        {
            Course course = SelectCourse();
            
            if (course == null)
            {
                return;
            }
                
            studentSvc.DeleteCourse(course);
                
            foreach (Assignment assignment in assignmentSvc.Assignments.ToList())
            {
                if (assignment.CourseId == course.Id)
                {
                    assignmentSvc.Delete(assignment);
                }
            }

            courseSvc.Delete(course);
            Console.WriteLine("Course Deleted successfully!\n");
        }

        public void PrintCourseList()
        {
            int courseCount = 0;
            CourseService.Current.Courses.ToList().ForEach(
                c => Console.WriteLine($"{++courseCount}, {c.Code} - {c.Name} - {c.Credits} Credits")
            ); 
        }

        public void SearchCourses() 
        {
            if (courseSvc.Courses.Count() <= 0) 
            {
                Console.WriteLine("Error, please create a Course before Searching.\n");
                return;
            }
            
            Console.WriteLine("Select category to Search:");
            Console.WriteLine("1, Course Name");
            Console.WriteLine("2, Course Code");
            Console.WriteLine("3, Course Description");

            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice) == false) 
            {
                Console.WriteLine("Error, please select a valid parameter to Search.\n");
                return;
            }

            int parameterCount = 3;

            if (intChoice <= 0 || intChoice > parameterCount)
            {
                Console.WriteLine("Error, please select a valid parameter to Search.\n");
                return;
            }
                        
            if(intChoice == 1)
            {
                SearchCourseName();
            }
            if(intChoice == 2)
            {
                SearchCourseCode();
            }
            if(intChoice == 3)
            {
                SearchCourseDescription();
            }
        }

        public void SearchCourseName() 
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            CourseService.SearchCourseName(query).ToList().ForEach(Console.WriteLine);
        }

        public void SearchCourseCode() 
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            CourseService.SearchCourseCode(query).ToList().ForEach(Console.WriteLine);
        }

        public void SearchCourseDescription() 
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            CourseService.SearchCourseDescription(query).ToList().ForEach(Console.WriteLine);
        }

        public void PrintCourse(Course course)
        {
            Console.WriteLine(course);
        }

        public void PrintCourseInfo()
        {
            Course course = SelectCourse();

            if (course == null)
            {
                return;
            }
            
            PrintCourse(course); 
        }

        public Course SelectCourse()
        {
            if (courseSvc.Courses.Count() <= 0) 
            {
                Console.WriteLine("Error, please create a Course.\n");
                return null;
            }
                
            Console.WriteLine("Select Course:");
            PrintCourseList();
                
            var courseChoice = Console.ReadLine();
            Course course;

            if(int.TryParse(courseChoice, out int courseIntChoice) == false) 
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }
                
            if (courseIntChoice <= 0 || courseIntChoice > courseSvc.Courses.Count())
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }

            course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
            return course;
        }
    }
}