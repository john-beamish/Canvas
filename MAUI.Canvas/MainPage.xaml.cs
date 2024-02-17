namespace MAUI.Canvas
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void InstructorViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Instructors");
        }

        private void StudentViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Students");
        }

    }
}
