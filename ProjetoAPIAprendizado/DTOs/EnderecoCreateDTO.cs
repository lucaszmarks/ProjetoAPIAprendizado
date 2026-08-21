using ProjetoAPIAprendizado.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPIAprendizado.DTOs
{
    public class EnderecoCreateDTO
    {
        [Required(ErrorMessage = "A Rua é obrigatória!")]
        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        [Required(ErrorMessage = "Você precisa informar o ID do Cliente dono deste endereço!")]
        public int ClienteId { get; set; }

    }
}
