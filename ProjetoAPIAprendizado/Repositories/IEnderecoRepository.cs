using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.Models;
namespace ProjetoAPIAprendizado.Repositories
{
    public interface IEnderecoRepository
    {
        
        Task<Endereco> CreateEnderecoAsync(Endereco novoEndereco);
        Task<List<Endereco>> GetEnderecoAsync();
        Task<Endereco> GetEnderecoByIdAsync(int id);
        Task<Endereco> UpdateEnderecoAsync(Endereco enderecoAtualizado);
        Task<bool> DeleteEnderecoAsync(int id);



    }
}
