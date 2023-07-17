using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

public partial class BillsMenu : ContentPage
{
	public BillsMenu()
	{
		InitializeComponent();
		BindingContext = new BillViewModel();
	}
    private void SearchClicked(object sender, EventArgs e)
    { (BindingContext as BillViewModel).Search(); }

    private void DeleteClicked(object sender, EventArgs e)
    { (BindingContext as BillViewModel).Delete(); }

    private void NewBillClicked(object sender, EventArgs e)
    { Shell.Current.GoToAsync("//NewBill"); }

    private void ToMainMenu(object sender, EventArgs e)
    { Shell.Current.GoToAsync("//MainPage"); }
    private void UpdateBill(object sender, EventArgs e)
    {
        if ((BindingContext as BillViewModel).SelectedBill != null)
        {
            var billId = (BindingContext as BillViewModel).SelectedBill.ID;
            Shell.Current.GoToAsync($"//UpdateBill?billid={billId}");
        }
    }

    private void OnDeparting(object sender, NavigatedFromEventArgs e)
    { BindingContext = null; }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    { BindingContext = new BillViewModel(true); }
}