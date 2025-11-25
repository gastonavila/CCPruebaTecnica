using Application.Interfaces;
using Domain.Entities;


namespace Application.Solicitudes;
public class ConsultarDatos
{
    private readonly IPacienteRepository _pacientes;
    private readonly IMedicoRepository _medicos;
    private readonly IPrestadorRepository _prestadores;
    private readonly IEstudioRepository _estudios;

    public ConsultarDatos(
        IPacienteRepository pacientes,
        IMedicoRepository medicos,
        IPrestadorRepository prestadores,
        IEstudioRepository estudios)
    {
        _pacientes = pacientes;
        _medicos = medicos;
        _prestadores = prestadores;
        _estudios = estudios;
    }

    public Task<List<Paciente>> GetPacientesAsync() => _pacientes.GetAllAsync();
    public Task<List<Medico>> GetMedicosAsync() => _medicos.GetAllAsync();
    public Task<List<Prestador>> GetPrestadoresAsync() => _prestadores.GetAllAsync();
    public Task<List<Estudio>> GetEstudiosAsync() => _estudios.GetAllAsync();
}