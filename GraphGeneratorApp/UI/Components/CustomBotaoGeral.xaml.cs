using System.Diagnostics;
using System.Runtime.CompilerServices;
using GraphGeneratorApp.Core.Utilidades;
using GraphGeneratorApp.UI.Views.BaseContent;

namespace GraphGeneratorApp.UI.Components
{
    public partial class CustomBotaoGeral : BaseContentView
    {
        //VARIÁVEIS INICIAIS PARA CONTROLE DOS EFEITOS
        private Color _backgroundColorBkp;
        private Color _corTextoBkp;
        private Color _backgroundColor;
        private Color _corTexto;

        //CONFIGURAÇÕES DE EFEITOS
        private float _coefClareamento = 0.3f;
        private float _coefOpacidade = 0.1f;
        private int _duracao = 150;

        public CustomBotaoGeral()
        {
            InitializeComponent();
            BindingContext = this;

            // FORÇA ATUALIZAÇÃO PARA OS VALORES DEFAULT
            MudarCorTexto(CorTexto);
            MudarCorFundo(CorFundo);
            MudarLargura(Largura);
            MudarAltura(Altura);
            UpdateLayoutForPosicaoIcone(PosicaoIcone);
            MudarEspacamento(EspacamentoIconeTexto);
            MudarMargem(Margem);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            //SALVA CORES INICIAIS PARA USOS FUTUROS
            _corTextoBkp = CorTexto;
            _backgroundColorBkp = borda.BackgroundColor;
            _backgroundColor = borda.BackgroundColor;
            _corTexto = CorTexto;

            //INVERTE AS CORES CASO O ESTILO FOR OUTLINED
            if (Outlined)
            {
                _corTexto = _backgroundColor;
                _backgroundColor = Colors.Transparent;

                InternalLabel.TextColor = _corTexto;
                borda.Stroke = _corTexto;
                borda.BackgroundColor = _backgroundColor;
            }

            //SE A COR DO ICONE NÃO FOI DEFINIDA, A DEFINE COM A MESMA DO TEXTO
            CorIcone ??= _corTexto;

            //APLICA O EFEITO DO DISABLE, CASO O BOTÃO JA FOR CONSTRUIDO DISABILITADO
            if (!IsEnabled)
            {
                HandleIsEnabledChanged();
            }
        }

        #region PROPRIEDADES VINCULAVEIS

        #region LARGURA

        public double Largura
        {
            get => (double)GetValue(LarguraProperty);
            set => SetValue(LarguraProperty, value);
        }

        public static readonly BindableProperty LarguraProperty = BindableProperty.Create(
            nameof(Largura),
            typeof(double),
            typeof(CustomBotaoGeral),
            defaultValue: null,
            propertyChanged: OnLarguraPropertyChanged);

