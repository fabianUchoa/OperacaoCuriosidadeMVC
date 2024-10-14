namespace OperacaoCuriosidadeMVC.Models
{
    public class FatosModel
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Nasc { get; set; }
        public string EstadoCivil { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Profissao { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;

        public int UserId { get; set; }
        
    }
}
