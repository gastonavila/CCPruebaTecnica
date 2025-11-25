using Application.Interfaces;
using Domain.Entities;
using System.Text.RegularExpressions;

namespace Application.Solicitudes;

public class CrearSolicitudHandler
{
    private readonly IPacienteRepository _pacientes;
    private readonly IMedicoRepository _medicos;
    private readonly IPrestadorRepository _prestadores;
    private readonly IEstudioRepository _estudios;
    private readonly IUnitOfWork _unitOfWork;


    public CrearSolicitudHandler(
        IPacienteRepository pacientes,
        IMedicoRepository medicos,
        IPrestadorRepository prestadores,
        IEstudioRepository estudios,
        IUnitOfWork unitOfWork)
    {
        _pacientes = pacientes;
        _medicos = medicos;
        _prestadores = prestadores;
        _estudios = estudios;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CrearSolicitudCommand request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Paciente == null) throw new ArgumentException("Paciente requerido.");
        if (request.Medico == null) throw new ArgumentException("Medico requerido.");
        if (request.Estudio == null) throw new ArgumentException("Estudio requerido.");


        // 1. Paciente (buscar por DNI)
        var paciente = await _pacientes.GetByDniAsync(request.Paciente.Dni);
        if (paciente == null)
        {
            paciente = new Paciente
            {
                Dni = request.Paciente.Dni,
                Nombre = request.Paciente.Nombre,
                Apellido = request.Paciente.Apellido,
                FechaNacimiento = request.Paciente.FechaNacimiento
            };
            await _pacientes.AddAsync(paciente);
        }

        // 2. Médico (buscar por matrícula)
        long matriculaLong = request.Medico.Matricula;
        var matriculaRaw = Math.Abs(matriculaLong).ToString();
        var digitCount = matriculaRaw.Length;
        if (digitCount > 12)
            throw new ArgumentException("La matrícula no puede tener más de 12 dígitos.");

        // normalizar a 12 caracteres con ceros a la izquierda
        var matriculaPadded = matriculaRaw.ToString().PadLeft(12, '0');

        var medico = await _medicos.GetByMatriculaAsync(matriculaPadded);
        if (medico == null)
        {
            medico = new Medico
            {
                Nombre = request.Medico.Nombre,
                Matricula = matriculaPadded
            };
            await _medicos.AddAsync(medico);
        }
        else
        {
            medico.Nombre = request.Medico.Nombre;
            medico.Matricula = matriculaPadded;
            await _medicos.UpdateAsync(medico);
        }

        // 3. Prestador
        var prestador = await _prestadores.GetByIdAsync(request.PrestadorId);
        if (prestador == null)
            throw new InvalidOperationException("Prestador no existe.");

        // 4. Cálculo de edad correcto
        var today = DateOnly.FromDateTime(DateTime.Today);
        var nacimiento = DateOnly.FromDateTime(paciente.FechaNacimiento);
        var edad = today.Year - nacimiento.Year;
        if (nacimiento > today.AddYears(-edad)) edad--;

        /// 5. Transformación del código del estudio
        var codigo = request.Estudio.Codigo ?? string.Empty;
        if (edad > 48)
            codigo = "MONO-" + codigo;

        // 6. Crear estudio
        var estudio = new Estudio
        {
            Codigo = codigo,
            Descripcion = request.Estudio.Descripcion,
            FechaSolicitud = request.Estudio.FechaSolicitud,
            PacienteId = paciente.Id,
            MedicoId = medico.Id,
            PrestadorId = prestador.Id
        };
        await _estudios.AddAsync(estudio);
        await _unitOfWork.SaveChangesAsync();
        return estudio.Id;
    }
}
