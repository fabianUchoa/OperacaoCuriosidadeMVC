using Microsoft.AspNetCore.Http.HttpResults;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;

namespace OperacaoCuriosidadeMVC.Generate
{
    public class GenerateOperationItemsId
    {
        private readonly OperacaoDbContext _operacaoContext;

        public GenerateOperationItemsId(OperacaoDbContext operacaoDbContext)
        {
            _operacaoContext = operacaoDbContext;
        }

        public int GenerateItemsId(OperacaoModel operacao, string funcChamada)
        {
            int IdItem;
            if (funcChamada == "Interesses")
            {
                if (operacao.Interesses.Any() == false)
                    IdItem = 0;
                else
                {
                    IdItem = operacao.Interesses.Last().InteressesId;
                }
                
            }
            else if (funcChamada == "Valores")
            {
                if (operacao.Valores.Any() == false)
                    IdItem = 0;
                else
                    IdItem = operacao.Valores.Last().ValoresId;
            }
            else
            {
                if (operacao.Sentimentos.Any() == false)
                    IdItem = 0;
                else
                    IdItem = operacao.Sentimentos.Last().SentimentosId;
            }

            if (IdItem == 0)
                IdItem = 1;
            else IdItem++;

            return IdItem;
        }

    }
}
