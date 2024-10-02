namespace OperacaoCuriosidadeMVC.Models
{
    public class SentimentosModel 
    {
        public int SentimentosId { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public void Update(int Id, string InputConteudo)
        {
            Conteudo = InputConteudo;
        }
    }
}
