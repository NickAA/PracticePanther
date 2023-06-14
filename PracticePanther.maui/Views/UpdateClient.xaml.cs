using PracticePanther.maui.ViewModels;
using PracticePanther.Models;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(ClientsId), "clientsid")]
public partial class UpdateClient : ContentPage
{
	public int ClientsId { set; get; }

	public UpdateClient()
	{
		InitializeComponent();
	}

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new ClientViewModel(ClientsId);
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
		BindingContext = null;
    }
}