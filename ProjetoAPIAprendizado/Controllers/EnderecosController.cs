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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoResponseDTO>>> GetEndereco()
        {

            var enderecos = await _repository.GetEnderecoAsync();
            var retornoEndereco = _mapper.Map<List<EnderecoResponseDTO>>(enderecos);
            return Ok(retornoEndereco);
        }




    }
}
