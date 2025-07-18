﻿using GloboClima.API.ModelDto;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GloboClima.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServicoUsuario _usuarioServico;
        public AuthController(IServicoUsuario usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto? login)
        {
            if (login == null)
                return BadRequest("Os dados de login são obrigatórios.");

            var token = await _usuarioServico.ValidarLoginEGerarToken(login.Nome, login.Senha);
            return Ok(new { Token = token });
        }
    }
}
