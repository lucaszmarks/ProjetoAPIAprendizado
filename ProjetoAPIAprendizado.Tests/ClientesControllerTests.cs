using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoAPIAprendizado.Controllers;
using ProjetoAPIAprendizado.DTOs;
using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoAPIAprendizado.Tests
{
    public class ClientesControllerTests
    {
        [Fact]
        public async Task GetClienteById_DeveRetornarNotFound_QuandoClienteNaoExiste()
        {
            int idInexistente = 99;

            var mockRepo = new Mock<IClienteRepository>();

            mockRepo.Setup(repo => repo.GetClienteByIdAsync(idInexistente)).ReturnsAsync((Cliente)null);

            var mockMapper = new Mock<IMapper>();

            var controller = new ClientesController(mockRepo.Object, mockMapper.Object);


            var resultado = await controller.GetClienteById(idInexistente);


            Assert.IsType<NotFoundResult>(resultado.Result);
        }
        [Fact]
        public async Task GetClienteById_DeveRetornarOk_QuandoClienteExiste()
        {
            // 1. ARRANGE (Preparação)
            int idExistente = 1;

            // Criamos objetos "falsos" em memória para simular o que viria do banco
            var clienteFake = new Cliente("Fulano", "12345678901");
            clienteFake.Id = idExistente;
            var dtoFake = new ClienteResponseDTO { Id = idExistente, Nome = "Fulano", Cpf = "12345678901" };

            var mockRepo = new Mock<IClienteRepository>();
            // Ensinamos o dublê: "Se pedirem o ID 1, devolva o clienteFake"
            mockRepo.Setup(repo => repo.GetClienteByIdAsync(idExistente)).ReturnsAsync(clienteFake);

            var mockMapper = new Mock<IMapper>();
            // Ensinamos o dublê: "Se pedirem para mapear o clienteFake, devolva o dtoFake"
            mockMapper.Setup(m => m.Map<ClienteResponseDTO>(clienteFake)).Returns(dtoFake);

            var controller = new ClientesController(mockRepo.Object, mockMapper.Object);

            // 2. ACT (Ação)
            var resultado = await controller.GetClienteById(idExistente);

            // 3. ASSERT (Verificação)
            // 3.1 - Garante que o retorno foi um código 200 (OK)
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);

            // 3.2 - Garante que os dados dentro do OK são do tipo ClienteResponseDTO
            var clienteRetornado = Assert.IsType<ClienteResponseDTO>(okResult.Value);

            // 3.3 - Garante que os dados são exatamente os que esperamos
            Assert.Equal(idExistente, clienteRetornado.Id);
            Assert.Equal("Fulano", clienteRetornado.Nome);
        }
    }
}