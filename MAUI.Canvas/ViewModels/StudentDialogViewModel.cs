using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canvas.Models;
using Canvas.Services;

namespace MAUI.Canvas.ViewModels
{
    public class StudentDialogViewModel
    {
        private Student? student;
        
        public String Name
        {
            get { return student?.Name ?? string.Empty; }
            set 
            {   
                if (student == null) student = new Student();
                student.Name = value;
            }
        }

        public string Year
        {
            get { return student?.Year ?? string.Empty; }
            set 
            { 
                if (student == null) student = new Student();
                student.Year = value;
            }
        }

        public StudentDialogViewModel()
        {
            student = new Student();
        }

        public void AddStudent()
        {
            if (student != null)
            {
                StudentService.Current.Add(student);
            }
        }
    }
}
