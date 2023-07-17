using PracticePanther.maui.ViewModels;

namespace PracticePanther.maui.Views;

[QueryProperty(nameof(BillId), "billid")]
public partial class UpdateBill : ContentPage
{
    public int BillId { set; get; }
	public UpdateBill()
	{
		InitializeComponent();
		BindingContext = new BillViewModel();
	}

    private void AddBill(object sender, EventArgs e)
    {
        (BindingContext as BillViewModel).Save();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new BillViewModel(BillId);
    }

    private void BillMenu(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Bills");
    }

}