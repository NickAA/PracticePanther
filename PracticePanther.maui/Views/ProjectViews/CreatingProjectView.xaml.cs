using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class CreatingProjectView : ContentPage
{
    public CreatingProjectView()
    {
        InitializeComponent();

        BindingContext = new ProjectViewModel();
    }

    private void ProjectMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Project");
    }

    private void AddProject(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).Add();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel();
    }
}