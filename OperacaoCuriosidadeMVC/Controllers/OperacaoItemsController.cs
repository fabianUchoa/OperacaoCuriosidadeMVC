using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using OperacaoCuriosidadeMVC.Generate;


namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route("api/user/operacao")]
    public class OperacaoItemsController : Controller
    {
        private readonly OperacaoDbContext _context;
        private readonly UserDbContext _contextUser;
        private readonly GenerateOperationItemsId _generateItemsId;

        public OperacaoItemsController(OperacaoDbContext context, GenerateOperationItemsId GenerateItemId, UserDbContext contextUser)
        {
            _contextUser = contextUser;
            _context = context;
            _generateItemsId = GenerateItemId;
        }

        [HttpPut("item-update/{userId}/{tipo}/{itemId}")]
        public IActionResult UpdateSentimentos(int userId, string tipo, int itemId,[FromBody] string conteudo)
        {
            var user = _contextUser.UserModels.FirstOrDefault(op => op.UserId == userId);
            if (user == null)
                return NotFound("Usuário inexistente");

            var operacao = user.Operacao;
            if (operacao == null)
                return NotFound("O usuário não possui operação cadastrada");

            if (tipo == "sentimentos")
            {
                operacao.Sentimentos.FirstOrDefault(s => s.SentimentosId == itemId).Conteudo = conteudo;
            }else if (tipo == "valores")
            {
                operacao.Valores.FirstOrDefault(v => v.ValoresId == itemId).Conteudo = conteudo;
            }else if(tipo == "interesses")
                operacao.Interesses.FirstOrDefault(i => i.InteressesId == itemId).Conteudo = conteudo;
            

            user.Operacao = operacao;
            _contextUser.UpdateOrDeleteUser();

            return Ok(operacao);
        }


        [HttpPost("{id}/interesses")]

        public IActionResult PostInteresses(int id, InteressesModel interesses)
        {
            var user = _contextUser.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuário não existe!");
            var operacao = user.Operacao;
            if(operacao == null)
                return NotFound("O usuário não possui operação cadastrada!");

            interesses.InteressesId = _generateItemsId.GenerateItemsId(operacao, "Interesses");

            operacao.Interesses.Add(interesses);
            
            user.Operacao = operacao;
            _contextUser.UpdateOrDeleteUser();
            return Ok(operacao.Interesses);
        }

        [HttpPost("{id}/sentimentos")]

        public IActionResult PostSentimentos(int id, SentimentosModel sentimentos)
        {
            var user = _contextUser.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuário não existe!");
            var operacao = user.Operacao;
            if(operacao == null)
                return NotFound("O usuário não possui operação cadastrada!");

            sentimentos.SentimentosId = _generateItemsId.GenerateItemsId(operacao, "Sentimentos");

            operacao.Sentimentos.Add(sentimentos);

            user.Operacao = operacao;
            _contextUser.UpdateOrDeleteUser();

            return Ok(operacao.Sentimentos);
        }

        [HttpPost("{id}/valores")]

        public IActionResult PostValores(int id, ValoresModel valores)
        {
            var user = _contextUser.UserModels.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuário não existe");
            var operacao = user.Operacao;
            if (operacao == null)
                return NotFound("O usuário não possui operação cadastrada!");
            valores.ValoresId = _generateItemsId.GenerateItemsId(operacao, "Valores");

            operacao.Valores.Add(valores);

            
            user.Operacao = operacao;
            _contextUser.UpdateOrDeleteUser();

            return Ok(operacao.Valores);
        }

        [HttpDelete("{id}/{tipo}/{itemId}")]

        public IActionResult DeleteOpItem(int id, string tipo, int itemId)
        {
            var user = _contextUser.UserModels.FirstOrDefault(u=>u.UserId==id);
            var operacao = user.Operacao;
            if (operacao == null || user == null)
                return NotFound();
            switch (tipo.ToLower())
            {
                case "valores":
                    var val = operacao.Valores.FirstOrDefault(val => val.ValoresId == itemId);
                    if (val == null)
                        return NotFound("Não há itens para remover!");

                    operacao.Valores.Remove(val);
                    user.Operacao = operacao;
                    _contextUser.UpdateOrDeleteUser();

                    return NoContent();
                case "sentimentos":
                    var sen = operacao.Sentimentos.FirstOrDefault(sen => sen.SentimentosId== itemId);
                    if (sen == null)
                        return NotFound("Não há itens para remover!");

                    operacao.Sentimentos.Remove(sen);
                    user.Operacao = operacao;
                    _contextUser.UpdateOrDeleteUser();

                    return NoContent();
                case "interesses":
                    var interes = operacao.Interesses.FirstOrDefault(interes => interes.InteressesId== itemId);
                    if (interes == null)
                        return NotFound("Não há itens para remover!");

                    operacao.Interesses.Remove(interes);
                    user.Operacao = operacao;
                    _contextUser.UpdateOrDeleteUser();

                    return NoContent();

                default:
                    return BadRequest("Tipo Inválido!");
            }


        }
    }
}