using Domain.Entities;

namespace Application.Interfaces;

public interface IPrestadorRepository
{
    Task<Prestador?> GetByIdAsync(int id);
    Task<List<Prestador>> GetAllAsync();
}
