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
            await CustomPopUpMensagem.Alerta("AVISO!", "Todos os campos são obrigatórios e devem possuir valor maior que zero!", Tipos.TipoNotificacao.Aviso);
        }
    }

    private async void Cancelar_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

    #region UTILITÁRIOS

    private bool ValidaCampos(SimulacaoModel simulacao)
    {
        return !string.IsNullOrEmpty(_simulacao.Descricao) && _simulacao.PrecoInicial > 0 && _simulacao.VotalidadeMedia > 0 && _simulacao.RetornoMedio > 0 && _simulacao.Tempo > 0;
    }

    private void SliderVotalidade_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _simulacao.VotalidadeMedia = Math.Round(e.NewValue);
        lblSliderVotalidade.Text = Math.Round(e.NewValue).ToString() + "%";
    }

    private void SliderRetorno_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _simulacao.RetornoMedio = Math.Round(e.NewValue);
        lblSliderRetorno.Text = Math.Round(e.NewValue).ToString() + "%";
    }

    private void OnColorFundoChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var selectedColor = (sender as RadioButton)?.Content.ToString();

            string CorFundo = selectedColor.Equals("AZUL") ? "#005495" : selectedColor.Equals("PRETO") ? "#000000" : selectedColor.Equals("LARANJA") ? "#F79B42" : "#000000";
            BoxFundo.Color = Color.FromHex(CorFundo);
            _simulacao.CorFundoGrafico = CorFundo;
        }
    }

    private void OnColorFrenteChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var selectedColor = (sender as RadioButton)?.Content.ToString();

            string CorFrente = selectedColor.Equals("LARANJA") ? "#F79B42" : selectedColor.Equals("BRANCO") ? "#FFFFFF" : selectedColor.Equals("AZUL") ? "#005495" : "#FFFFFF";
            BoxFrente.Color = Color.FromHex(CorFrente);
            _simulacao.CorFrenteGrafico = CorFrente;
        }
    }

    #endregion
}