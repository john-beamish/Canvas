using System.Text;

namespace Canvas.Models {
    public class Course {
        public Guid Id {get; set;}
        public Guid StudentId {get; set;}
        public Guid ModuleId {get; set;}
        public Guid AssignmentId {get; set;}
        public Guid ContentItemId {get; set;}

        public string? Code{get; set;}      // string? means it could be a string or it could be nothing (null)
        public string? Name{get; set;}
        public string? Description{get; set;}
        public List<Student>? Roster{get; set;}
        public List<string>? Assignments{get; set;}
        public List<string>? Modules{get; set;}
        public double? Credits{get; set;}
        public override string ToString()
        {   
            StringBuilder rosterString = new StringBuilder();

            if (Roster != null)
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
            
            } else 
            {   
                return 
                $"({Code}) - {Name} - {Credits} Credits\n" +
                $"Course Description: {Description}\n";
            }

            
        }
    }
}