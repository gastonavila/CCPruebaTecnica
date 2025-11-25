using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EstudioRepository : IEstudioRepository
{
    private readonly AppDbContext _context;

    public EstudioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Estudio> AddAsync(Estudio estudio)
    {
        _context.Estudios.Add(estudio);
        return Task.FromResult(estudio);
    }
    public Task<List<Estudio>> GetAllAsync()
    => _context.Estudios.AsNoTracking().ToListAsync();
}
