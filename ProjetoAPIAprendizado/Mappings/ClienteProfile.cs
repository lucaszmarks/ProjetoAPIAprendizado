using AutoMapper;
using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.DTOs;

namespace ProjetoAPIAprendizado.Mappings
{
    public class ClienteProfile : Profile 
    {
        public ClienteProfile()
        {
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<Cliente, ClienteResponseDTO>();
        }


    }
}
