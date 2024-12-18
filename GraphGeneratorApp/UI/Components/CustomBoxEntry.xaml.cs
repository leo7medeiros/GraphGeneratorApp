using System.Windows.Input;

namespace GraphGeneratorApp.UI.Components;

public partial class CustomBoxEntry : Grid
{
    #region BINDABLE PROPERTIES

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CustomBoxEntry),
        default(string),
        BindingMode.TwoWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.customEntry.Text = (string)newValue;
        }
    );

    public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(CustomBoxEntry),
        default(string),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.placeholder.Text = (string)newValue;
        }
    );

    public static readonly BindableProperty HelperTextProperty = BindableProperty.Create(
        nameof(HelperText),
        typeof(string),
        typeof(CustomBoxEntry),
        default(string),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.helperText.Text = (string)newValue;

            if (view.errorText.IsVisible)
                view.helperText.IsVisible = false;
            else
                view.helperText.IsVisible = !string.IsNullOrEmpty(view.helperText.Text);
        }
    );

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(CustomBoxEntry),
        default(string),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.labelText.Text = (string)newValue;
            view.labelText.IsVisible = !string.IsNullOrWhiteSpace(view.labelText.Text);
        }
    );

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(CustomBoxEntry),
        default(string),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.errorText.Text = (string)newValue;
        }
    );

    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(
        nameof(LeadingIcon),
        typeof(ImageSource),
        typeof(CustomBoxEntry),
        default(ImageSource),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.leadingIcon.Source = (ImageSource)newValue;
            view.leadingIcon.IsVisible = !view.leadingIcon.Source.IsEmpty && view.ShowLeadingIcon;
        }
    );

    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(
        nameof(TrailingIcon),
        typeof(ImageSource),
        typeof(CustomBoxEntry),
        default(ImageSource),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.trailingIcon.Source = (ImageSource)newValue;
            view.trailingIcon.IsVisible = view.trailingIcon.Source != null && view.ShowTrailingIcon;
        }
    );

    public static readonly BindableProperty HasErrorProperty = BindableProperty.Create(
        nameof(HasError),
        typeof(bool),
        typeof(CustomBoxEntry),
        default(bool),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.errorText.IsVisible = (bool)newValue;
            view.containerBorder.Stroke = view.errorText.IsVisible ? (Color)Application.Current.Resources["BORDA_VERMELHO"] : (Color)Application.Current.Resources["BORDA_CINZA"];
            view.helperText.IsVisible = !view.errorText.IsVisible;

            if (view.TrailingIcon != null && !view.TrailingIcon.IsEmpty)
                view.tempIcon = view.TrailingIcon;

            view.TrailingIcon = view.errorText.IsVisible
                ? ImageSource.FromFile("ic_error.png")
                : view.tempIcon;

            view.trailingIcon.IsVisible = view.errorText.IsVisible;
        }
    );

    public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(
        nameof(IsTextPredictionEnabled),
        typeof(bool),
        typeof(CustomBoxEntry),
        true,
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.customEntry.IsTextPredictionEnabled = (bool)newValue;
        }
    );

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        nameof(IsPassword),
        typeof(bool),
        typeof(CustomBoxEntry),
        default(bool),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.customEntry.IsPassword = (bool)newValue;
            view.passwordIcon.IsVisible = (bool)newValue;
        }
    );

    public static readonly BindableProperty ShowLeadingIconProperty = BindableProperty.Create(
        nameof(ShowLeadingIcon),
        typeof(bool),
        typeof(CustomBoxEntry),
        default(bool),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;

            if (view.LeadingIcon != null && !view.LeadingIcon.IsEmpty)
                view.leadingIcon.IsVisible = (bool)newValue;
        }
    );

    public static readonly BindableProperty ShowTrailingIconProperty = BindableProperty.Create(
        nameof(ShowTrailingIcon),
        typeof(bool),
        typeof(CustomBoxEntry),
        default(bool),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;

            if (view.TrailingIcon != null && !view.TrailingIcon.IsEmpty)
                view.trailingIcon.IsVisible = (bool)newValue;
        }
    );

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
        nameof(MaxLength),
        typeof(int),
        typeof(CustomBoxEntry),
        default(int),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.customEntry.MaxLength = (int)newValue;
            view.charCounterText.IsVisible = view.customEntry.MaxLength > 0;
            view.charCounterText.Text = $"0 / {view.MaxLength}";
        }
    );

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(CustomBoxEntry),
        Color.FromHex("#0EA9DF"),
        BindingMode.OneWay
    );

    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
        nameof(ReturnCommand),
        typeof(ICommand),
        typeof(CustomBoxEntry),
        default(ICommand),
        BindingMode.OneWay,
        null,
        (bindable, oldValue, newValue) =>
        {
            var view = (CustomBoxEntry)bindable;
            view.customEntry.ReturnCommand = (ICommand)newValue;
        }
    );

    #endregion

    #region PROPERTIES

    private ImageSource tempIcon;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public string HelperText
    {
        get => (string)GetValue(HelperTextProperty);
        set => SetValue(HelperTextProperty, value);
    }
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public ImageSource LeadingIcon
    {
        get => (ImageSource)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    public ImageSource TrailingIcon
    {
        get => (ImageSource)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    public bool IsTextPredictionEnabled
    {
        get => (bool)GetValue(IsTextPredictionEnabledProperty);
        set => SetValue(IsTextPredictionEnabledProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public bool ShowLeadingIcon
    {
        get => (bool)GetValue(ShowLeadingIconProperty);
        set => SetValue(ShowLeadingIconProperty, value);
    }

    public bool ShowTrailingIcon
    {
        get => (bool)GetValue(ShowTrailingIconProperty);
        set => SetValue(ShowTrailingIconProperty, value);
    }

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public Keyboard Keyboard
    {
        set => this.customEntry.Keyboard = value;
    }

    public ReturnType ReturnType
    {
        set => this.customEntry.ReturnType = value;
    }

    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }

    #endregion

    public CustomBoxEntry()
    {
        InitializeComponent();
        this.customEntry.Text = this.Text;
        this.customEntry.TextChanged += this.OnCustomEntryTextChanged;
        this.customEntry.Completed += this.OnCustomEntryCompleted;
        this.labelText.PropertyChanged += LabelText_PropertyChanged;
    }

    #region METHODS

    // EVENT HANDLERS
    public event EventHandler<EventArgs> EntryCompleted;
    public event EventHandler<TextChangedEventArgs> TextChanged;

    private void LabelText_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        ChangePlaceholderMargin();
    }

    // AQUI VERIFICAMOS SE HÁ ALGUM TEXTO NA ENTRADA,
    // SE NÃO, DEFINIMOS A COR DA BORDA E DO TEXTO DO MARCADOR DE POSIÇÃO
    // E ATIVAMOS A ANIMAÇÃO PARA MOVER O MARCADOR DE POSIÇÃO PARA CIMA
    private async Task ControlFocused()
    {
        if (string.IsNullOrEmpty(this.customEntry.Text) || this.customEntry.Text.Length > 0)
        {
            this.customEntry.Focus();
            this.containerBorder.Stroke = this.HasError ? (Color)Application.Current.Resources["BORDA_VERMELHO"] : (Color)Application.Current.Resources["BORDA_CINZA"];
            this.placeholder.TextColor = this.HasError ? (Color)Application.Current.Resources["TEXTO_VERMELHO"] : (Color)Application.Current.Resources["TEXTO_CINZA"];
            this.placeholderContainer.IsVisible = !string.IsNullOrWhiteSpace(this.placeholder.Text);

            int y = -25;
            await this.placeholderContainer.TranslateTo(0, y, 100, Easing.Linear);
            ChangePlaceholderMargin();

            this.placeholderContainer.HorizontalOptions = LayoutOptions.Start;
            this.placeholder.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
        }
        else
        {
            await this.ControlUnfocused();
        }
    }

    // AQUI, MUDAMOS A COR DA BORDA E DO TEXTO DO MARCADOR DE POSIÇÃO
    // VOLTA AO NORMAL E VERIFICA SE HÁ ALGUM TEXTO NA ENTRADA,
    // SE NÃO, LANÇAMOS A ANIMAÇÃO PARA POSICIONAR O MARCADOR DE POSIÇÃO DE VOLTA SOBRE A ENTRADA
    private async Task ControlUnfocused()
    {
        this.containerBorder.Stroke = this.HasError ? (Color)Application.Current.Resources["BORDA_VERMELHO"] : (Color)Application.Current.Resources["BORDA_CINZA"];
        this.placeholder.TextColor = this.HasError ? (Color)Application.Current.Resources["TEXTO_VERMELHO"] : (Color)Application.Current.Resources["TEXTO_CINZA"];
        this.customEntry.Unfocus();

        if (string.IsNullOrEmpty(this.customEntry.Text) || this.customEntry.MaxLength <= 0)
        {
            await this.placeholderContainer.TranslateTo(0, 0, 100, Easing.Linear);
            ChangePlaceholderMargin();
            this.placeholderContainer.HorizontalOptions = LayoutOptions.Start;
            this.placeholder.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
        }
    }

    private void ChangePlaceholderMargin()
    {
        this.placeholderContainer.Margin = leadingIcon.IsVisible && !string.IsNullOrWhiteSpace(this.labelText.Text) ? new Thickness(50, 28, 56, 0)
                                             : leadingIcon.IsVisible && string.IsNullOrWhiteSpace(this.labelText.Text) ? new Thickness(50, 0, 56, 0)
                                             : !leadingIcon.IsVisible && !string.IsNullOrWhiteSpace(this.labelText.Text) ? new Thickness(15, 28, 60, 0)
                                             : new Thickness(15, 0, 60, 0);
    }

    private void CustomEntryFocused(object sender, FocusEventArgs e)
    {
        if (e.IsFocused)
            MainThread.BeginInvokeOnMainThread(async () => await this.ControlFocused());
    }

    private void CustomEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (!e.IsFocused)
            MainThread.BeginInvokeOnMainThread(async () => await this.ControlUnfocused());

        HideKeyboard();
    }

    private void CustomBoxEntryTapped(object sender, TappedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () => await this.ControlFocused());
    }

    // AQUI, MUDAMOS O TIPO DE SENHA DA ENTRADA
    // PARA MUDAR O ÍCONE DO OLHO
    private void PasswordEyeTapped(object sender, EventArgs e)
    {
        this.customEntry.IsPassword = !this.customEntry.IsPassword;
    }

    // AQUI, DEFINIMOS O TEXTO A CADA NOVO CARACTERE
    // E ATUALIZAMOS A LABEL charCounter
    private void OnCustomEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        this.Text = e.NewTextValue;

        if (this.charCounterText.IsVisible)
            this.charCounterText.Text = $"{this.customEntry.Text.Length} / {this.MaxLength}";

        this.TextChanged?.Invoke(this, e);
    }

    private void OnCustomEntryCompleted(object sender, EventArgs e)
    {
        this.EntryCompleted?.Invoke(this, EventArgs.Empty);

        HideKeyboard();
    }

    private void HideKeyboard()
    {
        // Método específico de plataforma para esconder o teclado
#if ANDROID
        var context = Android.App.Application.Context;
        var inputMethodManager = (Android.Views.InputMethods.InputMethodManager)context.GetSystemService(Android.Content.Context.InputMethodService);
        var currentFocus = Platform.CurrentActivity?.CurrentFocus;
        if (currentFocus != null)
        {
            inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, Android.Views.InputMethods.HideSoftInputFlags.None);
        }
#endif
    }

    #endregion

    private void PasswordEyeTapped(object sender, TappedEventArgs e)
    {
        this.customEntry.IsPassword = !this.customEntry.IsPassword;
    }
}