

using Microsoft.IdentityModel.Tokens;
using ProjetoAPIAprendizado.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ProjetoAPIAprendizado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager) // Construtor com Injeção de Dependência
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            // Prepara a "ficha" do novo usuário
            var identityUser = new IdentityUser
            {
                UserName = requestDto.Username,
                Email = requestDto.Username
            };

            // Tenta criar o usuário. O UserManager já faz o hash da senha automaticamente aqui!
            var identityResult = await _userManager.CreateAsync(identityUser, requestDto.Password);

            if (identityResult.Succeeded)
            {
                //Se o usuário foi criado e vieram cargos (Roles) na requisição, nós fazemos o vínculo
                if (requestDto.Roles != null && requestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRoleAsync(identityUser, requestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok(new { mensagem = "Usuário registrado com sucesso e cargos vinculados!" });

                    }

                }
                else
                {
                    return Ok(new { mensagem = "Usuário registrado com sucesso!" });
                }
            }
            //Se algo der errado (ex: e-mail já existe, senha muito curta), devolvemos os erros nativos do Identity
            return BadRequest();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO requestDto)
        {
            // Busca o usuário no banco pelo e-mail (username)
            var user = await _userManager.FindByNameAsync(requestDto.Username);

            if (user != null)
            {
                // O Identity faz a mágica de pegar a senha que veio no requestDto, 
                // aplicar o hash e comparar com o que está salvo no banco
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, requestDto.Password);

                if (checkPasswordResult)
                {
                    // 3. Descobre quais cargos (Roles) esse usuário possui
                    var roles = await _userManager.GetRolesAsync(user);

                    // ==========================================================
                    // AQUI ENTRA O CÓDIGO DE GERAR O TOKEN JWT
                    // Agora vai passar a variável 'roles' (que buscamos acima) 
                    // para o gerador de token, no lugar dos cargos fixos.
                    // ==========================================================

                    var jwtToken = GerarToken(user.UserName, roles);

                    return Ok(new
                    {
                        mensagem = "Login bem-sucedido!",
                        token = jwtToken
                    });
                }
            }

            // Retorna 401 Unauthorized se o usuário não existir ou a senha estiver errada
            return Unauthorized("Usuário ou senha inválidos.");


        }
        private string GerarToken(string username, IList<string> roles) // Recebe uma lista de cargos
        {
            var key = Encoding.UTF8.GetBytes("ChaveSecretaDaSuaAPI-PrecisaSerLonga123!@#"); // Use a sua chave real aqui

            // Começamos a lista de Claims (carimbos do passaporte) com o nome do usuário
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };

            // Adicionamos uma Claim de 'Role' para CADA cargo que vier do banco de dados
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
