using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MedicoRepository : IMedicoRepository
{
    private readonly AppDbContext _context;

    public MedicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Medico?> GetByMatriculaAsync(string matricula)
        => _context.Medicos.FirstOrDefaultAsync(x => x.Matricula == matricula);

    public Task<Medico> AddAsync(Medico medico)
    {
        _context.Medicos.Add(medico);
        return Task.FromResult(medico);
    }

    public Task UpdateAsync(Medico medico)
    {
        _context.Medicos.Update(medico);
        return Task.CompletedTask;
    }
    public Task<List<Medico>> GetAllAsync()
    => _context.Medicos.AsNoTracking().ToListAsync();
}
