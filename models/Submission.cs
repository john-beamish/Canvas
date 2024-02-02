using System.Text;

namespace Canvas.Models {
    public class Submission {
        public string? Name {get; set;}
        public double? Grade {get; set;}
        public string? Description {get; set;}
        public DateTime? Date {get; set;}
        public Student? Student {get; set;}
        public Assignment? Assignment {get; set;}
        public Guid Id {get;}
        public Guid AssignmentId {get; set;}
        public Guid StudentId {get; set;}
        // public Guid ContentItemId {get; set;}
        // public Guid ModuleId {get; set;}

        public Submission()
        {
            Id = Guid.NewGuid(); 
        }
        public override string ToString()
        {   
            return 
                $"Submission: {Name} - Course: {Assignment?.Course?.Name} - Assignment: {Assignment?.Name} - Student: {Student?.Name}\n" +
                $"Grade: {Grade} / {Assignment?.AvailablePoints}\n" +
                $"Date Submitted: {Date} - (Due Date: {Assignment?.DueDate.ToString()})\n" +
                $"Description: {Description}\n";
        }
    }
}