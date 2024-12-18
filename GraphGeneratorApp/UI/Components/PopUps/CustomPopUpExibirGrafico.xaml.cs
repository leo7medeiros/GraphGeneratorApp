using Mopups.Services;
using Mopups.Pages;
using Microcharts;

namespace GraphGeneratorApp.UI.Components;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomPopUpExibirGrafico : PopupPage
{
    #region PROPERTIES

    private Chart _grafico;
    public Chart Grafico
    {
        get { return _grafico; }
        private set { _grafico = value; OnPropertyChanged(nameof(Grafico)); }
    }

    private string _descricao;
    public string Descricao
    {
        get { return _descricao; }
        private set { _descricao = value; OnPropertyChanged(nameof(Descricao)); }
    }

    #endregion

    public CustomPopUpExibirGrafico(Chart grafico, string descricao)
    {
        BindingContext = this;
        InitializeComponent();
        ConfiguraInformacoes(grafico, descricao);
    }

    private void ConfiguraInformacoes(Chart grafico, string descricao)
    {
        Grafico = grafico;
        Descricao = descricao;
    }

    private async void ButtonOk_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}