using PracticePanther.maui.ViewModels;
namespace PracticePanther.maui.Views;

public partial class ClientMenu : ContentPage
{
    public ClientMenu()
    {
        InitializeComponent();
        BindingContext = new ClientViewModel();
    }


    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Delete();
    }

    private void NewClientClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewClient");
    }

    private void ToMainMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}