using ProjetoAPIAprendizado.Models;
using System;
using System.ComponentModel.DataAnnotations;
namespace ProjetoAPIAprendizado.DTOs 
{

    public class ClienteCreateDTO
    {

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo de caracteres = 100")]
        public string Nome { get;  set; }

        [Required(ErrorMessage = "CPF Obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF tem obrigatoriamente 11 números")]
        public string Cpf { get;  set; }

        
    }
}