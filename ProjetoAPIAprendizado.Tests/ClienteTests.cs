using Xunit;
using ProjetoAPIAprendizado.Models; 


namespace ProjetoAPIAprendizado.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void TestarSeAtualizarDadosMudaONome()
        {
            var cliente = new Cliente("Lucas antigo", "123");

            cliente.AtualizarDados("Lucas Novo", "123");

            Assert.Equal("Lucas Novo", cliente.Nome);

        }
    }
}