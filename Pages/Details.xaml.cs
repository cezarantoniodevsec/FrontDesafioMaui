using MauiApiRest.ViewModels;

namespace MauiApiRest.Pages;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
        BindingContext = new ProductsViewModel();
    }
}