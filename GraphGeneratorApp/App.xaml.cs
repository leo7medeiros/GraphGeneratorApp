using GraphGeneratorApp.UI.Views;

namespace GraphGeneratorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TelaPrincipal());
            NavigationPage.SetHasNavigationBar(MainPage, false);
        }
    }
}