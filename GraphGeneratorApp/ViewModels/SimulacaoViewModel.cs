using GraphGeneratorApp.Data.Enums;
using GraphGeneratorApp.Models;
using GraphGeneratorApp.UI.Components;
using GraphGeneratorApp.UI.Views.BaseContent;
using Microcharts;
using Mopups.Services;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GraphGeneratorApp.ViewModels
{
    public class SimulacaoViewModel : BaseViewModel
    {
        #region PROPERTIES

        private ObservableCollection<GraficoViewModel> _listaGraficoView = [];
        public ObservableCollection<GraficoViewModel> ListaGraficoView
        {
            get => _listaGraficoView;
            set
            {
                _listaGraficoView = value; OnPropertyChanged(nameof(ListaGraficoView));
            }
        }

        private int _qtdeSimulacoes = 0;
        public int QtdeSimulacoes
        {
            get => _qtdeSimulacoes;
            set
            {
                _qtdeSimulacoes = value; OnPropertyChanged(nameof(QtdeSimulacoes));
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand? RefreshCommand { get; }

        private int IdGraficoExclusao = 0;

        #endregion

        #region CONSTRUTOR

        public SimulacaoViewModel()
        {
            RefreshCommand = new Command(async () => await ExecutarRefresh());
        }

        #endregion

        #region MANIPULAÇÃO DA LISTA DE GRÁFICOS

        public async void SalvarGrafico(SimulacaoModel simulacao)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // OBTEM VALORES PARA GERAÇÃO DO GRÁFICO
                    double[] valoresGrafico = GenerateBrownianMotion(simulacao.VotalidadeMedia / 100.0, simulacao.RetornoMedio / 100.0, simulacao.PrecoInicial, simulacao.Tempo);

                    // GERA GRÁFICO PERSONALIZADO
                    Chart grafico = GeraGrafico(valoresGrafico, simulacao);

                    if (simulacao.Id != 0) // EDITAR SIMULAÇÃO
                    {
                        GraficoViewModel? graficoView = ListaGraficoView.FirstOrDefault(x => x.Simulacao.Id == simulacao.Id);
                        graficoView.Simulacao = simulacao;
                        graficoView.Grafico = grafico;
                    }
                    else // GERAR NOVA SIMULAÇÃO
                    {
                        int idGrafico = ListaGraficoView.Count > 0 ? ListaGraficoView.OrderByDescending(x => x.Simulacao.Id)
                                                                                     .Select(x => x.Simulacao.Id)
                                                                                     .FirstOrDefault() : 0;

                        simulacao.Id = idGrafico + 1;

                        GraficoViewModel graficoView = new GraficoViewModel(simulacao, grafico)
                        {
                            AbrirPopUpGrafico = AbrirPopUpGrafico_Command,
                            EditarGrafico = Editar_Command,
                            ExcluirGrafico = Excluir_Command,
                        };

                        ListaGraficoView.Add(graficoView);
                    }

                    QtdeSimulacoes = ListaGraficoView.Count;
                });
            }
            catch (Exception ex)
            {
                await CustomPopUpMensagem.Alerta("ERRO!", ex.Message, Tipos.TipoNotificacao.Erro);
            }
        }

        public async Task ExcluirGrafico()
        {
            try
            {
                var grafico = ListaGraficoView.FirstOrDefault(x => x.Simulacao.Id == IdGraficoExclusao);

                if (grafico != null)
                {
                    ListaGraficoView.Remove(grafico);
                    QtdeSimulacoes = ListaGraficoView.Count;
                }
            }
            catch (Exception ex)
            {
                await CustomPopUpMensagem.Alerta("ERRO!", ex.Message, Tipos.TipoNotificacao.Erro);
            }
        }

        public async Task LimparListaGraficos()
        {
            try
            {
                ListaGraficoView.Clear();
                QtdeSimulacoes = ListaGraficoView.Count;
            }
            catch (Exception ex)
            {
                await CustomPopUpMensagem.Alerta("ERRO!", ex.Message, Tipos.TipoNotificacao.Erro);
            }
        }

        private async Task ExecutarRefresh()
        {
            await Task.Delay(1000);
            ListaGraficoView = ListaGraficoView;
            IsRefreshing = false;
        }

        #endregion

        #region GERAÇÃO DOS GRÁFICOS

        public static double[] GenerateBrownianMotion(double votalidadeMedia, double retornoMedio, double precoInicial, int tempo)
        {
            try
            {
                Random rand = new();
                double[] valores = new double[tempo];
                valores[0] = precoInicial;

                for (int i = 1; i < tempo; i++)
                {
                    double u1 = 1.0 - rand.NextDouble();
                    double u2 = 1.0 - rand.NextDouble();
                    double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                    double retornoDiario = retornoMedio + votalidadeMedia * z;

                    valores[i] = valores[i - 1] * Math.Exp(retornoDiario);
                }

                return valores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Chart GeraGrafico(double[] valores, SimulacaoModel simulacao)
        {
            var entries = new List<ChartEntry>();

            for (int i = 0; i < valores.Length; i++)
            {
                entries.Add(new ChartEntry((float)valores[i])
                {
                    ValueLabelColor = SKColor.Parse(simulacao.CorFrenteGrafico),
                    Label = $"Dia {i + 1}",
                    ValueLabel = valores[i].ToString("F2"),
                    Color = SKColor.Parse(simulacao.CorFrenteGrafico)
                });
            }

            var chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight, // ALTERA O MODO DE EXIBIÇÃO DAS LINHAS DO GRÁFICO
                LineSize = 8,
                PointSize = 4,
                LabelColor = SKColor.Parse(simulacao.CorFrenteGrafico),
                BackgroundColor = SKColor.Parse(simulacao.CorFundoGrafico)
            };

            return chart;
        }

        #endregion

        #region COMMANDS

        public async void AbrirPopUpGrafico_Command(Chart grafico, string descricao)
        {
            var popup = new CustomPopUpExibirGrafico(grafico, descricao);
            await MopupService.Instance.PushAsync(popup);
        }

        public async void Editar_Command(SimulacaoModel simulacao)
        {
            var popup = new CustomPopUpCadastroSimulacao(this, simulacao);
            await MopupService.Instance.PushAsync(popup);
        }

        public async void Excluir_Command(int IdGrafico)
        {
            IdGraficoExclusao = IdGrafico;
            var popup = new CustomPopUpConfirmar("ATENÇÃO!", "Deseja confirmar a exclusão?", "icone_aviso.svg", ExcluirGrafico, CancelarAcesso);
            await MopupService.Instance.PushAsync(popup);
        }

        public async void LimparListaGraficos_Command()
        {
            var popup = new CustomPopUpConfirmar("ATENÇÃO!", "Deseja confirmar a exclusão de todas as simulações?", "icone_aviso.svg", LimparListaGraficos, CancelarAcesso);
            await MopupService.Instance.PushAsync(popup);
        }

        public async Task CancelarAcesso()
        {
            FecharPopup(true);
        }

        #endregion
    }
}