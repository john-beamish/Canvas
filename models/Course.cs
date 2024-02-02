using System.Text;
using Canvas.Services;

namespace Canvas.Models{
    public class Course {
        
        public string? Name{get; set;}
        public string? Code{get; set;}      // string? means it could be a string or it could be nothing (null)
        public double? Credits{get; set;}
        public string? Description{get; set;}
        public List<Student>? Roster{get; set;}
        public List<Assignment>? Assignments{get; set;}
        // public List<string>? Modules{get; set;}

        public Guid Id {get; set;}
        public Guid StudentId {get; set;}
        public Guid AssignmentId {get; set;}
        // public Guid ModuleId {get; set;}
        // public Guid ContentItemId {get; set;}

        public Course()
        {
            Id = Guid.NewGuid(); // Assign a new Guid as the Id
        }

        public override string ToString()
        {   
            StringBuilder rosterString = new StringBuilder();
            StringBuilder assignmentString = new StringBuilder();

            if (Roster != null && Assignments != null)
            {
                foreach (Assignment assignment in AssignmentService.Current.Assignments.Where(a => a.CourseId == Id))
                {
                    assignmentString.AppendLine($"    {assignment?.Name} - Course: {assignment?.Course?.Name ?? string.Empty} - Total Available Points: {assignment?.AvailablePoints} - Due Date: {assignment?.DueDate.ToString()}");
                }

                foreach (Student student in Roster)
                {
                    rosterString.AppendLine($"    {student.Name} - {student.Year}");
                }

                return 
                $"({Code}) - {Name} - {Credits} Credits\n" +
                $"Course Description: {Description}\n" +
                $"Roster:\n" +
                $"{rosterString}\n" +
                $"Assignments:\n" +
                $"{assignmentString}";
            }
            if (Roster != null && Assignments == null)
            {
                foreach (Student student in Roster)
                {
                    rosterString.AppendLine($"  {student.Name} - {student.Year}");
                }

                return 
                $"({Code}) - {Name} - {Credits} Credits\n" +
                $"Course Description: {Description}\n" +
                $"Roster:\n" +
                $"{rosterString}";
            
            } 
            if (Roster == null && Assignments != null)
            {
                foreach (Assignment assignment in AssignmentService.Current.Assignments.Where(a => a.CourseId == Id))
                {
                    assignmentString.AppendLine($"  {assignment.Name} - Course: {assignment?.Course?.Name ?? string.Empty} - Total Available Points: {assignment?.AvailablePoints} - Due Date: {assignment?.DueDate.ToString()}");
                }

                return 
                $"({Code}) - {Name} - {Credits} Credits\n" +
                $"Course Description: {Description}\n" +
                $"Assignments:\n" +
                $"{assignmentString}";
            } 
            else
            {
                return 
                    $"({Code}) - {Name} - {Credits} Credits\n" +
                    $"Course Description: {Description}";
            }
        }
    }
}