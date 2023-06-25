using PracticePanther.maui.ViewModels;

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
}