using CommunityToolkit.Mvvm.ComponentModel;
using MauiApiRest.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace MauiApiRest.ViewModels
{
    public partial class AddProductViewModel : ObservableObject
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;

        [ObservableProperty]
        public Produto _produto;
        public AddProductViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public ICommand AddProductCommand => new Command(async () => {
            var url = "https://apiprodutos-k3vf.onrender.com/product";
            if (Produto is not null){                
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                string json = JsonSerializer.Serialize<Produto>(Produto, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                await App.Current.MainPage.DisplayAlert("Add", "Product Added!", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
        });
    }
}
