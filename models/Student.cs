using System.Text;

namespace Canvas.Models {
    public class Student {
        public string? Name {get; set;}
        public string? Year {get; set;}
        public List<Course>? Schedule{get; set;}
        public List<double>? Grades{get; set;}
        public Guid Id {get;}
        public Guid CourseId {get; set;}
        public Guid AssignmentId {get; set;}
        public Guid ContentItemId {get; set;}
        public Guid ModuleId {get; set;}

        public Student()
        {
            Id = Guid.NewGuid(); // Assign a new Guid as the Id
        }
        public override string ToString()
        {   
            StringBuilder scheduleString = new StringBuilder();
            StringBuilder gradesString = new StringBuilder();

            if (Schedule != null)
            {
                if(Grades != null)
                {
                    foreach (Course course in Schedule)
                    {
                        scheduleString.AppendLine($"    ({course.Code}) - {course.Name} - {course.Credits} Credits\n");
                    }

                    foreach (double grade in Grades)
                    {
                        scheduleString.AppendLine($"    Grade: {grade}\n");
                    }
                
                    return 
                    $"{Name} - {Year}\n" +
                    $"Schedule:\n" +
                    $"{scheduleString}" +
                    $"Grades:\n" +
                    $"{gradesString}";

                }
                else
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
                
            } else 
            {
                return 
                $"{Name} - {Year}\n";
            }

        }
    }
}