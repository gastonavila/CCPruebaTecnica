using Domain.Entities;

namespace Infrastructure.Persistence.Seed;

public class SeedModel
{
    public List<Paciente> Pacientes { get; set; }
    public List<Medico> Medicos { get; set; }
    public List<Prestador> Prestadores { get; set; }
    public List<Estudio> Estudios { get; set; }
}
