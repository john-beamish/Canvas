using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class AdminStudentsView : ContentPage
{
    public AdminStudentsView()
    {
        InitializeComponent();
        BindingContext = new AdminStudentsViewModel();
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructors");
    }



    private void Add_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//StudentDetail");
        //(BindingContext as InstructorsViewModel)?.AddStudent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AdminStudentsViewModel)?.Refresh();
    }

    private void Remove_Clicked(object sender, EventArgs e)
    {
        (BindingContext as AdminStudentsViewModel)?.Remove();
    }

}