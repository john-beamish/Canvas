using Canvas.Services;
using Canvas.Models;
using System.Transactions;

namespace Canvas.helpers
{
    public class SubmissionHelper
    {
        private SubmissionService submissionSvc = SubmissionService.Current;
        private AssignmentService assignmentSvc = AssignmentService.Current;
        private CourseService courseSvc = CourseService.Current;
        private StudentService studentSvc = StudentService.Current;

        public void NewSubmission()
        {    
            Student student = GetStudent();

            if (student == null)
            {
                return;
            }
            Assignment assignment = GetAssignment(student);

            if (assignment == null)
            {
                return;
            }
                
            Console.WriteLine("Submission Name:");
            var nameChoice = Console.ReadLine();

            Console.WriteLine($"Grade (Max Points: {assignment.AvailablePoints}):");
            var gradeChoice = Console.ReadLine();

            if (double.TryParse(gradeChoice, out double gradeChoiceDbl) == false)
            {
                Console.WriteLine("Error: Please enter a number for Submission Grade.\n");
                return;
            }

            if (gradeChoiceDbl > assignment.AvailablePoints)
            {
                Console.WriteLine($"Error: Submission Grade is greater than the total available points for Assignment '{assignment.Name}'.\n");
                return;
            }

            if (gradeChoiceDbl < 0)
            {
                Console.WriteLine("Error: Grade cannot be negative.\n");
                return;
            }

            Console.WriteLine("Submission Date (MM-DD-YYYY):");
            var dateChoice = Console.ReadLine();

            if (DateTime.TryParse(dateChoice, out DateTime validDateChoice) == false)
            {
                Console.WriteLine("Error: Please enter a valid Submission Date (MM-DD-YYYY).\n");
                return;
            }

            Console.WriteLine("Submission Description:");
            var descriptionChoice = Console.ReadLine();

                   
            Submission submission = new Submission {Name = nameChoice, Grade = gradeChoiceDbl, Date = validDateChoice, Description = descriptionChoice};

            if (assignment.Submissions == null)
            {
                assignment.Submissions = new List<Submission>();
            }

            if (student.Submissions == null)
            {
                student.Submissions = new List<Submission>();
            }
                        
            submission.Assignment = assignment;
            submission.AssignmentId = assignment.Id;
                                    
            submission.Student = student;
            submission.StudentId = student.Id;

            assignment.Submissions.Add(submission);
            student.Submissions.Add(submission);
                                    
            submissionSvc.Add(submission);

            Console.WriteLine("Submission created successfully!\n");
            PrintSubmission(submission);
        }

        public void DeleteSubmission()
        {
            Submission submission = SelectSubmission();

            if (submission != null)
            {
                submissionSvc.Delete(submission);
                Console.WriteLine($"({submission?.Assignment?.Course?.Name} - {submission?.Assignment?.Name}) - {submission?.Name} successfully Deleted.\n");
            }
        }

        public void Update() 
        {
            Submission submission = SelectSubmission();

            if (submission == null)
            {
                return;
            }

            string? updatedName;
            double? updatedGrade;;
            string? updatedDescription;
            DateTime? updatedDate;
                
            Console.WriteLine("Updated Submission Name:");
            updatedName = Console.ReadLine();

            Console.WriteLine($"Updated Grade (Max Points: {submission?.Assignment?.AvailablePoints}):");
            var gradeChoice = Console.ReadLine();

            if(double.TryParse(gradeChoice, out double gradeChoiceDbl) == false)
            {
                Console.WriteLine("Error: Please enter a number for Submission Grade."); 
                Console.WriteLine("Submission not updated.\n");
                return;
            }

            if (gradeChoiceDbl > submission?.Assignment?.AvailablePoints)
            {
                Console.WriteLine($"Error: Updated Submission Grade is greater than the total available points for Assignment '{submission.Assignment.Name}'.");
                Console.WriteLine("Submission not updated.\n");
                return;
            }

            if (gradeChoiceDbl < 0)
            {
                Console.WriteLine("Error: Grade cannot be negative.");
                Console.WriteLine("Submission not updated.\n");
                return;
            }

            updatedGrade = gradeChoiceDbl;

            Console.WriteLine("Updated Submission Date (MM-DD-YYYY):");
            var dateChoice = Console.ReadLine();

            if (DateTime.TryParse(dateChoice, out DateTime validDateChoice) == false)
            {
                Console.WriteLine("Error: Please enter a valid Submission Date (MM-DD-YYYY).");
                Console.WriteLine("Submission not updated.\n");
                return;
            }

            updatedDate = validDateChoice;

            Console.WriteLine("Updated Submission Description:");
            updatedDescription = Console.ReadLine();

            submission.Name = updatedName;
            submission.Grade = updatedGrade;
            submission.Date = updatedDate;
            submission.Description = updatedDescription;

            Console.WriteLine("Submission successfully Updated!\n");
            PrintSubmission(submission);
        }

