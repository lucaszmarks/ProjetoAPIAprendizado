namespace ProjetoAPIAprendizado.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }

        public int Numero { get; set; }
        public string Bairro { get; set; }
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

    }
}
