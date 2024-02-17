using System.Diagnostics;
using System.Net.WebSockets;
using Canvas.Models;

namespace Canvas.Services {
    public class SubmissionService {
        private static List<Submission> submissions = new List<Submission>();
        private string? query;
        private static object _lock = new object();
        private static SubmissionService instance;

        public static SubmissionService Current {
            get {
                lock(_lock) {
                    if (instance == null) {
                        instance = new SubmissionService();
                    }
                }

                return instance;
            }
        }
        
        public IEnumerable<Submission> Submissions 
        {
            get {
                return submissions.Where( s => s.Name.ToUpper().Contains(query ?? string.Empty));
            }
        }
        
        private SubmissionService() 
        {
            submissions = new List<Submission>();
        }

        public static IEnumerable<Submission> SearchName(string query) 
        {
            return submissions.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchAssignment(string query) 
        {
            return submissions.Where(s => s.Assignment.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchStudent(string query) 
        {
            return submissions.Where(s => s.Student.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchCourse(string query) 
        {
            return submissions.Where(s => s.Assignment.Course.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchGrade(double query) 
        {
            return submissions.Where(s => s.Grade == query);
        }

        public static IEnumerable<Submission> SearchDescription(string query) 
        {
            return submissions.Where(s => s.Description.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchDueDate(DateTime query) 
        {
            return submissions.Where(s => s.Assignment.DueDate == query);
        }

        public static IEnumerable<Submission> SearchSubmissionDate(DateTime query) 
        {
            return submissions.Where(s => s.Date == query);
        }
        
        public void Add(Submission submission) 
        {     
            submissions.Add(submission);
        }

        public void Delete(Submission submission)
        {
            submissions.Remove(submission);
        }

        public void DeleteAssignment(Assignment assignment)
        {
            foreach (Submission submission in submissions.ToList())
            {
                if (submission.AssignmentId == assignment.Id) {
                    Delete(submission);
                }
            }
        }
    }
}