
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApiRest.Models;
using MauiApiRest.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApiRest.ViewModels
{
    public partial class ProductDetailsViewModel : ObservableObject
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;

        [ObservableProperty]
        public Produto _produto;

        public ProductDetailsViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public ICommand UpdateProductCommand => new Command(async () => await AtualizaProdutosAsync());
        public async Task AtualizaProdutosAsync()
        {
            if (Produto.Id > 0 && Produto.Nome is not null)
            {
                var url = $"https://apiprodutos-k3vf.onrender.com/product/{Produto.Id}";

                string jsonResponse = JsonSerializer.Serialize<Produto>(Produto, _serializerOptions);

                StringContent content = new StringContent(jsonResponse, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);
                await App.Current.MainPage.DisplayAlert("Atualização", "Produto Atualizado com sucesso!","Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        public ICommand DeleteProductCommand => new Command(async () => await ExcluiProdutosAsync());

        public async Task ExcluiProdutosAsync()
        {

            if (Produto.Id > 0)
            {
                var url = $"https://apiprodutos-k3vf.onrender.com/product/{Produto.Id}";
                var response = await client.DeleteAsync(url);
                await App.Current.MainPage.DisplayAlert("Atualização", "Produto excluído com sucesso!", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
