using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence.JsonData;

namespace OperacaoCuriosidadeMVC.Persistence
{
    public class OperacaoDbContext
    {
        public List<OperacaoModel> OperacaoModels { get; set; }
        private readonly JsonFileService _jsonFileService;
        public OperacaoDbContext(JsonFileService jsonFileService)
        {
            OperacaoModels = new List<OperacaoModel>();
            _jsonFileService = jsonFileService;
        }
        /*
        public void addOperacao(OperacaoModel operacao)
        {
            OperacaoModels.Add(operacao);
            _jsonFileService
        }*/
    }
}
