using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GloboClima.API.Models;
using GloboClima.Dominio.Models.Usuarios;
using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GloboClima.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ServicoToken _tokenService;
        public AuthController(ServicoToken tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModels? loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Os dados de login são obrigatórios.");

            var token = _tokenService.GerarToken();
            return Ok(new { Token = token });
        }

        [HttpGet("debug-tabelas")]
        public async Task<IActionResult> ListarTabelas([FromServices] IAmazonDynamoDB dynamoDb)
        {
            var response = await dynamoDb.ListTablesAsync();
            return Ok(response.TableNames);
        }
    }
}
