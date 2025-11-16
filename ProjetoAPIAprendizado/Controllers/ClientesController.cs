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
        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _contexto.Clientes.ToList();
            return Ok(clientes);
        }
        [HttpPost]
        public IActionResult CriarCliente([FromBody] Cliente novoCliente)
        {
            
            _contexto.Clientes.Add(novoCliente);

            _contexto.SaveChanges();

            return Ok(novoCliente);
        }

    }
}
