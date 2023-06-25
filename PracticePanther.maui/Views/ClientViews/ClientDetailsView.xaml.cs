using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(ClientsId), "clientsid")]
public partial class ClientDetailsView : ContentPage
{
    public int ClientsId { get; set; }
	public ClientDetailsView()
	{
		InitializeComponent();
	}

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(ClientsId);
    }

    private void OnDeparting(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void ClientMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
    }
}