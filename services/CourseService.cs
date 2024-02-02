using System.Diagnostics;
using System.Net.WebSockets;
using Canvas.Models;

namespace Canvas.Services {
    public class CourseService {
        private static List<Course> courses = new List<Course>();
        private static List<Assignment> assignments = new List<Assignment>();
        private string? query;
        private static object _lock = new object();
        private static CourseService? instance;

        public static CourseService Current {
            get {
                lock(_lock) {
                    if (instance == null) {
                        instance = new CourseService();
                    }
                }

                return instance;
            }
        }
        
        public IEnumerable<Course> Courses 
        {
            get {
                return courses.Where(
                    c => 
                        c.Name.ToUpper().Contains(query ?? string.Empty)
                        || c.Code.ToUpper().Contains(query ?? string.Empty));
            }
        }

        public IEnumerable<Course> GetByAssignment(Guid assignmentId) {
            return courses.Where(c => c.AssignmentId == assignmentId);
        }

        public IEnumerable<Course> GetByStudent(Guid studentId) {
            return courses.Where(c => c.StudentId == studentId);
        }
        
        private CourseService() 
        {
            courses = new List<Course>();
        }

        public static IEnumerable<Course> SearchCourseName(string query) 
        {
            return courses.Where(c => c.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Course> SearchCourseCode(string query) 
        {
            return courses.Where(c => c.Code.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Course> SearchCourseDescription(string query) 
        {
            return courses.Where(c => c.Description.ToUpper().Contains(query.ToUpper()));
        }
        
        public void Add(Course course) 
        {     
            courses.Add(course);
        }

        public void AddAssignment(Course course, Assignment assignment) 
        {
            course.Assignments?.Add(assignment);
        }

        public void Delete(Course course)
        {
            courses.Remove(course);
        }

        public void DeleteAssignment(Course course, Assignment assignment)
        {
            course.Assignments?.Remove(assignment);
        }    

        public void DeleteStudent(Course course, Student student)  
        {
            course.Roster?.Remove(student);
        }  
    }
}