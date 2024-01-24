using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST.Models;

namespace API_REST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VoluntariadoController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public VoluntariadoController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/Voluntariado/ListarVoluntariados
        [HttpGet]
        [Route("ListarVoluntariados")]
        public async Task<IActionResult> ListarVoluntariados()
        {
            List<Voluntariado> voluntariados = await _context.Voluntariados.ToListAsync();
            return Ok(voluntariados);
        }

        // GET: api/Voluntariado/BuscarVoluntariado/5
        [HttpGet]
        [Route("BuscarVoluntariado")]
        public async Task<IActionResult> ObtenerVoluntariado(int id)
        {
            var voluntariado = await _context.Voluntariados.FindAsync(id);

            if (voluntariado == null)
            {
                return NotFound();
            }

            return Ok(voluntariado);
        }

        // PUT: api/Voluntariado/EditarVoluntariado/5
        [HttpPut]
        [Route("EditarVoluntariado")]
        public async Task<IActionResult> EditarVoluntariado(int id, [FromQuery] string Nombre, [FromQuery] int DuracionMeses,
            [FromQuery] DateTime FechaInicio, [FromQuery] DateTime FechaFin, [FromQuery] string Lugar, [FromQuery] string Descripcion)
        {
            var voluntariado = await _context.Voluntariados.FindAsync(id);

            if (voluntariado == null)
            {
                return NotFound();
            }

            voluntariado.Nombre = Nombre;
            voluntariado.DuracionMeses = DuracionMeses;
            voluntariado.FechaInicio = FechaInicio;
            voluntariado.FechaFin = FechaFin;
            voluntariado.Lugar = Lugar;
            voluntariado.Descripcion = Descripcion;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Voluntariado actualizado",
                result = voluntariado
            });
        }

        // POST: api/Voluntariado/RegistrarVoluntariado
        [HttpPost]
        [Route("RegistrarVoluntariado")]
        public async Task<IActionResult> RegistrarVoluntariado([FromQuery] string Nombre, [FromQuery] int DuracionMeses,
            [FromQuery] DateTime FechaInicio, [FromQuery] DateTime FechaFin, [FromQuery] string Lugar, [FromQuery] string Descripcion)
        {
            var voluntariado = new Voluntariado
            {
                Nombre = Nombre,
                DuracionMeses = DuracionMeses,
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                Lugar = Lugar,
                Descripcion = Descripcion
            };

            _context.Voluntariados.Add(voluntariado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Voluntariado registrado",
                result = voluntariado
            });
        }

        // DELETE: api/Voluntariado/EliminarVoluntariado/5
        [HttpDelete]
        [Route("EliminarVoluntariado")]
        public async Task<IActionResult> EliminarVoluntariado(int id)
        {
            var voluntariado = await _context.Voluntariados.FindAsync(id);

            if (voluntariado == null)
            {
                return NotFound();
            }

            _context.Voluntariados.Remove(voluntariado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Voluntariado eliminado",
                result = voluntariado
            });
        }
    }
}
