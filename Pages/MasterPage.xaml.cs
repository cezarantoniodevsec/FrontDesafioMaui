using MauiApiRest.Models;
using MauiApiRest.ViewModels;

namespace MauiApiRest.Pages;

public partial class MasterPage : ContentPage
{
    private ProductsViewModel viewModel; 
	public MasterPage()
	{
		InitializeComponent();

	}
    protected override async void OnAppearing()
    {
        this.viewModel = new ProductsViewModel();
        await viewModel.RecuperaProdutosAsync();
        BindingContext = viewModel;
    }

    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var IdSelecionado = (e.CurrentSelection.FirstOrDefault() as Produto)?.Id;
        var productDetails = new Produto();
        foreach (var product in this.viewModel.ProductsList.ToList())
        {
            if (product.Id == IdSelecionado){
                productDetails = product;
            }
        }
        var productDet = new ProductDetailsViewModel{
            Produto = productDetails,
        };
        var DetailPage = new NewPage1();
        DetailPage.BindingContext = productDet;
        Navigation.PushAsync(DetailPage);
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        var AddPage = new AddProduct();
        var productAdd = new AddProductViewModel
        {
            Produto = new(),
        };
        AddPage.BindingContext = productAdd;
        Navigation.PushAsync(AddPage);
    }
}