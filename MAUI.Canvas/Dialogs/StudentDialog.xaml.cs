using Canvas.Models;
using Canvas.Services;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

public partial class StudentDialog : ContentPage
{
	public StudentDialog()
	{
		InitializeComponent();
        BindingContext = new StudentDialogViewModel();
	}

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructors");
    }

    private void Ok_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).AddStudent();
        Shell.Current.GoToAsync("//AdminStudents");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new StudentDialogViewModel();
    }

    private void Freshman_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).Year = "Freshman";
    }

    private void Sophomore_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).Year = "Sophomore";
    }

    private void Junior_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).Year = "Junior";
    }

    private void Senior_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).Year = "Senior";
    }

    private void Grad_Student_Clicked(object sender, EventArgs e)
    {
        (BindingContext as StudentDialogViewModel).Year = "Graduate Student";
    }
}