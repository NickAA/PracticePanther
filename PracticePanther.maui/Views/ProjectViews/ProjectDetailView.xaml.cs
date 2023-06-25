using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(ProjectId), "projectsid")]
public partial class ProjectDetailView : ContentPage
{
    public int ProjectId { get; set; }
    public ProjectDetailView()
    {
        InitializeComponent();
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(ProjectId);
    }

    private void OnDeparting(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void ProjectMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Project");
    }
}