namespace OperacaoCuriosidadeMVC.Models
{
    public class OperacaoModel
    {
        public OperacaoModel()
        {
            Valores = new List<ValoresModel>();
            Interesses = new List<InteressesModel>();
            Sentimentos = new List<SentimentosModel>();
        }

        public List<ValoresModel> Valores { get; set; }
        public List<InteressesModel> Interesses { get; set; }
        public List<SentimentosModel> Sentimentos { get; set; }

        public int UserId { get; set; }
        public int OperacaoId { get; set; }

        public void Delete()
        {
            Valores.Clear();
            Interesses.Clear();
            Sentimentos.Clear();
            UserId = 0;
            OperacaoId = 0;
        }
    }
}
