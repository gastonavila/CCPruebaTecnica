using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicoRepository
{
    Task<Medico?> GetByMatriculaAsync(string matricula);
    Task<Medico> AddAsync(Medico medico);
    Task UpdateAsync(Medico medico);
    Task<List<Medico>> GetAllAsync();
}
