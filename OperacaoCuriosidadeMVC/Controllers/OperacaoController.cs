using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using System.Data.Entity;

namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route ("api/user")]
    public class OperacaoController : Controller
    {

        private readonly OperacaoDbContext _context;
        private readonly UserDbContext _contextUser;

        public OperacaoController(OperacaoDbContext context, UserDbContext contextUser)
        {
            _context = context;
            _contextUser = contextUser;
        }

        [HttpGet("/operacao")]

        public IActionResult GetAll()
        {
            var operacao = _context.OperacaoModels;
            return Ok(operacao);
        }

        [HttpPost("{id}/operacao")]

        public IActionResult PostOperacao(int id,OperacaoModel Operacao)
        {
            
            var user = _contextUser.UserModels.FirstOrDefault(u=> u.UserId == id);
            Operacao.UserId = id;
            Operacao.OperacaoId = id;
            _context.OperacaoModels.Add(Operacao);
            if (user == null)
                return NotFound("Usuário não existe!");
            user.Operacao = Operacao;

            return NoContent();
        }

        [HttpGet ("operacao{id}")]

        public IActionResult GetById(int id)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);

            return Ok(operacao);
        }

        [HttpDelete("operacao{id}")]

        public IActionResult DeleteById(int id)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op=>op.OperacaoId==id);
            if (operacao == null)
                return NotFound();
            operacao.Delete();
            return NoContent();

        }

        


    }
}
