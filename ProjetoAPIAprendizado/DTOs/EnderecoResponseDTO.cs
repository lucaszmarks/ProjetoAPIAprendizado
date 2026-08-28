using ProjetoAPIAprendizado.Models;

namespace ProjetoAPIAprendizado.DTOs
{
    public class EnderecoResponseDTO
    {

        public int Id { get; set; }
        public string Rua { get; set; }

        public int Numero { get; set; }
        public string Bairro { get; set; }
        public int ClienteId { get; set; }

        


    }
}
