using Canvas.Models;

namespace Canvas.Services {

    public class AssignmentService {
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;
        private SubmissionService submissionSvc = SubmissionService.Current;
        
        private static List<Assignment> assignments = new List<Assignment>();
        private string? query;
        private static object _lock = new object();
        private static AssignmentService instance;
        public static AssignmentService Current {
            get {
                lock(_lock) {
                    if (instance == null) {
                        instance = new AssignmentService();
                    }
                }

                return instance;
            }
        }
        public IEnumerable<Assignment> Assignments
        {
            get {
                return assignments;
            }
        }

        private AssignmentService() {
            assignments = new List<Assignment>();
        }

        public static IEnumerable<Assignment> SearchCourse(string query) 
        {
            return assignments.Where(a => a.Course.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Assignment> SearchName(string query) 
        {
            return assignments.Where(a => a.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Assignment> SearchDescription(string query) 
        {
            return assignments.Where(a => a.Description.ToUpper().Contains(query) );
        }

        public static IEnumerable<Assignment> SearchDueDate(DateTime date) 
        {
            return assignments.Where(a => a.DueDate == date);
        }
        
         public void Add(Assignment assignment)
        {
            assignments.Add(assignment);
        }

        public void Delete(Assignment assignment)
        {
            submissionSvc.DeleteAssignment(assignment);
            courseSvc.DeleteAssignment(assignment.Course, assignment);
            studentSvc.DeleteSubmission(assignment);
            assignments.Remove(assignment);
        }
    }
}