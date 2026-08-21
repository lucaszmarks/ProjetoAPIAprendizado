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
        public EnderecosController(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEndereco(EnderecoCreateDTO novoEnderecoDTO)
        {
            var endereco = new Endereco
            {
                Rua = novoEnderecoDTO.Rua,
                Numero = novoEnderecoDTO.Numero,
                Bairro = novoEnderecoDTO.Bairro,
                ClienteId = novoEnderecoDTO.ClienteId
            };

            var enderecoCriado = await _repository.CreateEnderecoAsync(endereco);
            return Ok(enderecoCriado);

        }




    }
}
