using Microsoft.AspNetCore.Http.HttpResults;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;

namespace OperacaoCuriosidadeMVC.Generate
{
    public class GenerateId
    {
        private readonly UserDbContext _userDbContext;

        public GenerateId(UserDbContext userContext)
        {
            _userDbContext = userContext;
        }
        public UserModel UserIdGenerator(UserModel user)
        {

            int id;
            if (_userDbContext.UserModels.Any()==false)
                user.UserId = 1;
            else
            {
                id = _userDbContext.UserModels.Last().UserId;
                id++;
                user.UserId = id;
            }

            user.Fatos.UserId = user.UserId;
            if (user.Operacao != null)
            {
                user.Operacao.UserId = user.UserId;
                user.Operacao.OperacaoId = user.UserId;
            }
            return user;
        }
    }
}
