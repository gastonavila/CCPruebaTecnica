namespace API.DTOs;

public record CrearSolicitudDto(
    int SolicitudId = 0,
    PacienteDto Paciente = null,
    EstudioDto Estudio = null,
    MedicoDto Medico = null,
    int PrestadorId = 0
);
