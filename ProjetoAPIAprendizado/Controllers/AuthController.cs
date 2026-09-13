

using Microsoft.IdentityModel.Tokens;
using ProjetoAPIAprendizado.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoAPIAprendizado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            // 1. Validamos o usuário e senha (simulando uma checagem no banco)
            if (login.Username == "admin" && login.Password == "senha123") {

                // 2. Preparamos a "Caneta" com a nossa Chave Secreta exata do Program.cs
                var key = Encoding.UTF8.GetBytes("ChaveSecretaDaSuaAPI-PrecisaSerLonga123!@#");
                
                // 3. Desenhamos o Crachá (Token)
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.Username) 
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),  // O crachá vale por 2 horas
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                // 4. Fabricamos e entregamos o Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            // Se errar a senha, barrado na porta
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
        }

            
    }
}
