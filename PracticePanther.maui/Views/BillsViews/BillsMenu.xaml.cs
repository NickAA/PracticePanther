using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class BillsMenu : ContentPage
{
	public BillsMenu()
	{
		InitializeComponent();
		BindingContext = new BillViewModel();
	}
    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as BillViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as BillViewModel).Delete();
    }

    private void NewClientClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewBill");
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

    private void OnDeparting(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillViewModel(true);
    }

    private void ClientDetailsClicked(object sender, EventArgs e)
    {
        if ((BindingContext as ClientViewModel).SelectedClient != null)
        {
            var clientsId = (BindingContext as ClientViewModel).SelectedClient.Id;
            Shell.Current.GoToAsync($"//ClientDetails?clientsid={clientsId}");
        }
    }
}