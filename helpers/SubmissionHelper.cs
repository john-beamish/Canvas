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

            if (student != null)
            {
                Assignment assignment = GetAssignment(student);

                if (assignment != null)
                {
                    Console.WriteLine("Submission Name:");
                    var nameChoice = Console.ReadLine();

                    Console.WriteLine($"Grade (Max Points: {assignment.AvailablePoints}):");
                    var gradeChoice = Console.ReadLine();

                    Console.WriteLine("Submission Description:");
                    var descriptionChoice = Console.ReadLine();

                    if(double.TryParse(gradeChoice, out double gradeChoiceDbl))
                    {
                        Submission submission = new Submission {Name = nameChoice, Grade = gradeChoiceDbl, Description = descriptionChoice};

                        if (assignment.Submissions == null){
                            assignment.Submissions = new List<Submission>();
                        }

                        if (student.Submissions == null){
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
                }
            }
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
            if (course.Assignments !=null)
            {
                int assignmentCount = 0;
                foreach (var assignment in course.Assignments)
                {
                    Console.WriteLine($"{++assignmentCount}, {assignment.Name}");
                }
            }
            else {
                Console.WriteLine("Error: Please create an Assignment for the course.\n");
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
            if (studentSvc.Students.Count() > 0) 
            {
                Console.WriteLine("Select Student:");
                PrintStudentList();
                var studentChoice = Console.ReadLine();
                
                if (int.TryParse(studentChoice, out int studentIntChoice)) 
                {
                    if (studentIntChoice > 0 && studentIntChoice <= studentSvc.Students.Count())
                    {
                        Student student;
                        student = StudentService.Current.Students.ElementAt(studentIntChoice - 1);
                        return student;
                    } 
                    else {
                        Console.WriteLine("Error: Please select a valid Student.\n");
                        return null;
                    }
                } 
                else {
                    Console.WriteLine("Error: Please select a valid Student.\n");
                    return null;
                }   
            }
            else {
                Console.WriteLine("Error, please create a Student.\n");
                return null;
            }
        }

        public Assignment GetAssignment(Student student)
        {
            if (student != null)
            {
                Console.WriteLine("Select Course:");
                PrintSchedule(student);
                var courseChoice = Console.ReadLine();
                
                if (int.TryParse(courseChoice, out int courseChoiceInt)) 
                {
                    if (courseChoiceInt > 0 && courseChoiceInt <= student.Schedule?.Count())
                    {
                        Course course;
                        course = student.Schedule.ElementAt(courseChoiceInt - 1);

                        Console.WriteLine("Select Assignment:");
                        PrintAssignmentList(course);
                        var assignmentChoice = Console.ReadLine();

                        if (int.TryParse(assignmentChoice, out int assignmentChoiceInt)) 
                        {
                            if (assignmentChoiceInt > 0 && assignmentChoiceInt <= course?.Assignments?.Count())
                            {
                                Assignment assignment;
                                assignment = course.Assignments.ElementAt(assignmentChoiceInt - 1);
                                    
                                return assignment;
                            }
                            else {
                                Console.WriteLine("Error, please select a valid Assignment.\n");
                                return null;
                            }
                        }
                        else {
                            Console.WriteLine("Error, please select a valid Assignment.\n");
                            return null;
                        }
                    }
                    else {
                        Console.WriteLine("Error, please select a valid Course.\n");
                        return null;
                    }
                }
                else {
                    Console.WriteLine("Error, please select a valid Assignment.\n");
                    return null;
                }
            }
            else {
                Console.WriteLine("Error, please select a valid Student.\n");
                return null;
            }
        }

        public void PrintSchedule(Student student)
        {
            if (student.Schedule != null) 
            {
                int courseCount = 0;
                foreach (Course course in student.Schedule)
                {
                Console.WriteLine($"{++courseCount}, {course.Name}");
            }
            } else {
                Console.WriteLine("Error, Student Schedule is empty.\n");
            }
        }
    }
}