using AutoMapper;
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

        [HttpPost]
        public async Task<IActionResult> CreateEndereco(EnderecoCreateDTO novoEnderecoDTO)
        {
            var endereco = _mapper.Map<Endereco>(novoEnderecoDTO);
            await _repository.CreateEnderecoAsync(endereco);


            return Created($"/api/Enderecos/{endereco.Id}", endereco);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoResponseDTO>> GetEnderecoById(int id)
        {
            var enderecoPorId = await _repository.GetEnderecoByIdAsync(id);
            if (enderecoPorId == null) return NotFound();
            var enderecoRetorno = _mapper.Map<EnderecoResponseDTO>(enderecoPorId);
            return Ok(enderecoRetorno);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoResponseDTO>>> GetEndereco()
        {

            var enderecos = await _repository.GetEnderecoAsync();
            var retornoEndereco = _mapper.Map<List<EnderecoResponseDTO>>(enderecos);
            return Ok(retornoEndereco);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEndereco(int id)
        {

            bool sucesso = await _repository.DeleteEnderecoAsync(id);

            if (!sucesso)
            {

                return NotFound(new { mensagem = "Endereco não encontrado para exclusão." });
            }


            return NoContent();
        }

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
