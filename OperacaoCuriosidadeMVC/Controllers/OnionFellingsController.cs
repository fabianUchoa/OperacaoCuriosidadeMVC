using Microsoft.AspNetCore.Mvc;
using OperacaoCuriosidadeMVC.Models;
using OperacaoCuriosidadeMVC.Persistence;
using OperacaoCuriosidadeMVC.Generate;
using System.Data.Entity;

namespace OperacaoCuriosidadeMVC.Controllers
{
    [ApiController]
    [Route("api/user/operacao")]
    public class OnionFellingsController : Controller
    {
        private readonly OperacaoDbContext _context;
        private readonly GenerateOperationItemsId _generateItemsId;

        public OnionFellingsController(OperacaoDbContext context, GenerateOperationItemsId GenerateItemId)
        {
            _context = context;
            _generateItemsId = GenerateItemId;
        }

        [HttpPut("{id}/valores")]

        public IActionResult UpdateValores(int id, ValoresModel valores)
        {

            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);
            if (operacao == null)
                return NotFound();
            var valoresUpdate = operacao.Valores.FirstOrDefault(val=>val.ValoresId == valores.ValoresId);
            if (valoresUpdate == null)
                return NotFound();
            valoresUpdate.Conteudo = valores.Conteudo;

            return Ok(operacao);
        }

        [HttpPut("{id}/sentimentos")]
        public IActionResult UpdateSentimentos(int id, SentimentosModel sentimentos)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);
            if (operacao == null)
                return NotFound();
            var sentimentosUpdate = operacao.Sentimentos.FirstOrDefault(s => s.SentimentosId == sentimentos.SentimentosId);
            if (sentimentosUpdate == null)
                return NotFound();
            sentimentosUpdate.Conteudo = sentimentos.Conteudo;
            
            return Ok(operacao);
        }

        [HttpPut("{id}/interesses")]
        public IActionResult UpdateInteresses(int id, InteressesModel interesses)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);
            if (operacao == null)
                return NotFound();
            var interessesUpdate = operacao.Interesses.FirstOrDefault( interes=>interes.InteressesId == interesses.InteressesId);
            if (interessesUpdate == null)
                return NotFound();
            interessesUpdate.Conteudo = interesses.Conteudo;
            return Ok(operacao);
        }

        [HttpPost("{id}/interesses")]

        public IActionResult PostInteresses(int id, InteressesModel interesses)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op=>op.OperacaoId==id);
            if(operacao == null)
                return NotFound();
            interesses.InteressesId = _generateItemsId.GenerateItemsId(operacao, "Interesses");
            operacao.Interesses.Add(interesses);
            return Ok(operacao.Interesses);
        }

        [HttpPost("{id}/sentimentos")]

        public IActionResult PostSentimentos(int id, SentimentosModel sentimentos)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);
            if(operacao == null)
                return NotFound();

            operacao.Sentimentos.Add(sentimentos);
            return Ok(operacao.Sentimentos);
        }

        [HttpPost("{id}/valores")]

        public IActionResult PostValores(int id, ValoresModel valores)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op => op.OperacaoId == id);
            if(operacao == null)
                return NotFound();
            valores.ValoresId = _generateItemsId.GenerateItemsId(operacao, "Valores");
            operacao.Valores.Add(valores);
            return Ok(operacao.Valores);
        }

        [HttpDelete("{id}/{tipo}/{itemId}")]

        public IActionResult DeleteOpItem(int id, string tipo, int itemId)
        {
            var operacao = _context.OperacaoModels.FirstOrDefault(op=> op.OperacaoId == id);
            if (operacao == null)
                return NotFound();
            switch (tipo.ToLower())
            {
                case "valores":
                    var val = operacao.Valores.FirstOrDefault(val => val.ValoresId == itemId);
                    if (val == null)
                        return NotFound();
                    operacao.Valores.Remove(val);
                    return NoContent();
                case "sentimentos":
                    var sen = operacao.Sentimentos.FirstOrDefault(sen => sen.SentimentosId== itemId);
                    if (sen == null)
                        return NotFound();
                    operacao.Sentimentos.Remove(sen);
                    return NoContent();
                case "interesses":
                    var interes = operacao.Interesses.FirstOrDefault(interes => interes.InteressesId== itemId);
                    if (interes == null)
                        return NotFound();
                    operacao.Interesses.Remove(interes);
                    return NoContent();

                default:
                    return BadRequest("Tipo Inválido!");
            }
        }
    }
}