using Mopups.Services;
using Mopups.Pages;
using GraphGeneratorApp.Data.Enums;

namespace GraphGeneratorApp.UI.Components;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomPopUpMensagem : PopupPage
{
    #region PROPERTIES

    private string _titulo;
    public string Titulo
    {
        get { return _titulo; }
        private set { _titulo = value; OnPropertyChanged(nameof(Titulo)); }
    }

    private string _mensagem;
    public string Mensagem
    {
        get { return _mensagem; }
        private set { _mensagem = value; OnPropertyChanged(nameof(Mensagem)); }
    }

    private string _icone;
    public string Icone
    {
        get { return _icone; }
        private set { _icone = value; OnPropertyChanged(nameof(Icone)); }
    }

    private Color _corMensagem;
    public Color CorMensagem
    {
        get { return _corMensagem; }
        private set { _corMensagem = value; OnPropertyChanged(nameof(CorMensagem)); }
    }

    #endregion

    public CustomPopUpMensagem(string titulo, string mensagem, Tipos.TipoNotificacao tipo)
    {
        BindingContext = this;
        InitializeComponent();
        ConfiguraMensagem(titulo, mensagem, tipo);
    }

    private void ConfiguraMensagem(string titulo, string mensagem, Tipos.TipoNotificacao tipo)
    {
        Titulo = titulo;
        Mensagem = mensagem;
        CorMensagem = tipo == Tipos.TipoNotificacao.Erro ? (Color)Application.Current.Resources["RED_COLOR_ALERT"] : tipo == Tipos.TipoNotificacao.Aviso ? (Color)Application.Current.Resources["YELLOW_COLOR_ALERT"] : tipo == Tipos.TipoNotificacao.Informacao ? (Color)Application.Current.Resources["AZUL_ESCURO_PRIMARIO"] : (Color)Application.Current.Resources["TEXTO_PRETO"];

        if (tipo != Tipos.TipoNotificacao.Nenhum)
        {
            iconeSVG.IsVisible = true;
            iconeSVG.Icone = tipo == Tipos.TipoNotificacao.Erro ? "icone_erro.svg" : tipo == Tipos.TipoNotificacao.Aviso ? "icone_aviso.svg" : tipo == Tipos.TipoNotificacao.Informacao ? "icone_info.svg" : string.Empty;
            iconeSVG.CorIcone = CorMensagem;
        }
        else
        {
            iconeSVG.IsVisible = false;
        }
    }

    public static async Task Alerta(string titulo, string mensagem, Tipos.TipoNotificacao tipo)
    {
        await MopupService.Instance.PushAsync(new CustomPopUpMensagem(titulo, mensagem, tipo), true);
    }

    private async void ButtonOk_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}