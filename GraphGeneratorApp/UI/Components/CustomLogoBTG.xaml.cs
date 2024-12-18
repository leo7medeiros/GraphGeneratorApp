using GraphGeneratorApp.UI.Views.BaseContent;

namespace GraphGeneratorApp.UI.Components;

public partial class CustomLogoBTG : BaseContentView
{
    public CustomLogoBTG()
    {
        InitializeComponent();
        BindingContext = this;
    }

    //PROPRIEDADES VINCULAVEIS

    #region LARGURA CUSTOMIZADA

    public static readonly BindableProperty LarguraCustomizadaProperty = BindableProperty.Create(
        propertyName: nameof(LarguraCustomizada),
        returnType: typeof(double),
        declaringType: typeof(CustomLogoBTG),
        defaultValue: 20.0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnCustomWidthRequestChanged);

    public double LarguraCustomizada
    {
        get => (double)GetValue(LarguraCustomizadaProperty);
        set => SetValue(LarguraCustomizadaProperty, value);
    }

    private static void OnCustomWidthRequestChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomLogoBTG)bindable;
        var newWidth = (double)newValue;

        control.UpdateLayoutForWidth(newWidth);
    }

    private void UpdateLayoutForWidth(double width)
    {
        RenderizadorSvg.LarguraCustomizada = width;
    }

    #endregion

    #region ALTURA CUSTOMIZADA

    public static readonly BindableProperty AlturaCustomizadaProperty = BindableProperty.Create(
        propertyName: nameof(AlturaCustomizada),
        returnType: typeof(double),
        declaringType: typeof(CustomLogoBTG),
        defaultValue: 20.0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnCustomHeightRequestChanged);

    public double AlturaCustomizada
    {
        get => (double)GetValue(AlturaCustomizadaProperty);
        set => SetValue(AlturaCustomizadaProperty, value);
    }
    private static void OnCustomHeightRequestChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomLogoBTG)bindable;
        var newHeight = (double)newValue;

        control.UpdateLayoutForHeight(newHeight);
    }

    private void UpdateLayoutForHeight(double height)
    {
        RenderizadorSvg.AlturaCustomizada = height;
    }

    #endregion

    #region COR

    public static readonly BindableProperty CorProperty = BindableProperty.Create(
        propertyName: nameof(Cor),
        returnType: typeof(Color),
        declaringType: typeof(CustomLogoBTG),
        defaultValue: default(Color),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnColorRequestChanged);

    public Color Cor
    {
        get => (Color)GetValue(CorProperty);
        set => SetValue(CorProperty, value);
    }

    private static void OnColorRequestChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CustomLogoBTG)bindable;
        var newColor = (Color)newValue;

        control.UpdateLayoutForColor(newColor);
    }

    private void UpdateLayoutForColor(Color color)
    {
        RenderizadorSvg.CorIcone = color;
    }

    #endregion
}