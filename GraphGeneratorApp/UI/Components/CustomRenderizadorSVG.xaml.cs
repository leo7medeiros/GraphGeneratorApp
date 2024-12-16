using GraphGeneratorApp.Core.Utilidades;
using GraphGeneratorApp.UI.Views.BaseContent;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace GraphGeneratorApp.UI.Components;

public partial class CustomRenderizadorSVG : BaseContentView
{
    public CustomRenderizadorSVG()
    {
        InitializeComponent();
        BindingContext = this;
        MudarCor(CorIcone);
        CanvasView.PaintSurface += OnPaintSurface;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
    }

    #region PROPRIEDADES VINCULAVEIS

    #region CorIcone

    public static readonly BindableProperty CorIconeProperty = BindableProperty.Create(
        propertyName: nameof(CorIcone),
        returnType: typeof(Color),
        declaringType: typeof(CustomRenderizadorSVG),
        defaultValue: default(Color),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnColorPropertyChanged);

    public Color CorIcone
    {
        get => (Color)GetValue(CorIconeProperty);
        set => SetValue(CorIconeProperty, value);

    }

    private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var renderer = bindable as CustomRenderizadorSVG;
        if (renderer != null && newValue is Color cor)
        {
            // Muda a cor do ícone conforme a nova cor
            renderer.MudarCor(cor);
        }
    }
    private void MudarCor(Color cor)
    {
        CanvasView.InvalidateSurface(); // Redesenha o Canvas para aplicar a nova cor
    }

    #endregion

    #region LarguraCustomizada

    public static readonly BindableProperty LarguraCustomizadaProperty = BindableProperty.Create(
        propertyName: nameof(LarguraCustomizada),
        returnType: typeof(double),
        declaringType: typeof(CustomRenderizadorSVG),
        defaultValue: 20.0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnCustomWidthChanged);

    public double LarguraCustomizada
    {
        get => (double)GetValue(LarguraCustomizadaProperty);
        set => SetValue(LarguraCustomizadaProperty, value);
    }

    private static void OnCustomWidthChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomRenderizadorSVG)bindable;
        var newWidth = (double)newValue;

        control.UpdateLayoutForWidth(newWidth);
    }

    private void UpdateLayoutForWidth(double width)
    {
        CanvasView.WidthRequest = width;
        CanvasView.InvalidateSurface();
    }

    #endregion

    #region AlturaCustomizada

    public static readonly BindableProperty AlturaCustomizadaProperty = BindableProperty.Create(
        propertyName: nameof(AlturaCustomizada),
        returnType: typeof(double),
        declaringType: typeof(CustomRenderizadorSVG),
        defaultValue: 20.0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnCustomHeightChanged);

    public double AlturaCustomizada
    {
        get => (double)GetValue(AlturaCustomizadaProperty);
        set => SetValue(AlturaCustomizadaProperty, value);
    }

    private static void OnCustomHeightChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomRenderizadorSVG)bindable;
        var newHeight = (double)newValue;

        control.UpdateLayoutForHeight(newHeight);
    }

    private void UpdateLayoutForHeight(double height)
    {
        CanvasView.HeightRequest = height;
        CanvasView.InvalidateSurface();
    }

    #endregion

    #region Icone

    private SKSvg _loadedSvg; // Este campo armazena o SVG carregado

    public static readonly BindableProperty IconeProperty = BindableProperty.Create(
        propertyName: nameof(Icone),
        returnType: typeof(string),
        declaringType: typeof(CustomRenderizadorSVG),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnSourcePropertyChanged);

    public string Icone
    {
        get => (string)GetValue(IconeProperty);
        set => SetValue(IconeProperty, value);
    }

    private static void OnSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var renderer = bindable as CustomRenderizadorSVG;
        if (renderer != null && newValue is string newSvgPath)
        {
            renderer.CarregarSvgERedesenhar(newSvgPath);
        }
    }

    #endregion

    #region ExecutaEfeitoClique

    private bool _clicked = false;

    public event Action TriggerEffect;
    public async void ExecutaEfeitoClique(Color color, int duration)
    {
        if (TriggerEffect != null)
        {
            TriggerEffect.Invoke();
        }

        if (!_clicked)
        {
            _clicked = true;

            // Armazena a cor atual antes de aplicar o efeito
            var corOriginal = CorIcone;

            CorIcone = color;
            CanvasView.InvalidateSurface(); // Redesenha a superfície

            await Task.Delay(duration); // Duração do efeito visual

            // Restaura a cor original após o efeito
            CorIcone = corOriginal;
            CanvasView.InvalidateSurface(); // Redesenha a superfície para normal

            _clicked = false;
        }
    }

    #endregion

    #region Proporcao

    public static readonly BindableProperty ProporcaoProperty = BindableProperty.Create(
        propertyName: nameof(Proporcao),
        returnType: typeof(bool),
        declaringType: typeof(CustomRenderizadorSVG),
        defaultBindingMode: BindingMode.TwoWay);

    public bool Proporcao
    {
        get => (bool)GetValue(ProporcaoProperty);
        set => SetValue(ProporcaoProperty, value);
    }

    #endregion

    #endregion

    #region METODOS DE RENDERIZAÇÃO

    private void CarregarSvgERedesenhar(string svgPath)
    {
        var svg = UtilsHelper.LoadSvg(svgPath);
        _loadedSvg = svg;
        CanvasView.InvalidateSurface();
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var surface = e.Surface;
        var canvas = surface.Canvas;

        canvas.Clear(SKColors.Transparent);

        if (_loadedSvg is not null)
        {
            var svg = _loadedSvg;

            float svgWidth = svg.Picture.CullRect.Width;
            float svgHeight = svg.Picture.CullRect.Height;

            float canvasWidth = e.Info.Width;
            float canvasHeight = e.Info.Height;

            float scaleX = canvasWidth / svgWidth;
            float scaleY = canvasHeight / svgHeight;
            SKMatrix matrix;

            if (Proporcao)
            {
                float scale = Math.Min(scaleX, scaleY);

                svgWidth *= scale;
                svgHeight *= scale;

                float xTranslate = (canvasWidth - svgWidth) / 2;
                float yTranslate = (canvasHeight - svgHeight) / 2;

                matrix = SKMatrix.CreateScale(scale, scale);
                matrix = matrix.PostConcat(SKMatrix.CreateTranslation(xTranslate, yTranslate));
            }
            else
            {
                svgWidth *= scaleX;
                svgHeight *= scaleY;

                float xTranslate = (canvasWidth - svgWidth) / 2;
                float yTranslate = (canvasHeight - svgHeight) / 2;

                matrix = SKMatrix.CreateScale(scaleX, scaleY);
                matrix = matrix.PostConcat(SKMatrix.CreateTranslation(xTranslate, yTranslate));
            }

            using var paint = new SKPaint();
            if (CorIcone != default(Color))
            {
                if (IsEnabled || _clicked)
                {
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(UtilsHelper.ToSKColor(CorIcone), SKBlendMode.SrcIn);
                }
                else
                {
                    paint.ColorFilter = SKColorFilter.CreateBlendMode(UtilsHelper.ToSKColor(UtilsHelper.GetColorFromResource("GRAYDARK_DISABLE_COLOR")), SKBlendMode.SrcIn);
                }
            }
            canvas.DrawPicture(svg.Picture, ref matrix, paint);
        }
    }

    #endregion
}