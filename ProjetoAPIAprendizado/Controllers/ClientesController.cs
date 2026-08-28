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
        public async Task<ActionResult<ClienteResponseDTO>> GetClienteById(int id)
        {
            var clientePorId = await _repositorio.GetClienteByIdAsync(id);
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
            bool cpfExiste = await _repositorio.CheckCpfExistsAsync(novoClienteDto.Cpf);

            if (cpfExiste)
            {
                return Conflict(new { mensagem = "Operação negada: Este CPF já está cadastrado no sistema." });
            }
            var cliente = _mapper.Map<Cliente>(novoClienteDto);

            await _repositorio.CreateClienteAsync(cliente);

            
            return CreatedAtAction(nameof(GetClientes), new { id = cliente.Id }, cliente);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            
            bool sucesso = await _repositorio.DeleteClienteAsync(id);

            if (!sucesso)
            {
                
                return NotFound(new { mensagem = "Cliente não encontrado para exclusão." }); 
            }

            
            return NoContent();
        }

        //Função Update, atualizar dados
        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> UpdateCliente(int id, [FromBody] ClienteCreateDTO clienteAtualizadoDto)
        {
            var clienteExistente = await _repositorio.GetClienteByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado." });
            }

            _mapper.Map(clienteAtualizadoDto, clienteExistente);

            await _repositorio.UpdateClienteAsync(clienteExistente);

            return NoContent();
        }


    }
}
