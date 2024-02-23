using DoToo.ViewModel;

namespace DoToo.Views;

public partial class ItemView : ContentPage
{
	public ItemView(ItemViewModel viewModel)
	{
		InitializeComponent();
		viewModel.Navigation = Navigation;
		BindingContext = viewModel;
	}
}