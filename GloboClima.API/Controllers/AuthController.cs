using GloboClima.API.Models;
using GloboClima.API.Services;
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
        public IActionResult Login(LoginDTO? loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Os dados de login são obrigatórios.");

            var token = _tokenService.GerarToken();
            return Ok(new { Token = token });
        }
    }
}
