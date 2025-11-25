using API.DTOs;

namespace Application.Solicitudes;
using System.ComponentModel.DataAnnotations;

public class CrearSolicitudCommand
{
    public int SolicitudId { get; set; } = 0;
    [Required]
    public PacienteDto Paciente { get; set; } = new PacienteDto();
    [Required]
    public EstudioDto Estudio { get; set; } = new EstudioDto();
    [Required]
    public MedicoDto Medico { get; set; } = new MedicoDto();
    [Range(1, int.MaxValue)]
    public int PrestadorId { get; set; } = 0;

    public CrearSolicitudCommand() { }

    public CrearSolicitudCommand(int solicitudId,
                                 PacienteDto paciente,
                                 EstudioDto estudio,
                                 MedicoDto medico,
                                 int prestadorId)
    {
        SolicitudId = solicitudId;
        Paciente = paciente;
        Estudio = estudio;
        Medico = medico;
        PrestadorId = prestadorId;
    }
}
