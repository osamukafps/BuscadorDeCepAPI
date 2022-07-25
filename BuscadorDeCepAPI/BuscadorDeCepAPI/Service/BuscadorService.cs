using BuscadorDeCepAPI.Interface;
using BuscadorDeCepAPI.Model;
using BuscadorDeCepAPI.ViewModels.Entrada;
using BuscadorDeCepAPI.ViewModels.Saida;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuscadorDeCepAPI.Service
{
    public class BuscadorService : IBuscadorService
    {
        private readonly IConfiguration _configuration;

        public BuscadorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<BuscaEnderecoViewModelSaida>> BuscarCepPorEndereco(BuscaEnderecoViewModelEntrada enderecoViewModelEntrada)
        {
            HttpClient _httpClient = new HttpClient();
            string url = MontarUrlBuscaPorEndereco(enderecoViewModelEntrada);

            Uri uri = new Uri(_configuration["BuscadorDeCepAPI:UrlBase"] + url + _configuration["BuscadorDeCepAPI:ReturnType"]);

            var response = await _httpClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            List<BuscaEnderecoViewModelSaida> endereco = JsonConvert.DeserializeObject<List<BuscaEnderecoViewModelSaida>>(responseString);

            return endereco;
        }

        public async Task<Endereco> BuscarEnderecoPorCep(string cep)
        {
            HttpClient _httpClient = new HttpClient();

            Uri uri = new Uri(_configuration["BuscadorDeCepAPI:UrlBase"] + cep + _configuration["BuscadorDeCepAPI:ReturnType"]);

            var response = await _httpClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<Endereco>(responseString);

            return endereco;
            
        }

        private string MontarUrlBuscaPorEndereco(BuscaEnderecoViewModelEntrada entrada)
        {
            string url = $"{entrada.Estado.ToUpper()}/{entrada.Cidade.Replace(" ", "%20")}/{entrada.Rua.Replace(' ', '+')}";

            return url;
        }
    }
}
