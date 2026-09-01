using ProjetoAPIAprendizado.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPIAprendizado.DTOs
{
    public class EnderecoCreateDTO
    {
        [Required(ErrorMessage = "A Rua é obrigatória!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome da rua deve ter entre 3 e 100 caracteres.")]
        public string Rua { get; set; }
        
        [Range(1, 99999, ErrorMessage = "O número deve ser maior que zero.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do bairro deve ter entre 3 e 100 caracteres.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Você precisa informar o ID do Cliente dono deste endereço!")]
        [Range(1, int.MaxValue, ErrorMessage = "O número deve ser maior que zero e igual ao ID do cliente do qual deseja adicionar o endereco.")]
        public int ClienteId { get; set; }

    }
}
