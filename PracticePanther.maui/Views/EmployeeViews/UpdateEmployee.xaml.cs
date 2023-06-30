using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(EmployeeId), "employeesid")]
public partial class UpdateEmployee : ContentPage
{
    public int EmployeeId { get; set; }
	public UpdateEmployee()
	{
		InitializeComponent();
		BindingContext = new EmployeeViewModel();
	}
    private void EmployeeMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Employee");
    }

    private void SaveEmployee(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Save();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel(EmployeeId);
    }
}