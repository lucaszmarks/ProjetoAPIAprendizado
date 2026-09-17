using System;
using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado.Context;
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
        public async Task<Endereco> GetEnderecoByIdAsync(int id)
        {
            return await _contexto.Enderecos
                          .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Endereco> UpdateEnderecoAsync(Endereco enderecoAtualizado)
        {
            _contexto.Enderecos.Update(enderecoAtualizado);
            await _contexto.SaveChangesAsync();
            return enderecoAtualizado;
        }

        public async Task<bool> DeleteEnderecoAsync(int id)
        {
            var endereco = await _contexto.Enderecos.FindAsync(id);

            if (endereco == null)
            {
                return false;
            }

            _contexto.Enderecos.Remove(endereco);
            await _contexto.SaveChangesAsync();

            return true;
        }

    }
}
