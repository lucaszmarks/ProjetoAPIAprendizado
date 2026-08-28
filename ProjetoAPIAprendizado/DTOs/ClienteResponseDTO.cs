using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado.DTOs
{
    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public List<EnderecoResponseDTO> Enderecos { get; set; }
    }
}