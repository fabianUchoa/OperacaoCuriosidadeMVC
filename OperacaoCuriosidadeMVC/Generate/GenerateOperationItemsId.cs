using Microsoft.AspNetCore.Http.HttpResults;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;

namespace OperacaoCuriosidadeMVC.Generate
{
    public class GenerateOperationItemsId
    {
        private readonly OperacaoDbContext _operacaoContext;
        private readonly UserDbContext _userContext;

        public GenerateOperationItemsId(OperacaoDbContext operacaoDbContext, UserDbContext userDbContext)
        {
            _operacaoContext = operacaoDbContext;
            _userContext = userDbContext;
        }

        public int GenerateItemsId(OperacaoModel operacao, string funcChamada)
        {
            int IdItem;
            if (funcChamada == "Interesses")
            {
                if (operacao.Interesses == null || operacao.Interesses.Any() == false) 
                {
                    operacao.Interesses = new List<InteressesModel>();
                    IdItem = 0; 
                }
                else
                {
                    IdItem = operacao.Interesses.Last().InteressesId;
                }
                
            }
            else if (funcChamada == "Valores")
            {
                if (operacao.Valores == null || operacao.Valores.Any() == false)
                {
                    operacao.Valores = new List<ValoresModel>();
                    IdItem = 0;
                }
                else
                    IdItem = operacao.Valores.Last().ValoresId;
            }
            else
            {
                if (operacao.Sentimentos == null || operacao.Sentimentos.Any() == false)
                {
                    operacao.Sentimentos = new List<SentimentosModel>();
                    IdItem = 0;
                }
                else
                    IdItem = operacao.Sentimentos.Last().SentimentosId;
            }

            if (IdItem == 0)
                IdItem = 1;
            else IdItem++;
            var user = _userContext.UserModels.FirstOrDefault(u => u.UserId == operacao.UserId);
            user.Operacao = operacao;
            _userContext.UpdateOrDeleteUser();

            return IdItem;
        }

    }
}
