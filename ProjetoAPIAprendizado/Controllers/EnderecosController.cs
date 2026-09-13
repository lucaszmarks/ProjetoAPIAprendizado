using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPIAprendizado.DTOs;
using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.Repositories;

namespace ProjetoAPIAprendizado.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly IEnderecoRepository _repository;
        private readonly IMapper _mapper;
        public EnderecosController(IEnderecoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Cria um novo endereco no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Enderecos
        ///     {
        ///        "rua": "correia de godoi",
        ///        "numero": "123",
        ///        "bairro": "itaquera",
        ///        "clienteid": "1"
        ///     }
        ///
        /// </remarks>
        /// <param name="novoEnderecoDTO">Objeto contendo a Rua, o Numero, o Bairro e o Id do cliente do novo endereco.</param>
        /// <returns>O endereco recém-criado com seu ID gerado.</returns>
        /// <response code="201">Retorna o endereco recém-criado com sucesso.</response>
        /// <response code="401">Se o usuário não está logado.</response>
        /// <response code="400">Se ocorrer um erro.</response>
        [HttpPost]
        public async Task<IActionResult> CreateEndereco(EnderecoCreateDTO novoEnderecoDTO)
        {
            var endereco = _mapper.Map<Endereco>(novoEnderecoDTO);
            await _repository.CreateEnderecoAsync(endereco);


            return Created($"/api/Enderecos/{endereco.Id}", endereco);
        }
        /// <summary>
        /// Busca um endereco específico através do seu ID.
        /// </summary>
        /// <param name="id">O ID do endereco que você deseja buscar.</param>
        /// <returns>Os detalhes do endereco solicitado.</returns>
        /// <response code="200">Retorna o endereco encontrado com sucesso.</response>
        /// <response code="401">Se o usuário não está logado.</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoResponseDTO>> GetEnderecoById(int id)
        {
            var enderecoPorId = await _repository.GetEnderecoByIdAsync(id);
            if (enderecoPorId == null) return NotFound();
            var enderecoRetorno = _mapper.Map<EnderecoResponseDTO>(enderecoPorId);
            return Ok(enderecoRetorno);
        }
        /// <summary>
        /// Retorna uma lista de todos os endereços cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoResponseDTO>>> GetEndereco()
        {

            var enderecos = await _repository.GetEnderecoAsync();
            var retornoEndereco = _mapper.Map<List<EnderecoResponseDTO>>(enderecos);
            return Ok(retornoEndereco);
        }
        /// <summary>
        /// Exclui um endereco permanentemente do sistema.
        /// </summary>
        /// <param name="id">O ID do endereco que será excluído.</param>
        /// <response code="204">Endereco excluído com sucesso (sem retorno de conteúdo).</response>
        /// <response code="401">Se o usuário está logado.</response>
        /// <response code="403">Se o usuário não tem permissão para realizar a requisição.</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEndereco(int id)
        {

            bool sucesso = await _repository.DeleteEnderecoAsync(id);

            if (!sucesso)
            {

                return NotFound(new { mensagem = "Endereco não encontrado para exclusão." });
            }


            return NoContent();
        }
        /// <summary>
        /// Atualiza os dados de um endereco existente.
        /// </summary>
        /// <param name="id">O ID do endereco que será atualizado.</param>
        /// <param name="enderecoAtualizadoDto">Objeto contendo os novos dados do endereco.</param>
        /// <response code="204">Endereco atualizado com sucesso (sem retorno de conteúdo).</response>
        /// <response code="401">Se o usuário não está logado.</response>
        /// <response code="404">Se o ID informado não existir no banco de dados.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Endereco>> UpdateEndereco(int id, [FromBody] EnderecoCreateDTO enderecoAtualizadoDto)
        {
            var enderecoExistente = await _repository.GetEnderecoByIdAsync(id);
            if (enderecoExistente == null)
            {
                return NotFound(new { mensagem = "Endereco não encontrado." });
            }

            _mapper.Map(enderecoAtualizadoDto, enderecoExistente);

            await _repository.UpdateEnderecoAsync(enderecoExistente);

            return NoContent();
        }


    }
}
