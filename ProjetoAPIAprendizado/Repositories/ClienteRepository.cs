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

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _contexto.Clientes
                            .Include(c => c.Enderecos) 
                          .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Cliente>> GetClientesAsync(int numeroPagina, int tamanhoPagina)
        {
            return await _contexto.Clientes.Include(c => c.Enderecos)
                          .Skip((numeroPagina - 1) * tamanhoPagina)
                          .Take(tamanhoPagina)
                          .ToListAsync();
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
        public async Task<Cliente> UpdateClienteAsync(Cliente clienteAtualizado) 
        {
            _contexto.Clientes.Update(clienteAtualizado);
            await _contexto.SaveChangesAsync();
            return clienteAtualizado;
        }
        public async Task<bool> CheckCpfExistsAsync(string cpf)
        {
            
            return await _contexto.Clientes.AnyAsync(c => c.Cpf == cpf);
        }
        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _contexto.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return false; 
            }

            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();

            return true; 
        }



    }
}