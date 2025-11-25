namespace API.DTOs;

public record EstudioDto(
    string Codigo = "",
    string Descripcion = "",
    DateTime FechaSolicitud = default
);
