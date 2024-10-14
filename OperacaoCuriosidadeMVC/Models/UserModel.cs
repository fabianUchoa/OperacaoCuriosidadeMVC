

using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OperacaoCuriosidadeMVC.Models
{
    public class UserModel
    {
        public DateOnly CreateTime { get; set; }
        public List<int>? RegisterByMe { get; set; }
        public int[]? SharedWithMe { get; set; }
        public string? ProfileImgPath { get; set; }
        public required int UserId { get; set; }
        public string? UserCode { get; set; }
        public required string Senha { get; set; }
        public required bool Tipo { get; set; }
        public required bool Status { get; set; }
        public required FatosModel Fatos { get; set; }
        public OperacaoModel? Operacao { get; set; }
        public void Delete()
        {
            Status = false;
        }

        public UserModel()
        {
            Fatos = new FatosModel();
            Status = true;
            Tipo = false;
            CreateTime = DateOnly.FromDateTime(DateTime.Today);
        }

        
    }
}
