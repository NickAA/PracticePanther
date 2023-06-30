using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }

        private void ProjectMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Project");
        }

        private void ClientMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Client");
        }

        private void EmployeeMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Employee");
        }
    }
}