namespace GraphGeneratorApp.Models
{
    internal class SimulacaoModel
    {
        public int Id { get; set; }
        public double PrecoInicial { get; set; }
        public double VotalidadeMedia { get; set; }
        public double RetornoMedio { get; set; }
        public double Tempo { get; set; }

        public SimulacaoModel()
        {

        }

        public SimulacaoModel(int id, double precoInicial, double votalidadeMedia, double retornoMedio, double tempo)
        {
            Id = id;
            PrecoInicial = precoInicial;
            VotalidadeMedia = votalidadeMedia;
            RetornoMedio = retornoMedio;
            Tempo = tempo;
        }
    }
}