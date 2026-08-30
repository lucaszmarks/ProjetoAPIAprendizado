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
        /// <summary>
        /// Busca um cliente específico através do seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente que você deseja buscar.</param>
        /// <returns>Os detalhes do cliente solicitado.</returns>
        /// <response code="200">Retorna o cliente encontrado com sucesso.</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDTO>> GetClienteById(int id)
        {
            var clientePorId = await _repositorio.GetClienteByIdAsync(id);
            if (clientePorId == null) return NotFound();
            var clientesRetorno = _mapper.Map<ClienteResponseDTO>(clientePorId);
            return Ok(clientesRetorno);
        }
        /// <summary>
        /// Retorna uma lista de clientes paginada.
        /// </summary>
        /// <param name="numeroPagina">O número da página que você deseja visualizar (padrão: 1)</param>
        /// <param name="tamanhoPagina">Quantos clientes devem aparecer por página (padrão: 5)</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> GetClientes([FromQuery] int numeroPagina = 1,
        [FromQuery] int tamanhoPagina = 5) 
        
        {
            var clientes = await _repositorio.GetClientesAsync(numeroPagina, tamanhoPagina);
            
            var clientesRetorno = _mapper.Map<List<ClienteResponseDTO>>(clientes);
            
            return Ok(clientesRetorno);
        }

        //Função Create, criação de dados
        /// <summary>
        /// Cria um novo cliente no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Clientes
        ///     {
        ///        "nome": "Lucas",
        ///        "cpf": "12345678901"
        ///     }
        ///
        /// </remarks>
        /// <param name="novoClienteDto">Objeto contendo o Nome e o CPF do novo cliente.</param>
        /// <returns>O cliente recém-criado com seu ID gerado.</returns>
        /// <response code="201">Retorna o cliente recém-criado com sucesso.</response>
        /// <response code="400">Se o CPF já estiver cadastrado ou os dados forem inválidos.</response>
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
        //Função Delete, remoção de dados
        /// <summary>
        /// Exclui um cliente permanentemente do sistema.
        /// </summary>
        /// <param name="id">O ID do cliente que será excluído.</param>
        /// <response code="204">Cliente excluído com sucesso (sem retorno de conteúdo).</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
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
        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente que será atualizado.</param>
        /// <param name="clienteAtualizadoDto">Objeto contendo os novos dados do cliente.</param>
        /// <response code="204">Cliente atualizado com sucesso (sem retorno de conteúdo).</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
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
