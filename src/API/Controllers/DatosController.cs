using Application.Solicitudes;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatosController : ControllerBase
    {
        private readonly ConsultarDatos _consultar;
        public DatosController(ConsultarDatos consultar) => _consultar = consultar;

        [HttpGet("pacientes")]
        public async Task<IActionResult> GetPacientes() => Ok(await _consultar.GetPacientesAsync());

        [HttpGet("medicos")]
        public async Task<IActionResult> GetMedicos() => Ok(await _consultar.GetMedicosAsync());

        [HttpGet("prestadores")]
        public async Task<IActionResult> GetPrestadores() => Ok(await _consultar.GetPrestadoresAsync());

        [HttpGet("estudios")]
        public async Task<IActionResult> GetEstudios() => Ok(await _consultar.GetEstudiosAsync());
    }
}
