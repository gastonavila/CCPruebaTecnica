using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PrestadorRepository : IPrestadorRepository
{
    private readonly AppDbContext _context;

    public PrestadorRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Prestador?> GetByIdAsync(int id)
        => _context.Prestadores.FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<Prestador>> GetAllAsync()
    => _context.Prestadores.AsNoTracking().ToListAsync();
}
