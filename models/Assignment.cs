using System.Text;

namespace Canvas.Models {
    public class Assignment {
        public string? Name {get; set;}
        public string? Description {get; set;}
        public double? AvailablePoints {get; set;}
        public DateTime DueDate {get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid StudentId {get; set;}
        public Guid ContentItemId {get; set;}
        public Guid ModuleId {get; set;}

        public Assignment()
        {
            Id = Guid.NewGuid(); 
        }
        public override string ToString()
        {   
            return 
                $"{Name} - Total Available Points: {AvailablePoints} - Due Date: {DueDate.ToString()}\n" +
                $"{Description}\n";
        }
    }
}