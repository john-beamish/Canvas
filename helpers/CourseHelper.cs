using Canvas.Models;
using Canvas.Services;

namespace Canvas.helpers {
    public class CourseHelper {
        private CourseService courseSvc = CourseService.Current;
        
        public void NewCourse() 
        {
            Console.WriteLine("Course Name:");
            var name = Console.ReadLine();

            Console.WriteLine("Course Code:");
            var code = Console.ReadLine();

            Console.WriteLine("Course Credits:");
            var credits = Console.ReadLine();

            Console.WriteLine("Course Description:");
            var description = Console.ReadLine();

            Course myCourse;
            if(double.TryParse(credits, out double creditsDbl)) {   // credits = cLimit in example, Credits = CreditLimit, creditsDbl = cLimitDbl
                myCourse = new Course{Name = name, Code = code, Credits = creditsDbl, Description = description}; 
            } else {
                myCourse = new Course{Name = name, Code = code, Description = description}; 
            }

            Console.WriteLine("Course created successfully!");
            courseSvc.Add(myCourse);
            PrintCourse(myCourse);
        }

        public void UpdateCourse() 
        {
            Console.WriteLine("Choose Course to Update:");
            PrintCourseList();
            
            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice)) {
                var courseToUpdate = CourseService.Current.Courses.ElementAt(intChoice - 1);
                
                Console.WriteLine("Updated Course Name:");
                courseToUpdate.Name = Console.ReadLine();

                Console.WriteLine("Updated Course Code:");
                courseToUpdate.Code = Console.ReadLine();

                string creditsLoop = "on";
                var newCredits = "notADouble";

                while (creditsLoop == "on") 
                {
                    if(double.TryParse(newCredits, out double newCreditsDbl)) {
                        courseToUpdate.Credits = newCreditsDbl;
                        creditsLoop = "off"; 
                    } else {
                        Console.WriteLine("Updated Course Credits:");
                        newCredits = Console.ReadLine();
                    }
                }

                Console.WriteLine("Updated Course Description:");
                courseToUpdate.Description = Console.ReadLine();
                
                Console.WriteLine("Course successfully Updated!");
                PrintCourse(courseToUpdate);
            }
        }

        public void DeleteCourse() 
        {
            PrintCourseList();
            
            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice)) 
            {
                var courseToDelete = CourseService.Current.Courses.ElementAt(intChoice - 1);
                
                courseSvc.Delete(courseToDelete);
            }
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
            Console.WriteLine("Select category to Search:");
            Console.WriteLine("1, Course Name");
            Console.WriteLine("2, Course Code");
            Console.WriteLine("3, Course Description");

            var choice = Console.ReadLine();

            if(int.TryParse(choice, out int intChoice)) 
            {
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
            
            } else
            {
                Console.WriteLine("Error, unable to Search Courses");
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
            Console.WriteLine("Select Course:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();
            Course course = null;

            if(int.TryParse(courseChoice, out int courseIntChoice)) 
            {
                course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
            } else {
                Console.WriteLine("Error: Unable to find Course.");
            }   

            PrintCourse(course);         
        }
    }
}