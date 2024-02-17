using Canvas.Models;

namespace Canvas.Services {
    public class StudentService {

        private CourseService courseSvc = CourseService.Current;
        private SubmissionService submissionSvc = SubmissionService.Current;
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
            students = new List<Student>()
            {
                new Student { Name = "TestStudent 1"},
                new Student { Name = "TestStudent 2" },
                new Student { Name = "TestStudent 3" },
                new Student { Name = "TestStudent 4" },
                new Student { Name = "TestStudent 5" },
            };
        }

        public static IEnumerable<Student> Search(string query) 
        {
            return students.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

        public void Add(Student student)
        {
            students.Add(student);
        }

        public void Delete(Student student)
        {
            if (student.Schedule != null)
            {
                foreach (Course course in student.Schedule.ToList())
                {
                    if (course.Roster != null)
                    {
                        foreach (Student myStudent in course.Roster.ToList())
                        {
                            if (myStudent.Id == student.Id){
                                courseSvc.DeleteStudent(course, student);
                            }
                        }
                    }
                }
            }
            
            if (student.Submissions != null)
            {
                foreach (Submission submission in student.Submissions)
                {
                    if (submission.StudentId == student.Id)
                    {
                        submissionSvc.Delete(submission);
                    }
                }
            }
            
            students.Remove(student);
        }
        
        public void DeleteSubmission(Assignment assignment)
        {
            foreach (Student student in students.ToList())
            {
                if (student.Submissions != null)
                {
                    foreach (Submission submission in student.Submissions.ToList())
                    {   
                        if (submission.AssignmentId == assignment.Id) 
                        {
                            student.Submissions?.Remove(submission);
                        }
                    }
                }
            }
        }

        public void DeleteCourse(Course course)
        {
            foreach (Student student in students.ToList())
            {
                if (student.Schedule != null)
                {
                    foreach (Course myCourse in student.Schedule.ToList())
                    {
                        if (myCourse.Id == course.Id)
                        {
                            student.Schedule?.Remove(course);
                        }
                    }  
                }  
            }
        }
    }
}