using GraphGeneratorApp.Provedores;
using System.Runtime.CompilerServices;

namespace GraphGeneratorApp.UI.Views.BaseContent;

public partial class BaseContentView : ContentView
{
    private ContentPage _parentPage;
    private bool _registered;

    public BaseContentView()
    {
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }

    #region GESTÃO DO CICLO DE VIDA DA PÁGINA

    protected virtual void OnAppearing()
    {

    }

    protected virtual void OnDisappearing()
    {

    }

    private void Register()
    {
        if (_parentPage != null && !_registered)
        {
            _parentPage.Appearing += OnAppearing;
            _parentPage.Disappearing += OnDisappearing;
            _registered = true;
        }
    }

    private void Unregister()
    {
        if (_parentPage != null && _registered)
        {
            _parentPage.Appearing -= OnAppearing;
            _parentPage.Disappearing -= OnDisappearing;
            _registered = false;
            _parentPage = null;
        }
    }

    #endregion

    #region EVENTOS DO CICLO DE VIDA

    private void OnAppearing(object sender, EventArgs e)
    {
        OnAppearing();
        DetectOrientation(DeviceDisplay.MainDisplayInfo);
    }

    private void OnDisappearing(object sender, EventArgs e)
    {
        OnDisappearing();
        DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
    }

    #endregion

    #region AJUSTE DE LAYOUT E ORIENTAÇÃO

    protected override void OnSizeAllocated(double largura, double altura)
    {
        base.OnSizeAllocated(largura, altura);
        if (largura > 0)
        {
            DetectOrientation(DeviceDisplay.MainDisplayInfo, largura);
        }
    }

    private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        DetectOrientation(e.DisplayInfo);
    }

    private void DetectOrientation(DisplayInfo displayInfo, double? largura = null)
    {
        var orientation = displayInfo.Orientation;
        if (orientation == DisplayOrientation.Landscape)
        {
            SetLayoutHorizontal(largura);
        }
        else if (orientation == DisplayOrientation.Portrait)
        {
            SetLayoutVertical(largura);
        }
    }

    protected virtual void SetLayoutHorizontal(double? largura)
    {

    }

    protected virtual void SetLayoutVertical(double? largura)
    {

    }

    #endregion

    #region GESTÃO DO RENDERER E PROPRIEDADES

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName.Equals("Renderer", StringComparison.OrdinalIgnoreCase))
        {
            var rr = DependencyService.Get<IRendererResolver>();

            if (rr != null)
            {
                if (rr.HasRenderer(this))
                {
                    Unregister();

                    var parent = Parent;
                    while (parent?.Parent != null && !(parent is ContentPage))
                    {
                        parent = parent.Parent;
                    }

                    if (parent is ContentPage page)
                    {
                        _parentPage = page;
                        Register();
                    }
                }
                else
                {
                    Unregister();
                }
            }
        }
    }

    #endregion
}