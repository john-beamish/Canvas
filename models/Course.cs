
namespace Canvas.Models {
    public class Course {
        public string? Code{get; set;}      // string? means it could be a string or it could be nothing (null)
        public string? Name{get; set;}
        public string? Description{get; set;}
        public List<string>? Roster{get; set;}
        public List<string>? Assignments{get; set;}
        public List<string>? Modules{get; set;}
        public double? Credits{get; set;}
        public Course() {

        }
        public override string ToString()
        {
            return $"({Code}) {Name} - {Credits} Credits";
        }
    }
}