using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using OperacaoCuriosidadeMVC.Generate;


namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Consumes("application/json")]
    [Produces("application/json")]


    public class UserController : Controller
    {
        private readonly UserDbContext _context;
        private readonly OperacaoDbContext _contextOperacao;
        private readonly GenerateId _IdGenerator;
        
        public int UserIdentificator { get; set; }

        public UserController(UserDbContext context, OperacaoDbContext contextOperacao, GenerateId generateId)
        {
            _context = context;
            _contextOperacao = contextOperacao;
            _IdGenerator = generateId;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var user = _context.UserModels;
            return Ok(user);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
 
            var user = _context.UserModels.FirstOrDefault(u=>u.UserId == id);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpPost]

        public IActionResult Post(UserModel user)
        {
            user = _IdGenerator.UserIdGenerator(user);
            _context.UserModels.Add(user);
            if (user.Operacao != null)
            {
                _contextOperacao.OperacaoModels.Add(user.Operacao);
                user.Operacao.Valores.First().ValoresId = 1;
                user.Operacao.Sentimentos.First().SentimentosId = 1;
                user.Operacao.Interesses.First().InteressesId = 1;
            }
            
            return CreatedAtAction(nameof(GetById), new { id = user.UserId },user);
        }

        [HttpPut]

        public IActionResult Update(int Id,UserModel InputUser)
        {
            var user = _context.UserModels.FirstOrDefault(u => u.UserId == Id);

            if (user == null)
                return NotFound();
            user.Update(InputUser.UserId, InputUser.Senha, InputUser.Tipo);
            return NoContent();
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            var user = _context.UserModels.SingleOrDefault(u=>u.UserId == Id);
            if(user == null)
                return NotFound();
            user.Delete();
            return NoContent();
        }

        
    }
}
