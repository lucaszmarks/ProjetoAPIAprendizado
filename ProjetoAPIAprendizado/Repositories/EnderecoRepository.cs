using System;
using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApiDbContext _contexto;

        public EnderecoRepository(ApiDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Endereco> CreateEnderecoAsync(Endereco novoEndereco)
        {
            await _contexto.SaveChangesAsync();
            return novoEndereco;
        } 
    }
}
