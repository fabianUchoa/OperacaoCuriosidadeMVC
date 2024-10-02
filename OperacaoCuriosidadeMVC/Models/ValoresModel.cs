namespace OperacaoCuriosidadeMVC.Models
{
    public class ValoresModel
    {
        public int ValoresId { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public void Update(int Id, string InputConteudo)
        {
            Conteudo = InputConteudo;
        }

    }
}
