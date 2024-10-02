namespace OperacaoCuriosidadeMVC.Models
{
    public class InteressesModel
    {
        public int InteressesId { get; set; }
        public string Conteudo { get; set; } = string.Empty;

        public void Update(int Id, string InputConteudo)
        {
            Conteudo = InputConteudo; 
        }
    }
}
