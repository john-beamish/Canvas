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

            Console.WriteLine("Course created successfully!\n");
            courseSvc.Add(myCourse);
            PrintCourse(myCourse);
            Console.WriteLine();
        }

        public void UpdateCourse() 
        {
            Course course = SelectCourse();
            if (course != null)
            {
                Console.WriteLine("Updated Course Name:");
                course.Name = Console.ReadLine();

                Console.WriteLine("Updated Course Code:");
                course.Code = Console.ReadLine();

                string creditsLoop = "on";
                var newCredits = "notADouble";

                while (creditsLoop == "on") 
                {
                    if(double.TryParse(newCredits, out double newCreditsDbl)) {
                        course.Credits = newCreditsDbl;
                        creditsLoop = "off"; 
                    } 
                    else {
                        Console.WriteLine("Updated Course Credits:");
                        newCredits = Console.ReadLine();
                    }
                }

                Console.WriteLine("Updated Course Description:");
                course.Description = Console.ReadLine();
                        
                Console.WriteLine("Course Updated successfully!\n");
                 PrintCourse(course);
                Console.WriteLine();      
            } 
        }

        public void DeleteCourse() 
        {
            Course course = SelectCourse();
            if (course != null)
            {
                courseSvc.Delete(course);
                Console.WriteLine("Course Deleted successfully!\n");
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
            if (courseSvc.Courses.Count() > 0) 
            {
                Console.WriteLine("Select category to Search:");
                Console.WriteLine("1, Course Name");
                Console.WriteLine("2, Course Code");
                Console.WriteLine("3, Course Description");

                var choice = Console.ReadLine();

                if(int.TryParse(choice, out int intChoice)) 
                {
                    if (intChoice > 0 && intChoice <= 3)
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
                    }
                    else {
                        Console.WriteLine("Error, please select a valid category to Search.\n");
                    }
                } else {
                    Console.WriteLine("Error, please select a valid category to Search.\n");
                }
            }
            else {
                Console.WriteLine("Error, please create a Course before Searching.\n");
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
            if (courseSvc.Courses.Count() > 0) 
            {
                Console.WriteLine("Select Course:");
                PrintCourseList();
                var choice = Console.ReadLine();
                Course course = null;

                if(int.TryParse(choice, out int intChoice)) 
                {
                    if (intChoice > 0 && intChoice <= courseSvc.Courses.Count())
                    {
                        course = CourseService.Current.Courses.ElementAt(intChoice - 1);
                        PrintCourse(course); 
                    } 
                    else {
                    Console.WriteLine("Error, please select a valid Course.\n");
                    }  
                } 
                else {
                    Console.WriteLine("Error, please select a valid Course.\n");
                }      
            } 
            else {
                Console.WriteLine("Error, please create a Course.\n");
            }      
        }

        public Course SelectCourse()
        {
            if (courseSvc.Courses.Count() > 0) 
            {
                Console.WriteLine("Select Course:");
                PrintCourseList();
                
                var courseChoice = Console.ReadLine();
                Course course;

                if(int.TryParse(courseChoice, out int courseIntChoice)) 
                {
                    if (courseIntChoice > 0 && courseIntChoice <= courseSvc.Courses.Count())
                    {
                        course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
                        return course;
                    }
                    else {
                        Console.WriteLine("Error, please select a valid Course.\n");
                        return null;
                    }
                }
                else {
                    Console.WriteLine("Error, please select a valid Course.\n");
                    return null;
                    }
            }
            else {
                Console.WriteLine("Error, please create a Course.\n");
                return null;
            }
        }
    }
}