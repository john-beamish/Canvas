using System;
using Canvas.Models;

namespace Canvas // Note: actual namespace depends on the project name.
{
    internal class Program
    {
    
        static void Main(string[] args)
        {
            var courses = new List<Course>();
            Console.WriteLine("Welcome to Canvas!");
            Console.WriteLine("A. Add a Course");
            Console.WriteLine("D. Delete a Course");

            string? choice = Console.ReadLine();
            var length = choice?.Length ?? 0;

            switch(choice) {

                case "A":
                case "a":
                AddCourse(courses);
                break;
            }

           
            foreach(Course c in courses) {
                Console.WriteLine(c);
            }
        }

        public static void AddCourse(IList<Course> courses) {
            
            Console.WriteLine("Name:");
            var name = Console.ReadLine();

            Console.WriteLine("Code:");
            var code = Console.ReadLine();

            Console.WriteLine("Credits:");
            var credits = Console.ReadLine();

            Course myCourse;
            if(double.TryParse(credits, out double creditsDbl)) {
                myCourse = new Course{Name = name, Code = code, Credits = creditsDbl};
            } else {
                myCourse = new Course{Name = name, Code = code};
            }
             
            courses.Add(myCourse);
        }
    }
}