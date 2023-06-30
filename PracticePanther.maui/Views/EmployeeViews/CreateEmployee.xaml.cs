using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class CreateEmployee : ContentPage
{
    public CreateEmployee()
    {
        InitializeComponent();

        BindingContext = new EmployeeViewModel();
    }

    private void EmployeeMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Employee");
    }

    private void AddEmployee(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Add();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel();
    }
}