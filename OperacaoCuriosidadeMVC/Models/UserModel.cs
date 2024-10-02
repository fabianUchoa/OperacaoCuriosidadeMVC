using System.Data;

namespace OperacaoCuriosidadeMVC.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Fatos = new FatosModel();
            Operacao = new OperacaoModel();
            Status = true;
            Tipo = false;
        }
        public required int UserId { get; set; }
        public required string Senha { get; set; }
        public bool Tipo { get; set; }
        public bool Status { get; set; }
        public required FatosModel Fatos { get; set; }
        public OperacaoModel? Operacao { get; set; }

        public void Update(int Id, string InputSenha, bool InputTipo)
        {
            UserId = Id;
            Senha = InputSenha;
            Tipo = InputTipo;

        }
        public void Delete()
        {
            Status = false;
        }
        

    }
}
