using System.Collections.Concurrent;
using Canvas.Models;
using Canvas.Services;

namespace Canvas.helpers {
    public class AssignmentHelper {
        
        private AssignmentService assignmentSvc = AssignmentService.Current;
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;

        public void NewAssignment() 
        {
            Console.WriteLine("Assignment Name:");
            var name = Console.ReadLine();

            Console.WriteLine($"Assignment Course:");
            PrintCourseList();
            var courseChoice = Console.ReadLine();

            Console.WriteLine("Assignment Description:");
            var description = Console.ReadLine();

            Console.WriteLine("Total available points:");
            var availablePoints = Console.ReadLine();

            Console.WriteLine("Due Date (MM-DD-YYYY):");
            var dueDate = Console.ReadLine();

            Assignment myAssignment = null;

            if (int.TryParse(courseChoice, out int intCourseChoice)) 
            {
                if (intCourseChoice > 0 && intCourseChoice <= courseSvc.Courses.Count())
                {
                    if (double.TryParse(availablePoints, out double availablePointsDbl)) 
                    {  
                        if(DateTime.TryParse(dueDate, out DateTime parsedDueDate))
                        {
                            myAssignment = new Assignment
                            {Name = name, Description = description, AvailablePoints = availablePointsDbl, DueDate = parsedDueDate};

                            var assignmentCourse = CourseService.Current.Courses.ElementAt(intCourseChoice - 1);

                            if (assignmentCourse.Assignments == null){
                                assignmentCourse.Assignments = new List<Assignment>();
                            }
                            
                            myAssignment.Course = assignmentCourse;
                            myAssignment.CourseId = assignmentCourse.Id;
                            assignmentSvc.Add(myAssignment);
                            courseSvc.AddAssignment(assignmentCourse, myAssignment);

                            Console.WriteLine("Assignment created successfully!\n");
                            
                            PrintAssignment(myAssignment);
                        }
                        else {
                            Console.WriteLine("Error: Please enter a valid Due Date (MM-DD-YYYY)\n");
                        }
                    }   
                    else {
                        Console.WriteLine("Error: Please enter a valid number of available points\n");
                    }
                }
                else {
                    Console.WriteLine("Error: Please select a valid Course.\n");
                }
            }
            else {
            Console.WriteLine("Error: Please select a valid Course.\n");
            }
        }

        public void DeleteAssignment() 
        {
            Assignment assignment = SelectAssignment();

            if (assignment != null)
            {
                Course course = assignment.Course;
                courseSvc.DeleteAssignment(course, assignment);
                assignmentSvc.Delete(assignment);
                Console.WriteLine($"{assignment.Name} Deleted.\n");
            }
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
            Assignment assignment = SelectAssignment();
            PrintAssignment(assignment);                     
        }

        public Assignment SelectAssignment()
        {
            if (courseSvc.Courses.Count() > 0) 
            {
                Console.WriteLine("Select Assignment Course:");
                PrintCourseList();
                var courseChoice = Console.ReadLine();
                Course course = null;

                if(int.TryParse(courseChoice, out int courseIntChoice)) 
                {
                    if (courseIntChoice > 0 && courseIntChoice <= courseSvc.Courses.Count())
                    {
                        course = CourseService.Current.Courses.ElementAt(courseIntChoice - 1);
                        
                        Console.WriteLine("Select Assignment:");
                        PrintAssignmentList(course);
                        var assignmentChoice = Console.ReadLine();
                        Assignment assignment = null;

                        if (int.TryParse(assignmentChoice, out int assignmentIntChoice))
                        {
                            if (assignmentIntChoice > 0 && assignmentIntChoice <= course.Assignments?.Count())
                            {
                                assignment = course.Assignments.ElementAt(assignmentIntChoice - 1);
                                return assignment;
                                }
                                else {
                                    Console.WriteLine("Error, please select a valid Assignment.\n");
                                    return null;
                                }
                            }
                            else {
                                Console.WriteLine("Error, please select a valid Assignment.\n");
                                return null;
                            }
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