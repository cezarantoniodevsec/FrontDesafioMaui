using MauiApiRest.ViewModels;

namespace MauiApiRest.Pages;

public partial class AddProduct : ContentPage
{
	public AddProduct()
	{
		InitializeComponent();
		BindingContext = new AddProductViewModel();
	}
}