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
            _contexto.Enderecos.Add(novoEndereco);
            await _contexto.SaveChangesAsync();
            return novoEndereco;
        }
        public async Task<List<Endereco>> GetEnderecoAsync()
        {
            return await _contexto.Enderecos.ToListAsync();
        }

    }
}