        private static void OnLarguraPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomBotaoGeral customBotao && newValue is double largura)
            {
                customBotao.MudarLargura(largura);
            }
        }
        private void MudarLargura(double largura)
        {
            if (largura > 0)
            {
                borda.WidthRequest = largura;
                baseContentView.WidthRequest = largura;
            }
        }

        #endregion

        #region ALTURA

        public double Altura
        {
            get => (double)GetValue(AlturaProperty);
            set => SetValue(AlturaProperty, value);
        }

        public static readonly BindableProperty AlturaProperty = BindableProperty.Create(
            nameof(Altura),
            typeof(double),
            typeof(CustomBotaoGeral),
            defaultValue: null,
            propertyChanged: OnAlturaPropertyChanged);

        private static void OnAlturaPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomBotaoGeral customBotao && newValue is double altura)
            {
                customBotao.MudarAltura(altura);
            }
        }

        private void MudarAltura(double altura)
        {
            if (altura > 0)
            {
                borda.HeightRequest = altura;
                baseContentView.HeightRequest = altura;
            }
        }

        #endregion

        #region COR FUNDO

        public Color CorFundo
        {
            get => (Color)GetValue(CorFundoProperty);
            set => SetValue(CorFundoProperty, value);
        }

        public static readonly BindableProperty CorFundoProperty = BindableProperty.Create(
            nameof(CorFundo),
            typeof(Color),
            typeof(CustomBotaoGeral),
            defaultValue: Colors.Transparent,
            propertyChanged: OnCorFundoPropertyChanged);

        private static void OnCorFundoPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;
            if (customButton != null && newValue is Color corFundo)
            {
                customButton.MudarCorFundo(corFundo);
            }
        }

        private void MudarCorFundo(Color? cor)
        {
            _backgroundColor = cor;
            _backgroundColorBkp = cor;
            borda.BackgroundColor = cor;
        }

        #endregion

        #region OUTLINED

        public bool Outlined
        {
            get => (bool)GetValue(OutlinedProperty);
            set => SetValue(OutlinedProperty, value);
        }

        public static readonly BindableProperty OutlinedProperty = BindableProperty.Create(
            nameof(Outlined),
            typeof(bool),
            typeof(CustomBotaoGeral),
            defaultValue: false,
            propertyChanged: OnOutlinedPropertyChanged);

        private static void OnOutlinedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;

            if (customButton != null && newValue is bool outlined)
            {
                customButton.MudarEstilo(outlined);
            }
        }

        private void MudarEstilo(bool outlined)
        {

            if (outlined)
            {
                //INVERTE AS CORES DO BACKGORUND E DO TEXTO DAS VARIAVEIS LOCAIS
                _corTexto = borda.BackgroundColor;
                _backgroundColor = Colors.Transparent;

                borda.Stroke = _corTexto;
            }
            else
            {
                //VOLTA AS CORES PARA O DEFAULT
                _backgroundColor = _backgroundColorBkp;
                _corTexto = _corTextoBkp;

                borda.Stroke = Colors.Transparent;
            }

            borda.BackgroundColor = _backgroundColor;
            InternalLabel.TextColor = _corTexto;
        }

        #endregion

        #region TEXTO

        public string Texto
        {
            get => (string)GetValue(TextoProperty);
            set => SetValue(TextoProperty, value);
        }

        public static readonly BindableProperty TextoProperty = BindableProperty.Create(
            nameof(Texto),
            typeof(string),
            typeof(CustomBotaoGeral),
            defaultValue: null,
            propertyChanged: OnTextoPropertyChanged);

        private static void OnTextoPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;
            if (customButton != null && newValue is string texto)
            {
                customButton.InternalLabel.Text = texto;
            }
        }

        #endregion

        #region COR TEXTO

        public Color? CorTexto
        {
            get => (Color)GetValue(CorTextoProperty);
            set => SetValue(CorTextoProperty, value);
        }

        public static readonly BindableProperty CorTextoProperty = BindableProperty.Create(
            nameof(CorTexto),
            typeof(Color),
            typeof(CustomBotaoGeral),
            defaultValue: Colors.Transparent,
            propertyChanged: OnColorTextPropertyChanged);

        private static void OnColorTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;
            if (customButton != null && newValue is Color corTexto)
            {
                customButton.MudarCorTexto(corTexto);
            }
        }

        private void MudarCorTexto(Color? cor)
        {
            _corTexto = cor;
            _corTextoBkp = cor;
            InternalLabel.TextColor = cor;
        }

        #endregion

        #region COR ICONE

        public Color? CorIcone
        {
            get => (Color)GetValue(CorIconeProperty);
            set => SetValue(CorIconeProperty, value);
        }

        public static readonly BindableProperty CorIconeProperty = BindableProperty.Create(
            nameof(CorIcone),
            typeof(Color),
            typeof(CustomBotaoGeral),
            defaultValue: Colors.Transparent,
            propertyChanged: OnColorPropertyChanged);

        private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;
            if (customButton != null && newValue is Color IconColor)
            {
                customButton.InternalIconLeft.CorIcone = IconColor;
                customButton.InternalIconRight.CorIcone = IconColor;
            }
        }

        #endregion

        #region ICONE

        public string Icone
        {
            get => (string)GetValue(IconeProperty);
            set => SetValue(IconeProperty, value);
        }

        public static readonly BindableProperty IconeProperty = BindableProperty.Create(
            nameof(Icone),
            typeof(string),
            typeof(CustomBotaoGeral),
            defaultValue: null,
            propertyChanged: OnIconePropertyChanged);

        private static void OnIconePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;

            if (customButton != null && newValue is string IconPath)
            {
                customButton.InternalIconRight.Icone = IconPath;
                customButton.InternalIconLeft.Icone = IconPath;
            }
        }

        #endregion

        #region ESPAÇAMENTO ICONE TEXTO

        public double EspacamentoIconeTexto
        {
            get => (double)GetValue(EspacamentoIconeTextoProperty);
            set => SetValue(EspacamentoIconeTextoProperty, value);
        }

        public static readonly BindableProperty EspacamentoIconeTextoProperty = BindableProperty.Create(
            nameof(EspacamentoIconeTexto),
            typeof(double),
            typeof(CustomBotaoGeral),
            defaultValue: 10.0,
            propertyChanged: OnEspacamentoIconeTextoPropertyChanged);

        private static void OnEspacamentoIconeTextoPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomBotaoGeral customButton && newValue is double espacamento)
            {
                customButton.MudarEspacamento(espacamento);
            }
        }
        private void MudarEspacamento(double espacamento)
        {
            InternalGrid.Spacing = espacamento;
        }

        #endregion

        #region MARGEM
        public double Margem
        {
            get => (double)GetValue(MargemProperty);
            set => SetValue(MargemProperty, value);
        }

        public static readonly BindableProperty MargemProperty = BindableProperty.Create(
            nameof(Margem),
            typeof(double),
            typeof(CustomBotaoGeral),
            defaultValue: 5d,
            propertyChanged: OnMargemPropertyChanged);

        private static void OnMargemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomBotaoGeral customButton && newValue is double margem)
            {
                customButton.MudarMargem(margem);
            }
        }

        private void MudarMargem(double margem)
        {
            InternalGrid.Padding = new Thickness(margem, 0);
        }

        #endregion

        #region POSICÃO ICONE

        public string PosicaoIcone
        {
            get => (string)GetValue(PosicaoIconeProperty);
            set => SetValue(PosicaoIconeProperty, value);
        }

        public static readonly BindableProperty PosicaoIconeProperty = BindableProperty.Create(
            nameof(PosicaoIcone),
            typeof(string),
            typeof(CustomBotaoGeral),
            defaultValue: "null",
            propertyChanged: OnPosicaoIconePropertyChanged);

        private static void OnPosicaoIconePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customButton = bindable as CustomBotaoGeral;
            var newPosicao = newValue as string;

            customButton?.UpdateLayoutForPosicaoIcone(newPosicao);
        }

        private void UpdateLayoutForPosicaoIcone(string? posicao)
        {
            switch (posicao)
            {
                case "Esquerda":
                    InternalIconLeft.IsVisible = true;
                    InternalIconLeft.WidthRequest = 25;
                    InternalIconRight.IsVisible = false;
                    InternalIconRight.WidthRequest = 0;
                    InternalLabel.Padding = new Thickness(0, 0, Margem, 0);
                    break;
                case "Direita":
                    InternalIconLeft.IsVisible = false;
                    InternalIconLeft.WidthRequest = 0;
                    InternalIconRight.IsVisible = true;
                    InternalIconRight.WidthRequest = 25;
                    InternalLabel.Padding = new Thickness(Margem, 0, 0, 0);
                    break;
                default:
                    InternalIconLeft.IsVisible = false;
                    InternalIconRight.IsVisible = false;
                    InternalIconRight.WidthRequest = 0;
                    InternalIconLeft.WidthRequest = 0;
                    InternalLabel.Padding = new Thickness(Margem, 0, Margem, 0);
                    break;
            }
        }

        #endregion

        #region NOCLIQUE

        public static readonly BindableProperty NoCliqueProperty = BindableProperty.Create(
            nameof(NoClique),
            typeof(bool),
            typeof(CustomBotaoGeral),
            defaultValue: false);

        public bool NoClique
        {
            get => (bool)GetValue(NoCliqueProperty);
            set => SetValue(NoCliqueProperty, value);
        }

        #endregion

        #region CLICADO

        private EventHandler? _clicado;
        public event EventHandler? Clicado
        {
            add => _clicado += value;
            remove => _clicado -= value;
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (!NoClique && IsEnabled)
            {
                try
                {
                    // Bloqueia o botão para evitar duplos cliques
                    NoClique = true;

                    // Executa o efeito visual
                    OnClicked();

                    // Aguarda o evento de clique (caso seja assíncrono)
                    if (_clicado != null)
                    {
                        await Task.Run(() => _clicado.Invoke(this, e));
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Erro ao executar clique: {ex.Message}");
                }
                finally
                {
                    // Libera o botão após a conclusão do processo
                    NoClique = false;
                }
            }
        }

        #endregion

        #endregion

        #region EFEITOS 

        private async void OnClicked()
        {

            if (!NoClique)
            {
                NoClique = true;

                var shadow = new Shadow()
                {
                    Radius = 10,
                    Brush = _backgroundColorBkp,
                    Offset = new Point(5, 5),
                    Opacity = 0.5f
                };

                var noShadow = new Shadow()
                {
                    Radius = 0,
                    Brush = Colors.Transparent,
                    Opacity = 0,
                    Offset = new Point(0, 0)

                };

                if (Outlined)
                {
                    borda.BackgroundColor = UtilsHelper.Alpha(_backgroundColorBkp, _coefOpacidade);
                    borda.Shadow = shadow;
                    await Task.Delay(_duracao);
                    borda.BackgroundColor = Colors.Transparent;
                    borda.Shadow = noShadow;
                }
                else if (_backgroundColor.Equals(Colors.Transparent))
                {
                    borda.BackgroundColor = UtilsHelper.Alpha(_corTexto, _coefOpacidade);
                    borda.Shadow = shadow;
                    await Task.Delay(_duracao);
                    borda.BackgroundColor = _backgroundColorBkp;
                    borda.Shadow = noShadow;
                }
                else
                {
                    borda.BackgroundColor = UtilsHelper.Lighten(_backgroundColor, _coefClareamento);
                    borda.Shadow = shadow;
                    await Task.Delay(_duracao);
                    borda.BackgroundColor = _backgroundColorBkp;
                    borda.Shadow = noShadow;
                }

                NoClique = false;
            }
        }

        #endregion

        #region APONTADORES CUSTOMIZADOS DE EVENTOS

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
            {
                HandleIsEnabledChanged();
            }
        }

        private void HandleIsEnabledChanged()
        {
            InternalIconLeft.IsEnabled = IsEnabled;
            InternalIconRight.IsEnabled = IsEnabled;

            if (IsEnabled)
            {
                InternalLabel.TextColor = _corTexto;
                borda.Stroke = Outlined ? _corTexto : Colors.Transparent;
                borda.BackgroundColor = Outlined ? Colors.Transparent : _backgroundColor;
            }
            else
            {
                InternalLabel.TextColor = UtilsHelper.GetColorFromResource("GRAYDARK_DISABLE_COLOR");
                borda.Stroke = Outlined ? UtilsHelper.GetColorFromResource("GRAYDARK_DISABLE_COLOR") : Colors.Transparent;
                borda.BackgroundColor = Outlined ? Colors.Transparent : UtilsHelper.GetColorFromResource("GRAY_DISABLE_COLOR");
            }
        }

        #endregion
    }
}