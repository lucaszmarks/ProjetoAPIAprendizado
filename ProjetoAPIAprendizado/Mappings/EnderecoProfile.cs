using AutoMapper;
using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.DTOs;
namespace ProjetoAPIAprendizado.Mappings
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile() 
        {    
            CreateMap<EnderecoCreateDTO, Endereco>();
            CreateMap<Endereco, EnderecoResponseDTO>();
        }
    }
}
