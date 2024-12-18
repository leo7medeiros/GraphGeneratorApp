using GraphGeneratorApp.Data.Enums;
using GraphGeneratorApp.Models;
using GraphGeneratorApp.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace GraphGeneratorApp.UI.Components;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomPopUpCadastroSimulacao : PopupPage
{
    private SimulacaoViewModel _vm;
    private SimulacaoModel _simulacao;

    public CustomPopUpCadastroSimulacao(SimulacaoViewModel vm, SimulacaoModel? simulacao = null)
    {
        InitializeComponent();
        _vm = vm;
        _simulacao = simulacao ?? new SimulacaoModel();
        BindingContext = _simulacao;
    }

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        if (ValidaCampos(_simulacao))
        {
            _vm?.SalvarGrafico(_simulacao);
            await MopupService.Instance.PopAsync();
        }
        else
        {
            await CustomPopUpMensagem.Alerta("AVISO!", "Todos os campos são obrigatórios.", Tipos.TipoNotificacao.Aviso);
        }
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

    private bool ValidaCampos(SimulacaoModel simulacao)
    {
        return true; // IMPLEMENTAR
    }
}