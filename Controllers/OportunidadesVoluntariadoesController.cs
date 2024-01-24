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
    public class OportunidadesVoluntariadoController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public OportunidadesVoluntariadoController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/OportunidadesVoluntariado/ListarOportunidadesVoluntariado
        [HttpGet]
        [Route("ListarOportunidadesVoluntariado")]
        public async Task<IActionResult> ListarOportunidadesVoluntariado()
        {
            List<OportunidadesVoluntariado> oportunidades = await _context.OportunidadesVoluntariados.ToListAsync();
            return Ok(oportunidades);
        }

        // GET: api/OportunidadesVoluntariado/BuscarOportunidadVoluntariado/5
        [HttpGet]
        [Route("BuscarOportunidadVoluntariado")]
        public async Task<IActionResult> ObtenerOportunidadVoluntariado(int id)
        {
            var oportunidad = await _context.OportunidadesVoluntariados.FindAsync(id);

            if (oportunidad == null)
            {
                return NotFound();
            }

            return Ok(oportunidad);
        }

        // PUT: api/OportunidadesVoluntariado/EditarOportunidadVoluntariado/5
        [HttpPut]
        [Route("EditarOportunidadVoluntariado")]
        public async Task<IActionResult> EditarOportunidadVoluntariado(int id, [FromQuery] int? IdVoluntariado, [FromQuery] int? IdUsuario)
        {
            var oportunidad = await _context.OportunidadesVoluntariados.FindAsync(id);

            if (oportunidad == null)
            {
                return NotFound();
            }

            oportunidad.IdVoluntariado = IdVoluntariado;
            oportunidad.IdUsuario = IdUsuario;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Oportunidad de voluntariado actualizada",
                result = oportunidad
            });
        }

        // POST: api/OportunidadesVoluntariado/RegistrarOportunidadVoluntariado
        [HttpPost]
        [Route("RegistrarOportunidadVoluntariado")]
        public async Task<IActionResult> RegistrarOportunidadVoluntariado([FromQuery] int? IdVoluntariado, [FromQuery] int? IdUsuario)
        {
            var oportunidad = new OportunidadesVoluntariado
            {
                IdVoluntariado = IdVoluntariado,
                IdUsuario = IdUsuario
            };

            _context.OportunidadesVoluntariados.Add(oportunidad);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Oportunidad de voluntariado registrada",
                result = oportunidad
            });
        }

        // DELETE: api/OportunidadesVoluntariado/EliminarOportunidadVoluntariado/5
        [HttpDelete]
        [Route("EliminarOportunidadVoluntariado")]
        public async Task<IActionResult> EliminarOportunidadVoluntariado(int id)
        {
            var oportunidad = await _context.OportunidadesVoluntariados.FindAsync(id);

            if (oportunidad == null)
            {
                return NotFound();
            }

            _context.OportunidadesVoluntariados.Remove(oportunidad);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Oportunidad de voluntariado eliminada",
                result = oportunidad
            });
        }
    }
}
