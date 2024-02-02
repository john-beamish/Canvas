using System.Text;
using Canvas.Services;

namespace Canvas.Models {
    public class Assignment {
        public string? Name {get; set;}
        public string? Description {get; set;}
        public DateTime? DueDate {get; set;}
        public double? AvailablePoints {get; set;}
        public Course? Course {get; set;}
        public List<Submission>? Submissions {get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid SubmissionId {get; set;}
        // public Guid ContentItemId {get; set;}
        // public Guid ModuleId {get; set;}

        public Assignment()
        {
            Id = Guid.NewGuid(); 
        }
        public override string ToString()
        {   
            StringBuilder submissionString = new StringBuilder();

            if (Submissions != null)
            {
                foreach (Submission submission in SubmissionService.Current.Submissions.Where(s => s.AssignmentId == Id))
                {
                    submissionString.AppendLine($"    {submission?.Name} - Student: {submission?.Student?.Name} - Grade: {submission?.Grade} / {submission?.Assignment?.AvailablePoints}");
                }

                return 
                    $"{Name} - Course: {Course?.Name} - Total Available Points: {AvailablePoints} - Due Date: {DueDate.ToString()}\n" +
                    $"Description: {Description}\n" +
                    $"Submissions:\n" +
                    $"{submissionString}\n";
            }
            else {
                return 
                    $"{Name} - Course: {Course?.Name} - Total Available Points: {AvailablePoints} - Due Date: {DueDate.ToString()}\n" +
                    $"Description: {Description}\n";
            }
        }
    }
}