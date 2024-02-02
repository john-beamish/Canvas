using System.Collections.Concurrent;
using Canvas.Models;
using Canvas.Services;

namespace Canvas.helpers {
    public class AssignmentHelper {
        
        private AssignmentService assignmentSvc = AssignmentService.Current;
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;
        private SubmissionService submissionSvc = SubmissionService.Current;

        public void NewAssignment() 
        {
            Console.WriteLine("Assignment Name:");
            var name = Console.ReadLine();

            var course = GetCourse();

            if (course == null)
            {
                return;
            }

            Console.WriteLine("Assignment Description:");
            var description = Console.ReadLine();

            Console.WriteLine("Total available points:");
            var availablePoints = Console.ReadLine();

            if (double.TryParse(availablePoints, out double availablePointsDbl) == false)
            {
                Console.WriteLine("Error: Please enter a valid number of available points.\n");
                return;
            } 

            Console.WriteLine("Due Date (MM-DD-YYYY):");
            var dueDate = Console.ReadLine();

            if(DateTime.TryParse(dueDate, out DateTime parsedDueDate) == false)
            {
                Console.WriteLine("Error: Please enter a valid Due Date (MM-DD-YYYY)\n");
                return;
            }
            
            Assignment assignment = new Assignment
                {Name = name, Description = description, AvailablePoints = availablePointsDbl, DueDate = parsedDueDate};

            if (course.Assignments == null)
            {
                course.Assignments = new List<Assignment>();
            }
                            
            assignment.Course = course;
            assignment.CourseId = course.Id;
            assignmentSvc.Add(assignment);
            courseSvc.AddAssignment(course, assignment);

            Console.WriteLine("Assignment created successfully!\n");
            PrintAssignment(assignment);    
        }

        public void DeleteAssignment() 
        {
            Assignment assignment = getAssignment();

            if (assignment == null)
            {
                return;
            }
            
            assignmentSvc.Delete(assignment);
            Console.WriteLine($"{assignment.Name} Deleted.\n");
        }

        public void PrintAssignment(Assignment assignment)
        {
            Console.WriteLine(assignment);
        }

        public void PrintAssignmentList(Course course)
        {
            int assignmentCount = 0;
            course.Assignments?.ToList().ForEach(
                a => Console.WriteLine($"{++assignmentCount}, {a.Name}")
            );
        }

        public void PrintCourseList()
        {
         int courseCount = 0;
            foreach (var course in CourseService.Current.Courses)
            {
                Console.WriteLine($"{++courseCount}, {course.Name}");
            }
        }

        public void PrintAssignmentInfo()
        {
            Assignment assignment = getAssignment();
            PrintAssignment(assignment);                     
        }

        public Course GetCourse()
        {
            if (courseSvc.Courses.Count() <= 0) 
            {
                Console.WriteLine("Error, please create a Course.\n");
                return null;
            }

            Console.WriteLine("Select Course:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();
            
            if(int.TryParse(courseChoice, out int courseChoiceInt) == false) 
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }
                
            if (courseChoiceInt <= 0 || courseChoiceInt > courseSvc.Courses.Count())
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }
                    
            Course course = CourseService.Current.Courses.ElementAt(courseChoiceInt - 1);

            return course;
        }

        public Assignment getAssignment()
        {
            if (assignmentSvc.Assignments.Count() <= 0)
            {
                Console.WriteLine("Error: Please create an Assignment.\n");
                return null;
            }
                
            var course = GetCourse();
            if (course == null)
            {
                return null;
            }
                    
            Console.WriteLine("Select Assignment:");
            PrintAssignmentList(course);
            var assignmentChoice = Console.ReadLine();

            if (int.TryParse(assignmentChoice, out int assignmentIntChoice) == false)
            {
                Console.WriteLine("Error: Please select a valid Assignment.\n");
                return null;
            }
            
            if (assignmentIntChoice <= 0 || assignmentIntChoice > course.Assignments?.Count())
            {
                Console.WriteLine("Error: Please select a valid Assignment.\n");
                return null;
            }
            
            Assignment assignment = course.Assignments.ElementAt(assignmentIntChoice - 1);
            return assignment;
        }
    }
}