namespace OperacaoCuriosidadeMVC.Models
{
    public class Auditoria
    {
        public string Local {  get; set; }
        public string Categoria { get; set; };
        public string Usuario { get; set; };
        public DateTime DateTime { get; set; };
        public string Item { get; set; };

    }
}
