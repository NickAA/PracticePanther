using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(TimeId), "timeid")]
public partial class UpdateTime : ContentPage
{
	public int TimeId { get; set; }

	public UpdateTime()
	{
		InitializeComponent();
		BindingContext = new TimeViewModel();
	}

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel(TimeId);
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void SaveTime(object sender, EventArgs e)
    {
        (BindingContext as TimeViewModel).Save();
    }
    private void TimeMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Time");
    }
}