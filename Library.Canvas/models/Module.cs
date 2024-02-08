namespace Canvas.Models {
    public class Module {
        public string? Name {get; set;}
        public string? Description{get; set;}
        public List<string>? Content{get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid StudentId {get; set;}
        public Guid AssignmentId {get; set;}
        public Guid ContentItemId {get; set;}

    }
}