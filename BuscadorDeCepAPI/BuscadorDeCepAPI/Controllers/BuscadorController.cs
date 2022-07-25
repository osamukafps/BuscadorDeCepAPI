using BuscadorDeCepAPI.Interface;
using BuscadorDeCepAPI.Model;
using BuscadorDeCepAPI.ViewModels.Entrada;
using BuscadorDeCepAPI.ViewModels.Saida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuscadorDeCepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuscadorController : ControllerBase
    {
        private readonly IBuscadorService _buscadorService;

        public BuscadorController(IBuscadorService buscadorService)
        {
            _buscadorService = buscadorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarEnderecoPorCep(string cep)
        {
            var endereco = new Endereco();
            try
            {
                if(cep != null && cep.Length == 8)
                {
                   endereco = await _buscadorService.BuscarEnderecoPorCep(cep);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(endereco);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarCepPorEndereco([FromBody]BuscaEnderecoViewModelEntrada entrada)
        {
            var endereco = new List<BuscaEnderecoViewModelSaida>();
            try
            {
                endereco = await _buscadorService.BuscarCepPorEndereco(entrada);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(endereco);
        }
    }
}
