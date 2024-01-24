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
    public class RegistroEventoController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public RegistroEventoController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistroEvento/ListarRegistrosEventos
        [HttpGet]
        [Route("ListarRegistrosEventos")]
        public async Task<IActionResult> ListarRegistrosEventos()
        {
            List<RegistroEvento> registros = await _context.RegistroEventos.ToListAsync();
            return Ok(registros);
        }

        // GET: api/RegistroEvento/BuscarRegistroEvento/5
        [HttpGet]
        [Route("BuscarRegistroEvento")]
        public async Task<IActionResult> ObtenerRegistroEvento(int id)
        {
            var registroEvento = await _context.RegistroEventos.FindAsync(id);

            if (registroEvento == null)
            {
                return NotFound();
            }

            return Ok(registroEvento);
        }

        // PUT: api/RegistroEvento/EditarRegistroEvento/5
        [HttpPut]
        [Route("EditarRegistroEvento")]
        public async Task<IActionResult> EditarRegistroEvento(int id, [FromQuery] int? IdEvento, [FromQuery] int? IdUsuario)
        {
            var registroEvento = await _context.RegistroEventos.FindAsync(id);

            if (registroEvento == null)
            {
                return NotFound();
            }

            registroEvento.IdEvento = IdEvento;
            registroEvento.IdUsuario = IdUsuario;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Registro de evento actualizado",
                result = registroEvento
            });
        }

        // POST: api/RegistroEvento/RegistrarRegistroEvento
        [HttpPost]
        [Route("RegistrarRegistroEvento")]
        public async Task<IActionResult> RegistrarRegistroEvento([FromQuery] int? IdEvento, [FromQuery] int? IdUsuario)
        {
            var registroEvento = new RegistroEvento
            {
                IdEvento = IdEvento,
                IdUsuario = IdUsuario
            };

            _context.RegistroEventos.Add(registroEvento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Registro de evento registrado",
                result = registroEvento
            });
        }

        // DELETE: api/RegistroEvento/EliminarRegistroEvento/5
        [HttpDelete]
        [Route("EliminarRegistroEvento")]
        public async Task<IActionResult> EliminarRegistroEvento(int id)
        {
            var registroEvento = await _context.RegistroEventos.FindAsync(id);

            if (registroEvento == null)
            {
                return NotFound();
            }

            _context.RegistroEventos.Remove(registroEvento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Registro de evento eliminado",
                result = registroEvento
            });
        }
    }
}
