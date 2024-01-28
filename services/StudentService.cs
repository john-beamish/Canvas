using Canvas.Models;

namespace Canvas.Services {
    public class StudentService {

        private static List<Student> students = new List<Student>();
        private string? query;
        private static object _lock = new object();
        private static StudentService instance;
        public static StudentService Current {
            get {
                lock(_lock) {
                    if (instance == null) {
                        instance = new StudentService();
                    }
                }

                return instance;
            }
        }

        public IEnumerable<Student> Students {
            get {
                return students;
            }
        }
        private StudentService() {
            students = new List<Student>();
        }

        public static IEnumerable<Student> Search(string query) 
        {
            return students.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

        public IEnumerable<Student> GetByCourse(Guid courseId) {
            return students.Where(s => s.CourseId == courseId);
        }

        public void Add(Student student)
        {
            students.Add(student);
        }

        public void Delete(Student studentToDelete)
        {
            students.Remove(studentToDelete);
        }
    }
}