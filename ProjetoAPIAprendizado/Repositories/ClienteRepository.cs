using System;
using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado.Models;
namespace ProjetoAPIAprendizado.Repositories
{

	public class ClienteRepository : IClienteRepository
    {
        private readonly ApiDbContext _contexto;

        public ClienteRepository(ApiDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cliente> SearchByIdAsync(int id)
        {
            return await _contexto.Clientes.FindAsync(id);
        }
        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _contexto.Clientes.ToListAsync();
        }
        public async Task<Cliente> CreateClienteAsync(Cliente novoCliente)
        {
            await _contexto.Clientes.AddAsync(novoCliente);
            await _contexto.SaveChangesAsync();
            return novoCliente;
        }
        public async Task<Cliente> RemoveClienteAsync(int id)
        {
            var clienteEncontrado = await _contexto.Clientes.FindAsync(id);
            if (clienteEncontrado == null)
            {
                return null;
            }
            _contexto.Clientes.Remove(clienteEncontrado);
            await _contexto.SaveChangesAsync();
            return clienteEncontrado;
        }
        public async Task<Cliente> UpdateClienteAsync(int id, Cliente clienteAtualizado) 
        {
            var clienteExistente = await _contexto.Clientes.FindAsync(id);

            if (clienteExistente == null)
            {
                return null;

            }
            clienteExistente.AtualizarDados(clienteAtualizado.Nome, clienteAtualizado.Cpf);
            await _contexto.SaveChangesAsync();
            return clienteExistente;
        }




    }
}