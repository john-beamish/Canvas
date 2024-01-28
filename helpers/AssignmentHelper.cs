using Canvas.Models;
using Canvas.Services;

namespace Canvas.helpers {
    public class AssignmentHelper {
        private AssignmentService assignmentSvc = AssignmentService.Current;

        public void NewAssignment() 
        {
            Console.WriteLine("Assignment Name:");
            var name = Console.ReadLine();

            Console.WriteLine("Assignment Description:");
            var description = Console.ReadLine();

            Console.WriteLine("Total available points:");
            var availablePoints = Console.ReadLine();

            Console.WriteLine("Due Date (MM-DD-YYYY):");
            var dueDate = Console.ReadLine();

            Assignment myAssignment;
            if(double.TryParse(availablePoints, out double availablePointsDbl)) 
            {  
                if(DateTime.TryParse(dueDate, out DateTime parsedDueDate))
                {
                     myAssignment = new Assignment
                    {Name = name, Description = description, AvailablePoints = availablePointsDbl, DueDate = parsedDueDate};
                }
            
                else {
                myAssignment = new Assignment
                    {Name = name, Description = description, AvailablePoints = availablePointsDbl};
                }
                
            } else {
                myAssignment = new Assignment
                {Name = name, Description = description};
            }

            Console.WriteLine("Assignment created successfully!");
            assignmentSvc.Add(myAssignment);
            PrintAssignment(myAssignment);
        }

        public void PrintAssignment(Assignment assignment)
        {
            Console.WriteLine(assignment);

        }

        public void PrintAssignmentList()
        {
            int assignmentCount = 0;
            AssignmentService.Current.Assignments.ToList().ForEach(
                a => Console.WriteLine($"{++assignmentCount}, {a.Name} - Total Available Points: {a.AvailablePoints} - Due Date: {a.DueDate.ToString()}\n" +
                $"{a.Description}\n")
            ); 
        }
    }
}