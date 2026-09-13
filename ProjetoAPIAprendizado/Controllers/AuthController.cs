

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
            if (login.Username == "admin" && login.Password == "senha123")
            {
                return Ok(new { token = GerarToken(login.Username, "Admin") });
            }
            else if (login.Username == "operador" && login.Password == "senha123")
            {
                return Ok(new { token = GerarToken(login.Username,"User")});
            }
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });


        }
        private string GerarToken(string username, string role)
        {
            var key = Encoding.UTF8.GetBytes("ChaveSecretaDaSuaAPI-PrecisaSerLonga123!@#"); // Lembre-se de usar a SUA chave exata aqui

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role) // Pega o cargo dinamicamente
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    }
