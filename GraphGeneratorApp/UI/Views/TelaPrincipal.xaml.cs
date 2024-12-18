using GraphGeneratorApp.UI.Components;
using GraphGeneratorApp.UI.Views.BaseContent;
using GraphGeneratorApp.ViewModels;
using Mopups.Services;

namespace GraphGeneratorApp.UI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TelaPrincipal : BaseContentPage
{
    public SimulacaoViewModel? _vm => BindingContext as SimulacaoViewModel;

    public TelaPrincipal()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void NovaSimulacao_Clicked(object sender, EventArgs e)
    {
        var popup = new CustomPopUpCadastroSimulacao(_vm);
        await MopupService.Instance.PushAsync(popup);
    }

    private void LimparSimulacoes_Clicked(object sender, EventArgs e)
    {
        _vm.LimparListaGraficos_Command();
    }
}