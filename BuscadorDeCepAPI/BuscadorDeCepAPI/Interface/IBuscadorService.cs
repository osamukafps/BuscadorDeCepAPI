using BuscadorDeCepAPI.Model;
using BuscadorDeCepAPI.ViewModels.Entrada;
using BuscadorDeCepAPI.ViewModels.Saida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscadorDeCepAPI.Interface
{
    public interface IBuscadorService
    {
        Task<Endereco> BuscarEnderecoPorCep(string cep);
        Task<List<BuscaEnderecoViewModelSaida>> BuscarCepPorEndereco(BuscaEnderecoViewModelEntrada enderecoViewModelEntrada);

    }
}
