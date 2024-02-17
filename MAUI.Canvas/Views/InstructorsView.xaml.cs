using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class InstructorsView : ContentPage
{
	public InstructorsView()
	{
		InitializeComponent();
        BindingContext = new InstructorsViewModel();
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InstructorsViewModel)?.Refresh();
    }


    private void Students_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AdminStudents");
    }
}