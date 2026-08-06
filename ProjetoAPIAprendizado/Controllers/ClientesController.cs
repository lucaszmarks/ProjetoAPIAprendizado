using Microsoft.AspNetCore.Mvc;
using ProjetoAPIAprendizado.Models;

using ProjetoAPIAprendizado.DTOs;
using ProjetoAPIAprendizado.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProjetoAPIAprendizado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repositorio;
        public ClientesController(IClienteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        //Função Get, para vizualizar os dados
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> SearchById(int id)
        {
            var clientePorId = await _repositorio.SearchByIdAsync(id);
            if (clientePorId == null) return NotFound();
            return Ok(clientePorId);
        }
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            var clientes = await _repositorio.GetClientesAsync();
            return Ok(clientes);
        }
        //Função Create, criação de dados
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] ClienteCreateDTO novoClienteDto)
        {
            Cliente novoCliente = new Cliente(novoClienteDto.Nome,novoClienteDto.Cpf);
            var clienteCriado = await _repositorio.CreateClienteAsync(novoCliente);
            return Ok(clienteCriado);
        }
        //Função Delete, deletar dados
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> RemoveCliente(int id)
        {
            var clienteRemovido = await _repositorio.RemoveClienteAsync(id);

            if (clienteRemovido == null)
            {
                return NotFound();
            }
            
            return Ok(clienteRemovido);
        }

        //Função Update, atualizar dados
        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> UpdateClienteAsync(int id, [FromBody] ClienteCreateDTO novoClienteDto)
        {
            Cliente novoCliente = new Cliente(novoClienteDto.Nome, novoClienteDto.Cpf);
            var clienteAtualizado = await _repositorio.UpdateClienteAsync(id, novoCliente);

            if (clienteAtualizado == null)
            {
                return NotFound();

            }
            return Ok(clienteAtualizado);
        }


    }
}
