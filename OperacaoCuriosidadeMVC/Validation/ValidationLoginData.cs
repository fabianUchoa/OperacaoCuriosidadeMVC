using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;

namespace OperacaoCuriosidadeMVC.Validation
{
    public class ValidationLoginData
    {
        private readonly UserDbContext _userDbContext;

        public ValidationLoginData(UserDbContext userContext)
        {
            _userDbContext = userContext;
        }

        public int DataValidation(UserModel user)
        {
            if(_userDbContext.UserModels.FirstOrDefault(u=>u.Fatos.Email == user.Fatos.Email) != null)
            {
                return 1;
            }else if(_userDbContext.UserModels.FirstOrDefault(u=>u.Fatos.UserName == user.Fatos.UserName) != null)
            {
                return 1;
            }
            return 0;
        }
    }
}
