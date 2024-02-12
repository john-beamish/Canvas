namespace Canvas.Models {
    public class ContentItem {
        public string? Name {get; set;}
        public string? Description{get; set;}
        public string? Path{get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid StudentId {get; set;}
        public Guid AssignmentId {get; set;}
        public Guid ModuleId {get; set;}

    }
}