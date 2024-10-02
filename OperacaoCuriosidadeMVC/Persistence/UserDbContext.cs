using OperacaoCuriosidadeMVC.Models;


namespace OperacaoCuriosidadeMVC.Persistence
{
    public class UserDbContext
    {
        public List<UserModel> UserModels { get; set; }


        public UserDbContext()
        {
            UserModels = new List<UserModel>();
            
        }

        
    }
}
