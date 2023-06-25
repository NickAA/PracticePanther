using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class ProjectMenu : ContentPage
{
	public ProjectMenu()
	{
		InitializeComponent();
		BindingContext = new ProjectViewModel();
	}

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).Delete();
    }

    private void NewClientClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewProject");
    }

    private void ToMainMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void UpdateClient(object sender, EventArgs e)
    {
        if ((BindingContext as ProjectViewModel).SelectedProject != null)
        {
            var projectID = (BindingContext as ProjectViewModel).SelectedProject.Id;
            Shell.Current.GoToAsync($"//UpdateProject?projectsid={projectID}");
        }
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProjectViewModel(true);
    }

    private void ClientDetailsClicked(object sender, EventArgs e)
    {
        if ((BindingContext as ProjectViewModel).SelectedProject != null)
        {
            var projectID = (BindingContext as ProjectViewModel).SelectedProject.Id;
            Shell.Current.GoToAsync($"//ProjectDetails?projectsid={projectID}");
        }
    }

}