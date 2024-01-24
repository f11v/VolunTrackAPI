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
    public class RecompensaController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public RecompensaController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/Recompensa/ListarRecompensas
        [HttpGet]
        [Route("ListarRecompensas")]
        public async Task<IActionResult> ListarRecompensas()
        {
            List<Recompensa> recompensas = await _context.Recompensas.ToListAsync();
            return Ok(recompensas);
        }

        // GET: api/Recompensa/BuscarRecompensa/5
        [HttpGet]
        [Route("BuscarRecompensa")]
        public async Task<IActionResult> ObtenerRecompensa(int id)
        {
            var recompensa = await _context.Recompensas.FindAsync(id);

            if (recompensa == null)
            {
                return NotFound();
            }

            return Ok(recompensa);
        }

        // PUT: api/Recompensa/EditarRecompensa/5
        [HttpPut]
        [Route("EditarRecompensa")]
        public async Task<IActionResult> EditarRecompensa(int id, [FromQuery] string Título, [FromQuery] string Detalle)
        {
            var recompensa = await _context.Recompensas.FindAsync(id);

            if (recompensa == null)
            {
                return NotFound();
            }

            recompensa.Título = Título;
            recompensa.Detalle = Detalle;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Recompensa actualizada",
                result = recompensa
            });
        }

        // POST: api/Recompensa/RegistrarRecompensa
        [HttpPost]
        [Route("RegistrarRecompensa")]
        public async Task<IActionResult> RegistrarRecompensa([FromQuery] string Título, [FromQuery] string Detalle)
        {
            var recompensa = new Recompensa
            {
                Título = Título,
                Detalle = Detalle
            };

            _context.Recompensas.Add(recompensa);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Recompensa registrada",
                result = recompensa
            });
        }

        // DELETE: api/Recompensa/EliminarRecompensa/5
        [HttpDelete]
        [Route("EliminarRecompensa")]
        public async Task<IActionResult> EliminarRecompensa(int id)
        {
            var recompensa = await _context.Recompensas.FindAsync(id);

            if (recompensa == null)
            {
                return NotFound();
            }

            _context.Recompensas.Remove(recompensa);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Recompensa eliminada",
                result = recompensa
            });
        }
    }
}
