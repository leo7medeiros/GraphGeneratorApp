using Mopups.Pages;
using Mopups.Services;
using GraphGeneratorApp.Core.Utilidades;

namespace GraphGeneratorApp.UI.Components;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomPopUpConfirmar : PopupPage
{
    #region PROPRIEDADES

    private Func<Task>? _okAsync = null;
    private Action? _ok = null;
    public Func<Task>? OKAsync
    {
        get => _okAsync;
        set => _okAsync = value;
    }
    public Action OK
    {
        get => _ok;
        set => _ok = value;
    }

    private Func<Task>? _cancelAsync = null;
    private Action? _cancel = null;
    public Func<Task>? CancelAsync
    {
        get => _cancelAsync;
        set => _cancelAsync = value;
    }
    public Action? Cancel
    {
        get => _cancel;
        set => _cancel = value;
    }

    #endregion

    public CustomPopUpConfirmar(string titulo, string descricao, string imagem, Delegate? okAction = null, Delegate cancelAction = null, bool alerta = false)
    {
        InitializeComponent();

        if (alerta)
        {
            lbltitulo.TextColor = UtilsHelper.GetColorFromResource("TEXTO_VERMELHO");
            lbltitulo.FontSize = 25;
            lblDescricao.TextColor = UtilsHelper.GetColorFromResource("TEXTO_DIMGRAY_COLOR");
        }

        if (string.IsNullOrEmpty(imagem))
        {
            img.IsVisible = false;
        }
        else
        {
            img.Icone = imagem;
        }

        lbltitulo.Text = titulo;
        lblDescricao.Text = descricao;

        // ATRIBUI AS AÇÕES OK E CANCEL, VERIFICANDO SE SÃO ASSÍNCRONAS OU SÍNCRONAS
        SetAction(okAction, ref _okAsync, ref _ok);
        SetAction(cancelAction, ref _cancelAsync, ref _cancel);
    }

    // MÉTODO AUXILIAR PARA DETERMINAR SE A AÇÃO É SÍNCRONA OU ASSÍNCRONA
    private void SetAction(Delegate? action, ref Func<Task>? asyncAction, ref Action? syncAction)
    {
        if (action is Func<Task> asyncFunc)
        {
            asyncAction = asyncFunc;
        }
        else if (action is Action syncAct)
        {
            syncAction = syncAct;
        }
        else if (action != null)
        {
            throw new ArgumentException("A ação fornecida deve ser do tipo Action ou Func<Task>.");
        }
    }

    public static async Task Alerta(
        string titulo,
        string descricao,
        string descricao2,
        string imagem,
        Delegate? okAction = null,
        Delegate? cancelAction = null,
        bool alerta = false)
    {
        var popup = new CustomPopUpConfirmar(titulo, descricao, imagem, okAction, cancelAction, alerta);
        await MopupService.Instance.PushAsync(popup);
    }

    private async void buttonCancel_Clicado(object sender, EventArgs e)
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                buttonCancel.NoClique = true;
                IsBusy = true;
                if (_cancelAsync != null)
                {
                    await _cancelAsync();
                }
                else
                {
                    _cancel?.Invoke();
                }
                await MopupService.Instance.PopAllAsync();
                IsBusy = false;
                buttonCancel.NoClique = false;
            });
        }
        catch (Exception exception)
        {

        }
    }

    private async void buttonOk_Clicado(object sender, EventArgs e)
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                buttonOk.NoClique = true;
                IsBusy = true;

                if (_okAsync != null)
                {
                    await _okAsync();
                }
                else
                {
                    _ok?.Invoke();
                }

                await MopupService.Instance.PopAllAsync();
                IsBusy = false;
                buttonOk.NoClique = false;
            });
        }
        catch (Exception exception)
        {

        }
    }
}