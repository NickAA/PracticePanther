using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();


        }


        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Search();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Delete();
        }
    }
}