using Canvas.Models;
using Canvas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    internal class AdminStudentsViewModel : INotifyPropertyChanged
    {
        private StudentService studentSvc;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<Student> Students
        {
            get
            {
                return new ObservableCollection<Student>(studentSvc.Students);
            }
        }

        public Student SelectedStudent
        {
            get; set;
        }

        public void AddStudent()
        {
            studentSvc.Add(new Student { Name = "This is a new Student" });
            NotifyPropertyChanged(nameof(Students));
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Students));
        }

        public void Remove()
        {
            studentSvc.Delete(SelectedStudent);
            Refresh();
        }

        public AdminStudentsViewModel()
        {
            studentSvc = StudentService.Current;
        }
    }
}
