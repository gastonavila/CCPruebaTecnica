using Application.Solicitudes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly CrearSolicitudHandler _handler;

    public SolicitudesController(CrearSolicitudHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudCommand request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var id = await _handler.Handle(request);
            return CreatedAtAction(null, new { id, message = "Estudio creado correctamente." });
        }
        catch (InvalidOperationException ex) // caso prestador no existe
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (DbUpdateException)
        {
            return Conflict(new { error = "Conflicto de datos (posible DNI duplicado)." });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Error interno." });
        }
    }
}
