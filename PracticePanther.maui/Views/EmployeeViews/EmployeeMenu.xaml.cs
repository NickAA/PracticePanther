using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class EmployeeMenu : ContentPage
{
	public EmployeeMenu()
	{
		InitializeComponent();
        BindingContext = new EmployeeViewModel();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Search();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Delete();
    }

    private void NewEmployeeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewEmployee");
    }

    private void ToMainMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void UpdateEmployee(object sender, EventArgs e)
    {
        if ((BindingContext as EmployeeViewModel).SelectedEmployee != null)
        {
            var employeeId = (BindingContext as EmployeeViewModel).SelectedEmployee.Id;
            Shell.Current.GoToAsync($"//UpdateEmployee?employeesid={employeeId}");
        }
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel(true);
    }

    private void EmployeeDetailsClicked(object sender, EventArgs e)
    {
        if ((BindingContext as EmployeeViewModel).SelectedEmployee != null)
        {
            var employeeId = (BindingContext as EmployeeViewModel).SelectedEmployee.Id;
            Shell.Current.GoToAsync($"//UpdateEmployee?employeesid={employeeId}");
        }
    }


}