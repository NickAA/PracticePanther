using PracticePanther.maui.ViewModels;
using PracticePanther.Models;
using System.Collections.ObjectModel;

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
        (BindingContext as ClientViewModel).SelectedProjects = null;
		BindingContext = null;
    }

    private void ClientMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Client");
    }

    private void Save(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).Save();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var value = new ObservableCollection<Project>((e.CurrentSelection).Select(o => (o as Project)).Where(t => t != null));
        (BindingContext as ClientViewModel).SelectedProjects = value;
    }
}