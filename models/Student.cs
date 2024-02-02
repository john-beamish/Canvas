using System.Text;
using Canvas.Services;

namespace Canvas.Models {
    public class Student {
        public string? Name {get; set;}
        public string? Year {get; set;}
        public List<Course>? Schedule{get; set;}
        public List<Submission>? Submissions{get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid SubmissionId {get; set;}
        // public Guid ContentItemId {get; set;}
        // public Guid ModuleId {get; set;}

        public Student()
        {
            Id = Guid.NewGuid(); // Assign a new Guid as the Id
        }
        public override string ToString()
        {   
            StringBuilder scheduleString = new StringBuilder();
            StringBuilder submissionString = new StringBuilder();

            if (Schedule != null && Submissions == null)
            {
                foreach (Course course in Schedule)
                {
                    scheduleString.AppendLine($"    ({course.Code}) - {course.Name} - {course.Credits} Credits\n");
                }
                
                return 
                $"{Name} - {Year}\n" +
                $"Schedule:\n" +
                $"{scheduleString}";

            }

            if (Schedule == null && Submissions != null)
            {
                foreach (Submission submission in Submissions)
                {
                    submissionString.AppendLine($"    {submission.Name} - Course: {submission.Assignment?.Course?.Name} - Assignment: {submission.Assignment?.Name} - Student: {submission.Student?.Name} - Grade: {submission.Grade} / {submission.Assignment?.AvailablePoints}\n");
                }
                
                return 
                    $"{Name} - {Year}\n" +
                    $"Submissions:\n" +
                    $"{submissionString}";
            } 

            if (Schedule != null && Submissions != null)
            {
                foreach (Course course in Schedule)
                {
                    scheduleString.AppendLine($"    ({course.Code}) - {course.Name} - {course.Credits} Credits\n");
                }
                foreach (Submission submission in Submissions)
                {
                    submissionString.AppendLine($"    {submission.Name} - Assignment: {submission.Assignment?.Name} - Student: {submission.Student?.Name} - Grade: {submission.Grade} / {submission.Assignment?.AvailablePoints}\n");
                }

                return 
                $"{Name} - {Year}\n" +
                $"Schedule:\n" +
                $"{scheduleString}" +
                $"Submissions:\n" +
                $"{submissionString}";
            } 
            
            else {
                return 
                $"{Name} - {Year}\n";
            }
        }
    }
}