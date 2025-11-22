namespace ProjetoAPIAprendizado.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; protected set; }
        public string Cpf { get; protected set; }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public void AtualizarDados(string novoNome, string novoCpf)
        {
            Cpf = novoCpf;
            Nome = novoNome;
        }
    }
}
