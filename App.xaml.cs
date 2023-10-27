using MauiApiRest.Pages;
using MauiApiRest.ViewModels;

namespace MauiApiRest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MasterPage());
                
        }
    }
}