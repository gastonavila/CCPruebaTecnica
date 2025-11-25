using Domain.Entities;

namespace Application.Interfaces;

public interface IEstudioRepository
{
    Task<Estudio> AddAsync(Estudio estudio);
    Task<List<Estudio>> GetAllAsync();
}
