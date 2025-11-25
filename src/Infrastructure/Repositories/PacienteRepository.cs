using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly AppDbContext _context;

    public PacienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Paciente?> GetByDniAsync(string dni)
        => _context.Pacientes.FirstOrDefaultAsync(x => x.Dni == dni);

    public Task<Paciente> AddAsync(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        return Task.FromResult(paciente);
    }
    public Task<List<Paciente>> GetAllAsync()
    => _context.Pacientes.AsNoTracking().ToListAsync();
}
