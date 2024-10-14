using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using OperacaoCuriosidadeMVC.Persistence.JsonData;


namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route ("api/user")]
    public class OperacaoController : Controller
    {

        private readonly OperacaoDbContext _context;
        private readonly UserDbContext _contextUser;
        private readonly JsonFileService _jsonFileService;

        public OperacaoController(OperacaoDbContext context, UserDbContext contextUser, JsonFileService jsonFileService)
        {
            _context = context;
            _contextUser = contextUser;
            _jsonFileService = jsonFileService;
        }
  



        [HttpPost("{id}/operacao")]

        public IActionResult PostOperacao(int id,OperacaoModel Operacao)
        {
            var userCadastrador = _contextUser.UserModels.FirstOrDefault(u => u.UserId == Operacao.idCadastrador);
            if (userCadastrador == null)
                return NotFound("Usuário não está logado!");
            if (userCadastrador.RegisterByMe == null)
                userCadastrador.RegisterByMe = new List<int>();
            userCadastrador.RegisterByMe.Add(id);

            _contextUser.UpdateOrDeleteUser();

            var user = _contextUser.UserModels.FirstOrDefault(u=> u.UserId == id);
            if (user == null)
                return NotFound("Usuário não existe!");
            Operacao.UserId = id;
            Operacao.OperacaoId = id;
            
            _context.OperacaoModels.Add(Operacao);
           
            user.Operacao = Operacao;
            _contextUser.UpdateOrDeleteUser();
            return NoContent();
        }

       
        [HttpDelete("operacao/{id}")]

        public IActionResult DeleteById(int id)
        {
            var user = _contextUser.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("O usuário não existe");
            var operacao = user.Operacao;
            if (operacao == null)
                return NotFound("O usuário não possui operação para excluir!");
            

            
            user.Operacao = null;
            _contextUser.UpdateOrDeleteUser();

            return NoContent();

        }

        


    }
}
