using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using OperacaoCuriosidadeMVC.Generate;
using OperacaoCuriosidadeMVC.Persistence.JsonData;
using OperacaoCuriosidadeMVC.Validation;



namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route("api/user")]


    public class UserController : Controller
    {
        private readonly UserDbContext _context;
        private readonly OperacaoDbContext _contextOperacao;
        private readonly GenerateId _IdGenerator;
        private readonly JsonFileService _jsonFileServices;
        private readonly ValidationLoginData _validationLoginData;

        public int UserIdentificator { get; set; }

        public UserController(UserDbContext context, OperacaoDbContext contextOperacao, GenerateId generateId, JsonFileService jsonFileServices, ValidationLoginData validationLoginData)
        {
            _context = context;
            _contextOperacao = contextOperacao;
            _IdGenerator = generateId;
            _jsonFileServices = jsonFileServices;
            _validationLoginData = validationLoginData;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var user = _context.UserModels;
            return Ok(user);
        }

        [HttpGet ("operacao-cadastrada")]

        public IActionResult Get() 
        {
            var user = _context.UserModels.Where(u => u.Operacao != null).ToList();
            if (user == null)
                return NotFound("Não existe usuários com operação cadastrados.");
            return Ok(user);
        }

        [HttpGet("cadastrados-por-mim/{id}")]

        public IActionResult GetRegisterByMe(int id) 
        {
            var user = _context.UserModels.FirstOrDefault(u=> u.UserId==id);
            if (user == null)
                return NotFound("Não há usuário cadastrado com esse ID.");

            if (user.RegistradasPorMim == null || user.RegistradasPorMim.Usuarios==null)
                return NotFound("Esse usuário não possui usuários cadastrados.");

            var RegisteredUsers = user.RegistradasPorMim.Usuarios;
            return Ok(RegisteredUsers);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {

            var user = _context.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpGet ("registrados")]

        public int[] GetRegisterUsers()
        {
            var userRegister = _context.UserModels.Count;
            var userRegisterIncomplete = _context.UserModels.Where(u => u.Operacao == null).Count();

            int[] user = new int[2];
            user[0] = userRegister;
            user[1] = userRegisterIncomplete;
            return user;
        }

        [HttpGet("filtros")]
        
        public IActionResult UserFilter(int id, string? userCode, bool? status, bool? tipo, DateOnly? dataIn, DateOnly? dataOut)
        {
            var user = _context.UserModels.FirstOrDefault(u=> u.UserId == id);
            
            if (user.RegistradasPorMim == null||user.RegistradasPorMim.Usuarios==null )
                return NotFound("Esse usuário não possui registros de usuários.");
            var query = user.RegistradasPorMim.Usuarios.AsQueryable();

            
            if (!string.IsNullOrEmpty(userCode))
            {
                query = query.Where(u => u.UserCode == userCode); 
            }

          
            if (status.HasValue)
            {
                query = query.Where(u => u.Status == status); 
            }

            if (tipo.HasValue)
            {
                query = query.Where(u => u.Tipo == tipo);
            }

            if (dataIn.HasValue)
            {
                query = query.Where(u => u.CreateTime >= dataIn); 
            }

       
            if (dataOut.HasValue)
            {
                query = query.Where(u => u.CreateTime <= dataOut); 
            }

        
            var users = query.ToList();

         
            return Ok(users);
        }
       

        [HttpPost]

        public IActionResult Post(UserModel user)
        {
            user = _IdGenerator.UserIdGenerator(user);
            

             var validation = _validationLoginData.DataValidation(user);

            if(validation == 1)
            {
                return BadRequest("Email ou UserName já cadastrados!");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.AddUser(user);

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        
        [HttpPut("update-picture/{id}")]
        public IActionResult UpdateProfilePicture(int id, [FromBody]string picture)
        {
            var user = _context.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound("O usuário não existe!");
            user.ProfileImgPath = picture;

            _context.UpdateOrDeleteUser();
            return Ok("Foto de perfil atualizada!");

        }


   
    [HttpPut("{Id}")]

        public IActionResult Update(int Id, FatosModel fatos)
        {
            var user = _context.UserModels.FirstOrDefault(u => u.UserId == Id);

            if (user == null)
                return NotFound("Usuário não existe");
            user.Fatos = fatos;
            _context.UpdateOrDeleteUser();
            return NoContent();
        }

        [HttpPut("nova-senha/{id}")]
        public IActionResult UpdateSenha(int id, [FromBody]string senha)
        {
            var user = _context.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound("Usuário não encontrado");
            user.Senha = senha;
            _context.UpdateOrDeleteUser();
            return NoContent();
        }

        [HttpPut("altera-status/{Id}")]
        public IActionResult AlteraStatusUser(int Id)
        {
            var user = _context.UserModels.FirstOrDefault(u=>u.UserId == Id);
            if (user == null)
                return NotFound("Usuário não existe");
            if(user.Status == true)
                user.Status = false;
            else
                user.Status = true;
            _context.UpdateOrDeleteUser();
            return Ok(user);
        }

        [HttpPut("altera-cargo")]
        public IActionResult AlterarCargoUser(int Id)
        {
            var user = _context.UserModels.FirstOrDefault(u=>u.UserId==Id);
            if (user == null)
                return NotFound("Usuário não existe");
            if (user.Tipo == true)
                user.Tipo = false;
            else
                user.Tipo = true;

            _context.UpdateOrDeleteUser();
            return Ok(user);
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            var user = _context.UserModels.SingleOrDefault(u=>u.UserId == Id);
            if(user == null)
                return NotFound("Usuário não existe");
            _context.UserModels.Remove(user);   
            _context.UpdateOrDeleteUser();
            return NoContent();
        }

        
    }
}
