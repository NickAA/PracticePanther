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


        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}