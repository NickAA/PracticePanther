using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class CreateTimeView : ContentPage
{
	public CreateTimeView()
	{
		InitializeComponent();
		BindingContext = new TimeViewModel();
	}

    private void AddTime(object sender, EventArgs e)
    {
		(BindingContext as TimeViewModel).Add();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel();
    }

    private void TimeMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Time");
    }
}