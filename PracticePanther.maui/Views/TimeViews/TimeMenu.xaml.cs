using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class TimeMenu : ContentPage
{
    public TimeMenu()
    {
        InitializeComponent();
        BindingContext = new TimeViewModel();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TimeViewModel).Delete();
    }

    private void NewTimeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewTime");
    }

    private void ToMainMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void UpdateTime(object sender, EventArgs e)
    {
        if ((BindingContext as TimeViewModel).SelectedTime != null)
        {
            var timeID = (BindingContext as TimeViewModel).SelectedTime.Id;
            Shell.Current.GoToAsync($"//UpdateTime?timeid={timeID}");
        }
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new TimeViewModel(true);
    }

    private void TimeDetailsClicked(object sender, EventArgs e)
    {
        if ((BindingContext as TimeViewModel).SelectedTime != null)
        {
            var timeID = (BindingContext as TimeViewModel).SelectedTime.Id;
            Shell.Current.GoToAsync($"//TimeView?timeid={timeID}");
        }
    }
}