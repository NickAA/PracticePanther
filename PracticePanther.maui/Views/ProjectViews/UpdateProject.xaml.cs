using PracticePanther.maui.ViewModels;
using PracticePanther.Models;
using System.Collections.ObjectModel;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(ProjectsId), "projectsid")]
public partial class UpdateProject : ContentPage
{
    public int ProjectsId { get; set; }

    public UpdateProject()
    {
        InitializeComponent();
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ProjectsId);
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        ProjectsId = 0;
        BindingContext = null;
    }

    private void ProjectMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Project");
    }

    private void Save(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).Save();
    }

    private void ClientsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var value = new ObservableCollection<Client>((e.CurrentSelection).Select(o => (o as Client)).Where(t => t != null));
        if (ProjectsId != 0)
            (BindingContext as ProjectViewModel).AssociatedClients = value;
    }
}