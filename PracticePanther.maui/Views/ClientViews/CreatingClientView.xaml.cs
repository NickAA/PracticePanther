using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class CreatingClientView : ContentPage
{
	public CreatingClientView()
	{
		InitializeComponent();

        BindingContext = new ClientViewModel();
	}

    private void ClientMenu(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Client");
    }

    private void AddClient(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Add();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel();
    }
}