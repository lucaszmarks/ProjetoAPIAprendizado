using Microsoft.AspNetCore.Mvc;
using ProjetoAPIAprendizado.Models; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProjetoAPIAprendizado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ApiDbContext _contexto;

        
        public ClientesController(ApiDbContext contexto)
        {
            _contexto = contexto;
        }
        //Função Get, para vizualizar os dados
        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _contexto.Clientes.ToList();
            return Ok(clientes);
        }
        //Função Create, criação de dados
        [HttpPost]
        public IActionResult CriarCliente([FromBody] Cliente novoCliente)
        {

            _contexto.Clientes.Add(novoCliente);

            _contexto.SaveChanges();

            return Ok(novoCliente);
        }
        //Função Delete, deletar dados
        [HttpDelete("{id}")]
        public IActionResult DeleteClientes(int id) {
            var clienteEncontrado = _contexto.Clientes.Find(id);

            if (clienteEncontrado == null)
            {
                return NotFound();
            }
            _contexto.Clientes.Remove(clienteEncontrado);
            _contexto.SaveChanges();
            return Ok(clienteEncontrado); 
        }

        //Função Update, atualizar dados
        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, [FromBody] Cliente clienteAtualizado)
        {
            var clienteExistente = _contexto.Clientes.Find(id);

            if ( clienteExistente == null)
            {
                return NotFound();

            }
            clienteExistente.AtualizarDados(clienteAtualizado.Nome, clienteAtualizado.Cpf);
            _contexto.SaveChanges();
            return Ok(clienteExistente);
        }


    }
}
