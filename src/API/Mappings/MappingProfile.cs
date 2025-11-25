using API.DTOs;
using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PacienteDto, Paciente>();
        CreateMap<MedicoDto, Medico>();
        CreateMap<EstudioDto, Estudio>();
    }
}