        public void PrintSubmission(Submission submission)
        {
            Console.WriteLine(submission);
        }

        public void PrintAssignmentList()
        {
         int assignmentCount = 0;
            foreach (var assignment in AssignmentService.Current.Assignments)
            {
                Console.WriteLine($"{++assignmentCount}, {assignment.Name}");
            }
        }

        public void PrintAssignmentList(Course course)
        {
            if (course.Assignments == null)
            {
                Console.WriteLine("Error: Please create an Assignment for the course.\n");
                return;
            }
            
            int assignmentCount = 0;
            
            foreach (var assignment in course.Assignments)
            {
                Console.WriteLine($"{++assignmentCount}, {assignment.Name}");
            }
        }

        public void PrintStudentList()
        {
         int studentCount = 0;
            foreach (var student in StudentService.Current.Students)
            {
                Console.WriteLine($"{++studentCount}, {student.Name}");
            }
        }

        public Student GetStudent()
        {
            if (studentSvc.Students.Count() <= 0) 
            {
                Console.WriteLine("Error, please create a Student.\n");
                return null;
            }
                
            Console.WriteLine("Select Student:");
            PrintStudentList();
            var studentChoice = Console.ReadLine();
                
            if (int.TryParse(studentChoice, out int studentIntChoice) == false) 
            {
                Console.WriteLine("Error: Please select a valid Student.\n");
                return null;
            }

            if (studentIntChoice <= 0 || studentIntChoice > studentSvc.Students.Count())
            {
                Console.WriteLine("Error: Please select a valid Student.\n");
                return null;
            }
            
            Student student = StudentService.Current.Students.ElementAt(studentIntChoice - 1);
            
            return student;
        }

        public Assignment GetAssignment(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("Error, please select a valid Student.\n");
                return null;
            }

            Course? course = SelectCourse(student);

            if (course.Assignments == null)
            {
                Console.WriteLine($"Error: Please create an Assignment for {course.Name}.\n");
                return null;
            }

            Console.WriteLine("Select Assignment:");
            PrintAssignmentList(course);
            var assignmentChoice = Console.ReadLine();

            if (int.TryParse(assignmentChoice, out int assignmentChoiceInt) == false) 
            {
                Console.WriteLine("Error, please select a valid Assignment.\n");
                return null;
            }
            
            if (assignmentChoiceInt <= 0 || assignmentChoiceInt > course?.Assignments?.Count())
            {
                Console.WriteLine("Error, please select a valid Assignment.\n");
                return null;
            }
            
            Assignment assignment = course.Assignments.ElementAt(assignmentChoiceInt - 1);
                                    
            return assignment;
        }

        public void PrintSchedule(Student student)
        {
            if (student.Schedule == null) 
            {
                Console.WriteLine("Error, Student Schedule is empty.\n");
                return;
            }
            
            int courseCount = 0;
            foreach (Course course in student.Schedule)
            {
                Console.WriteLine($"{++courseCount}, {course.Name}");
            } 
        }

        public Course SelectCourse(Student student)
        {
            Console.WriteLine("Select Course:");
            PrintSchedule(student);
            var courseChoice = Console.ReadLine();
                
            if (int.TryParse(courseChoice, out int courseChoiceInt) == false) 
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }
            
            if (courseChoiceInt <= 0 || courseChoiceInt > student.Schedule?.Count())
            {
                Console.WriteLine("Error, please select a valid Course.\n");
                return null;
            }
             
            Course course = student.Schedule.ElementAt(courseChoiceInt - 1);

            return course;
        }

        public Submission SelectSubmission()
        {
            var student = GetStudent();

            if (student == null)
            {
                return null;
            }

            if (student.Submissions == null)
            {
                Console.WriteLine("Error: Please create a Submission for the Student.\n");
                return null;
            }

            Console.WriteLine("Select Submission:");
            int submissionCount = 0;
            foreach (Submission submission in student.Submissions.ToList())
            {
                Console.WriteLine($"{++submissionCount}, ({submission?.Assignment?.Course?.Name} - {submission?.Assignment?.Name}) - {submission?.Name}");
            }

            var submissionChoice = Console.ReadLine();
                
            if (int.TryParse(submissionChoice, out int submissionChoiceInt) == false) 
            {
                Console.WriteLine("Error: Please select a valid Submission.\n");
                return null;
            }
                       
            if (submissionChoiceInt <= 0 || submissionChoiceInt > student?.Submissions?.Count())
            {
                Console.WriteLine("Error: Please select a valid Submission.\n");
                return null;
            }
                            
            Submission mySubmission = student.Submissions.ElementAt(submissionChoiceInt - 1);
                                    
            return mySubmission;
        }
    }
}