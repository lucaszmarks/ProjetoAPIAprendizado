namespace ProjetoAPIAprendizado.Models
{
    public class Cliente
    {
        public string Nome { get; protected set; }
        public string Cpf { get; protected set; }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }
    }
}
