using Canvas.Models;

namespace Canvas.Services {

    public class AssignmentService {
        private static AssignmentService instance;
        public static AssignmentService Current {
            get {
                if (instance == null) {
                    instance = new AssignmentService();
                }
                return instance;
            }
        }

        private IList<Assignment> assignments;

        public IEnumerable<Assignment> Assignments
        {
            get
            {
                return assignments;
            }
        }

        private AssignmentService() {
            assignments = new List<Assignment>();
        }

        public IEnumerable<Assignment> GetByCourse(Guid courseId) {
            return assignments.Where(a => a.CourseId == courseId);
        }

        public void Add(Assignment assignment)
        {
            assignments.Add(assignment);
        }
    }
}