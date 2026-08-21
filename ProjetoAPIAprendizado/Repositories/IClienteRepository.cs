using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado.Repositories
{
	public interface IClienteRepository
	{
		Task<Cliente> SearchByIdAsync(int id);
        Task<List<Cliente>> GetClientesAsync();
		Task<Cliente> CreateClienteAsync(Cliente novoCliente);
		Task<Cliente> RemoveClienteAsync(int id);
		Task<Cliente> UpdateClienteAsync(int id , Cliente clienteAtualizado);


    }
}