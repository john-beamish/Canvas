using System.Diagnostics;
using System.Net.WebSockets;
using Canvas.Models;

namespace Canvas.Services {
    public class CourseService {
        private static List<Course> courses = new List<Course>();
        private string? query;
        private static object _lock = new object();
        private static CourseService instance;

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

        public void Delete(Course courseToDelete)
        {
            courses.Remove(courseToDelete);
        }
    }
}