using OperacaoCuriosidadeMVC.Models;
using System.Text.Json;

namespace OperacaoCuriosidadeMVC.Persistence.JsonData
{
    public class JsonFileService
    {
        private string _filePath = "./users.json";

        public List<UserModel> ReadUsersFromFile()
        {
            if (System.IO.File.Exists(_filePath))
            {
               var CreateTime = DateTime.Today;
                var jsonData = System.IO.File.ReadAllText(_filePath);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    return JsonSerializer.Deserialize<List<UserModel>>(jsonData);
                }
            }
            return new List<UserModel>();
        }

        public void WriteUserstoFile(List<UserModel> user)
        {
            var jsonData = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true});
            System.IO.File.WriteAllText(_filePath, jsonData);
        }

        public void WriteOperacaoToFile(OperacaoModel operacao)
        {
            var jsonData = JsonSerializer.Serialize(operacao, new JsonSerializerOptions {WriteIndented=true});

        }
    }
}
