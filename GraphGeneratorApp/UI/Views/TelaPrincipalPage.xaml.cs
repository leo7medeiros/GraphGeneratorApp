using GraphGeneratorApp.UI.Views.BaseContent;

namespace GraphGeneratorApp.UI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TelaPrincipalPage : BaseContentPage
{
    public TelaPrincipalPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}