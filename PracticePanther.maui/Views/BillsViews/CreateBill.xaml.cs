using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class CreateBill : ContentPage
{
	public CreateBill()
	{
		InitializeComponent();
		BindingContext = new BillViewModel();
	}
    private void AddBill(object sender, EventArgs e)
    {
        (BindingContext as BillViewModel).Add();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillViewModel();
    }

    private void BillMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Bills");
    }
}