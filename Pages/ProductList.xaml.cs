using MauiApiRest.ViewModels;

namespace MauiApiRest.Pages;

public partial class ProductList : ContentPage
{
	public ProductList()
	{
		InitializeComponent();
		BindingContext = new ProductsViewModel();
    }
}