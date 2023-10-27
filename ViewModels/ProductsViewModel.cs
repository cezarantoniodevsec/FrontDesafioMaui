using CommunityToolkit.Mvvm.ComponentModel;
using MauiApiRest.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace MauiApiRest.ViewModels
{
    public partial class ProductsViewModel: ObservableObject
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        // string baseUrl = "https://apiprodutos-k3vf.onrender.com/";

        [ObservableProperty]
        public string _id;
        [ObservableProperty]
        public string _nome;
        [ObservableProperty]
        public Produto _descricao;
        [ObservableProperty]
        public Produto _preco;
        [ObservableProperty]
        public ObservableCollection<Produto> _productsList;

        public ProductsViewModel() {
            client = new HttpClient();
            ProductsList = new ObservableCollection<Produto>();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public ICommand GetProductCommand => new Command(async () => await RecuperaProdutosAsync());
        public async Task RecuperaProdutosAsync()
        {
            var url = "https://apiprodutos-k3vf.onrender.com/product";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Produto>>(responseStream, _serializerOptions);
                    ProductsList = data;
                }
            }
        }

       
    }
}
