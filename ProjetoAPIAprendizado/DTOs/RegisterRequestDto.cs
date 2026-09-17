using System.ComponentModel.DataAnnotations;

namespace ProjetoAPIAprendizado.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Recebendo os cargos em formato de lista (Array) para o usuário poder ser Admin e User ao mesmo tempo, se necessário
        public string Roles { get; set; }

    }
}
