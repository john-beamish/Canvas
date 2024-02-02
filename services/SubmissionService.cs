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

        public static IEnumerable<Submission> SearchSubmissionAssignment(string query) 
        {
            return submissions.Where(s => s.Assignment.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchSubmissionStudent(string query) 
        {
            return submissions.Where(s => s.Student.Name.ToUpper().Contains(query.ToUpper()));
        }

        public static IEnumerable<Submission> SearchSubmissionDescription(string query) 
        {
            return submissions.Where(s => s.Description.ToUpper().Contains(query.ToUpper()));
        }
        
        public void Add(Submission submission) 
        {     
            submissions.Add(submission);
        }

        public void Delete(Submission submission)
        {
            submissions.Remove(submission);
        }
    }
}