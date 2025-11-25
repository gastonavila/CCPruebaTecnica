using Domain.Entities;

namespace Application.Interfaces;

public interface IPacienteRepository
{
    Task<Paciente?> GetByDniAsync(string dni);
    Task<Paciente> AddAsync(Paciente paciente);
    Task<List<Paciente>> GetAllAsync();
}
