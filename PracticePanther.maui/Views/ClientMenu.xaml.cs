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
    private void UpdateClient(object sender, EventArgs e)
    {
        if ((BindingContext as ClientViewModel).SelectedClient != null)
        {
            var clientsId = (BindingContext as ClientViewModel).SelectedClient.Id;
            Shell.Current.GoToAsync($"//UpdateClient?clientsid={clientsId}");
        }
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(true);
    }
}