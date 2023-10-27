using CommunityToolkit.Mvvm.ComponentModel;
using MauiApiRest.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace MauiApiRest.ViewModels;

public partial class MainViewModel : ObservableObject
{
    HttpClient client;
    JsonSerializerOptions _serializerOptions;
    string baseUrl = "5000/v1/";

    [ObservableProperty]
    public string _categoriaInfoId;
    [ObservableProperty]
    public string _categoriaInfoNome;
    [ObservableProperty]
    public Produto _categoria;
    [ObservableProperty]
    public ObservableCollection<Produto> _categorias;
    [ObservableProperty]
    private string _nome;
    [ObservableProperty]
    private string _imagemUrl;

    public MainViewModel()
    {
        client = new HttpClient();
        Categorias = new ObservableCollection<Produto>();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    //iniciar o consumo da api rest
    //retornar a coleção de categorias
    public ICommand GetCategoriasCommand =>
       new Command(async () => await CarregaCategoriasAsync());
    private async Task CarregaCategoriasAsync()
    {
        var url = "https://apiprodutos-k3vf.onrender.com/product";
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Produto>>(responseStream, _serializerOptions);
                Categorias = data;
            }
        }
    }

    public ICommand GetCategoriaCommand =>
     new Command(async () =>
     {
         if (CategoriaInfoId is not null)
         {
             var categoriaId = Convert.ToInt32(CategoriaInfoId);
             if (categoriaId > 0)
             {
                 var url = $"{baseUrl}/categorias/{categoriaId}";
                 var response = await client.GetAsync(url);

                 if (response.IsSuccessStatusCode)
                 {
                     using (var responseStream =
                             await response.Content.ReadAsStreamAsync())
                     {
                         var data = await JsonSerializer
                          .DeserializeAsync<Produto>(responseStream, _serializerOptions);
                         Categoria = data;
                     }
                 }
             }
         }
     });

        public ICommand AddCategoriaCommand =>
        new Command(async () =>
        {
            var url = "https://apiprodutos-k3vf.onrender.com/product";

            if (CategoriaInfoNome is not null)
            {
                var categoria =
                  new Produto
                  {
                       Nome = CategoriaInfoNome,
                       Descricao = "Teste", 
                       Preco = 0.10,                      
                  };
                string json = JsonSerializer.Serialize<Produto>(categoria, _serializerOptions);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                await CarregaCategoriasAsync();
            }
        });

    public ICommand UpdateCategoriaCommand =>
       new Command(async () =>
       {
           if (CategoriaInfoId is not null && CategoriaInfoNome is not null)
           {
               var categoriaId = Convert.ToInt32(CategoriaInfoId);
               var categoria = Categorias.FirstOrDefault(x => x.Id == categoriaId);

               var url = $"https://apiprodutos-k3vf.onrender.com/product/{categoriaId}";
               categoria.Nome = CategoriaInfoNome;

               string jsonResponse = JsonSerializer.Serialize<Produto>(categoria, _serializerOptions);

               StringContent content = new StringContent(jsonResponse, Encoding.UTF8, "application/json");

               var response = await client.PutAsync(url, content);
               await CarregaCategoriasAsync(); // Atualiza a lista de produtos
           }
       });

    public ICommand DeleteCategoriaCommand =>
         new Command(async () =>
         {
             if (CategoriaInfoId is not null)
             {
                 var categoriaId = Convert.ToInt32(CategoriaInfoId);
                 if (categoriaId > 0)
                 {
                     var url = $"https://apiprodutos-k3vf.onrender.com/product/{categoriaId}";
                     var response = await client.DeleteAsync(url);
                     await CarregaCategoriasAsync();
                 }
             }
         });
}
