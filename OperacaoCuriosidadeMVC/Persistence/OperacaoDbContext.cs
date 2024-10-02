using OperacaoCuriosidadeMVC.Models;

namespace OperacaoCuriosidadeMVC.Persistence
{
    public class OperacaoDbContext
    {
        public List<OperacaoModel> OperacaoModels { get; set; }
        public OperacaoDbContext()
        {
            OperacaoModels = new List<OperacaoModel>();
        }
    }
}
