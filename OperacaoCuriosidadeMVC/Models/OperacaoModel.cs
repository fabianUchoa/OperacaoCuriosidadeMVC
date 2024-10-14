namespace OperacaoCuriosidadeMVC.Models
{
    public class OperacaoModel
    {
       

        public List<ValoresModel>? Valores { get; set; }
        public List<InteressesModel>? Interesses { get; set; }
        public List<SentimentosModel>? Sentimentos { get; set; }

        public int UserId { get; set; }
        public int OperacaoId { get; set; }
        public int? idCadastrador { get; set; }

    }
}
