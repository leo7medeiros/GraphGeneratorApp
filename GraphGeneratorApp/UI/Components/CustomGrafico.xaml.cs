using GraphGeneratorApp.ViewModels;

namespace GraphGeneratorApp.UI.Components;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomGrafico : ContentView
{
    private GraficoViewModel? _vm => BindingContext as GraficoViewModel;

    public CustomGrafico()
    {
        InitializeComponent();
        BindingContext = new GraficoViewModel();
    }

    private void btnEditar_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is GraficoViewModel context)
        {
            _vm?.EditarGrafico?.Invoke(context.Simulacao);
        }
    }

    private void btnExcluir_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int id)
        {
            _vm?.ExcluirGrafico?.Invoke(id);
        }
    }

    private void AbrirGrafico_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is GraficoViewModel context)
        {
            _vm?.AbrirPopUpGrafico?.Invoke(context.Grafico, context.Simulacao.Descricao);
        }
    }
}