using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(TimeId), "timeid")]
public partial class TimeView : ContentPage
{
	public int TimeId { get; set; }
	public TimeView()
	{
		InitializeComponent();
		BindingContext = new TimeViewModel();
	}

    private void TimeMenu(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Time");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel(TimeId);
    }
}