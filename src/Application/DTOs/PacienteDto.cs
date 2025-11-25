namespace API.DTOs;

public record PacienteDto(
    string Dni = "",
    string Nombre = "",
    string Apellido = "",
    DateTime FechaNacimiento = default
);
