namespace GraphGeneratorApp.Models
{
    public class SimulacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoInicial { get; set; }
        public double VotalidadeMedia { get; set; }
        public double RetornoMedio { get; set; }
        public int Tempo { get; set; }
        public Color? CorFundoGrafico { get; set; }
        public Color? CorFrenteGrafico { get; set; }

        public SimulacaoModel()
        {

        }

        public SimulacaoModel(int id, string descricao, double precoInicial, double votalidadeMedia, double retornoMedio, int tempo)
        {
            Id = id;
            Descricao = descricao;
            PrecoInicial = precoInicial;
            VotalidadeMedia = votalidadeMedia;
            RetornoMedio = retornoMedio;
            Tempo = tempo;
        }

        public SimulacaoModel(int id, string descricao, double precoInicial, double votalidadeMedia, double retornoMedio, int tempo, Color corFundoGrafico, Color corFrenteGrafico)
        {
            Id = id;
            Descricao = descricao;
            PrecoInicial = precoInicial;
            VotalidadeMedia = votalidadeMedia;
            RetornoMedio = retornoMedio;
            Tempo = tempo;
            CorFundoGrafico = corFundoGrafico;
            CorFrenteGrafico = corFrenteGrafico;
        }
    }
}