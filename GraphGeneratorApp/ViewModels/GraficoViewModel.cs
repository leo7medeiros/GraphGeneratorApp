using GraphGeneratorApp.Models;
using GraphGeneratorApp.UI.Views.BaseContent;
using Microcharts;

namespace GraphGeneratorApp.ViewModels;

public class GraficoViewModel : BaseViewModel
{
    #region PROPERTIES

    private SimulacaoModel _simulacao;
    public SimulacaoModel Simulacao
    {
        get => _simulacao;
        set { _simulacao = value; OnPropertyChanged(nameof(Simulacao)); }
    }

    private Chart _grafico;
    public Chart Grafico
    {
        get => _grafico;
        set { _grafico = value; OnPropertyChanged(nameof(Grafico)); }
    }

    #endregion

    #region ACTIONS

    private Action<Chart, string> _abrirPopUpGrafico;
    public Action<Chart, string> AbrirPopUpGrafico
    {
        get { return _abrirPopUpGrafico; }
        set { _abrirPopUpGrafico = value; }
    }

    private Action<int> _excluirGrafico;
    public Action<int> ExcluirGrafico
    {
        get { return _excluirGrafico; }
        set { _excluirGrafico = value; }
    }

    private Action<SimulacaoModel> _editarGrafico;
    public Action<SimulacaoModel> EditarGrafico
    {
        get { return _editarGrafico; }
        set { _editarGrafico = value; }
    }

    #endregion

    public GraficoViewModel()
    {

    }

    public GraficoViewModel(SimulacaoModel simulacao, Chart grafico)
    {
        _simulacao = simulacao;
        _grafico = grafico;
    }
}