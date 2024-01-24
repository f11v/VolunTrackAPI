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
    public class EventoController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public EventoController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/Evento/ListarEventos
        [HttpGet]
        [Route("ListarEventos")]
        public async Task<IActionResult> ListarEventos()
        {
            List<Evento> eventos = await _context.Eventos.ToListAsync();
            return Ok(eventos);
        }

        // GET: api/Evento/BuscarEvento/5
        [HttpGet]
        [Route("BuscarEvento")]
        public async Task<IActionResult> ObtenerEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // PUT: api/Evento/EditarEvento/5
        [HttpPut]
        [Route("EditarEvento")]
        public async Task<IActionResult> EditarEvento(int id, [FromQuery] string Título, [FromQuery] DateOnly Fecha,
            [FromQuery] string Lugar, [FromQuery] string Descripcion, [FromQuery] TimeOnly? Hora)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Título = Título;
            evento.Fecha = Fecha;
            evento.Lugar = Lugar;
            evento.Descripcion = Descripcion;
            evento.Hora = Hora;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Evento actualizado",
                result = evento
            });
        }

        // POST: api/Evento/RegistrarEvento
        [HttpPost]
        [Route("RegistrarEvento")]
        public async Task<IActionResult> RegistrarEvento([FromQuery] string Título, [FromQuery] DateOnly Fecha,
            [FromQuery] string Lugar, [FromQuery] string Descripcion, [FromQuery] TimeOnly? Hora)
        {
            var evento = new Evento
            {
                Título = Título,
                Fecha = Fecha,
                Lugar = Lugar,
                Descripcion = Descripcion,
                Hora = Hora
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Evento registrado",
                result = evento
            });
        }

        // DELETE: api/Evento/EliminarEvento/5
        [HttpDelete]
        [Route("EliminarEvento")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Evento eliminado",
                result = evento
            });
        }
    }
}
