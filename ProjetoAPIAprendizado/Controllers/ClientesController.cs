using Microsoft.AspNetCore.Mvc;
using ProjetoAPIAprendizado.Models;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public ClientesController(IClienteRepository repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        //Função Get, para vizualizar os dados
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDTO>> SearchById(int id)
        {
            var clientePorId = await _repositorio.SearchByIdAsync(id);
            if (clientePorId == null) return NotFound();
            var clientesRetorno = _mapper.Map<ClienteResponseDTO>(clientePorId);
            return Ok(clientesRetorno);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> GetClientes([FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 5) 
        
        {
            var clientes = await _repositorio.GetClientesAsync(numeroPagina, tamanhoPagina);
            
            var clientesRetorno = _mapper.Map<List<ClienteResponseDTO>>(clientes);
            
            return Ok(clientesRetorno);
        }
        //Função Create, criação de dados
        [HttpPost]
        public async Task<ActionResult<Cliente>> CriarCliente([FromBody] ClienteCreateDTO novoClienteDto)
        {
            
            var cliente = _mapper.Map<Cliente>(novoClienteDto);

            await _repositorio.CreateClienteAsync(cliente);

            
            return CreatedAtAction(nameof(GetClientes), new { id = cliente.Id }, cliente);
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
