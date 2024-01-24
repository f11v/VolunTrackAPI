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
    public class EstanteRecompensaController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public EstanteRecompensaController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/EstanteRecompensa/ListarEstanteRecompensas
        [HttpGet]
        [Route("ListarEstanteRecompensas")]
        public async Task<IActionResult> ListarEstanteRecompensas()
        {
            List<EstanteRecompensa> estantes = await _context.EstanteRecompensas.ToListAsync();
            return Ok(estantes);
        }

        // GET: api/EstanteRecompensa/BuscarEstanteRecompensa/5
        [HttpGet]
        [Route("BuscarEstanteRecompensa")]
        public async Task<IActionResult> ObtenerEstanteRecompensa(int id)
        {
            var estante = await _context.EstanteRecompensas.FindAsync(id);

            if (estante == null)
            {
                return NotFound();
            }

            return Ok(estante);
        }

        // PUT: api/EstanteRecompensa/EditarEstanteRecompensa/5
        [HttpPut]
        [Route("EditarEstanteRecompensa")]
        public async Task<IActionResult> EditarEstanteRecompensa(int id, [FromQuery] int? IdRecompensa, [FromQuery] int? IdUsuario)
        {
            var estante = await _context.EstanteRecompensas.FindAsync(id);

            if (estante == null)
            {
                return NotFound();
            }

            estante.IdRecompensa = IdRecompensa;
            estante.IdUsuario = IdUsuario;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Estante de recompensa actualizado",
                result = estante
            });
        }

        // POST: api/EstanteRecompensa/RegistrarEstanteRecompensa
        [HttpPost]
        [Route("RegistrarEstanteRecompensa")]
        public async Task<IActionResult> RegistrarEstanteRecompensa([FromQuery] int? IdRecompensa, [FromQuery] int? IdUsuario)
        {
            var estante = new EstanteRecompensa
            {
                IdRecompensa = IdRecompensa,
                IdUsuario = IdUsuario
            };

            _context.EstanteRecompensas.Add(estante);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Estante de recompensa registrado",
                result = estante
            });
        }

        // DELETE: api/EstanteRecompensa/EliminarEstanteRecompensa/5
        [HttpDelete]
        [Route("EliminarEstanteRecompensa")]
        public async Task<IActionResult> EliminarEstanteRecompensa(int id)
        {
            var estante = await _context.EstanteRecompensas.FindAsync(id);

            if (estante == null)
            {
                return NotFound();
            }

            _context.EstanteRecompensas.Remove(estante);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Estante de recompensa eliminado",
                result = estante
            });
        }
    }
}
