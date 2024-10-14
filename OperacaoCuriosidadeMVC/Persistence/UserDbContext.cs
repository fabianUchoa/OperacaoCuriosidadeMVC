using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence.JsonData;


namespace OperacaoCuriosidadeMVC.Persistence
{
    public class UserDbContext
    {
        public List<UserModel> UserModels { get; set; }
        private readonly JsonFileService _jsonFileService;


        public UserDbContext(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;

            UserModels = _jsonFileService.ReadUsersFromFile();
        }

        public void AddUser(UserModel user)
        {
            if ((user.UserId).ToString().Length == 1)
                user.UserCode = ("24-0000" + user.UserId).ToString();
            else
                user.UserCode = ("24-000" + user.UserCode).ToString();
            user.ProfileImgPath = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIGhlaWdodD0iMjRweCIgdmlld0JveD0iMCAtOTYwIDk2MCA5NjAiIHdpZHRoPSIyNHB4IiBmaWxsPSIjNmIxYmM2Ij48cGF0aCBkPSJNNDgwLTQ4MHEtNjYgMC0xMTMtNDd0LTQ3LTExM3EwLTY2IDQ3LTExM3QxMTMtNDdxNjYgMCAxMTMgNDd0NDcgMTEzcTAgNjYtNDcgMTEzdC0xMTMgNDdaTTE2MC0yNDB2LTMycTAtMzQgMTcuNS02Mi41VDIyNC0zNzhxNjItMzEgMTI2LTQ2LjVUNDgwLTQ0MHE2NiAwIDEzMCAxNS41VDczNi0zNzhxMjkgMTUgNDYuNSA0My41VDgwMC0yNzJ2MzJxMCAzMy0yMy41IDU2LjVUNzIwLTE2MEgyNDBxLTMzIDAtNTYuNS0yMy41VDE2MC0yNDBabTgwIDBoNDgwdi0zMnEwLTExLTUuNS0yMFQ3MDAtMzA2cS01NC0yNy0xMDktNDAuNVQ0ODAtMzYwcS01NiAwLTExMSAxMy41VDI2MC0zMDZxLTkgNS0xNC41IDE0dC01LjUgMjB2MzJabTI0MC0zMjBxMzMgMCA1Ni41LTIzLjVUNTYwLTY0MHEwLTMzLTIzLjUtNTYuNVQ0ODAtNzIwcS0zMyAwLTU2LjUgMjMuNVQ0MDAtNjQwcTAgMzMgMjMuNSA1Ni41VDQ4MC01NjBabTAtODBabTAgNDAwWiIvPjwvc3ZnPg==";


            UserModels.Add(user);
            _jsonFileService.WriteUserstoFile(UserModels);
        }

        public void UpdateOrDeleteUser()
        {
            _jsonFileService.WriteUserstoFile(UserModels);
        }



        
    }
}
