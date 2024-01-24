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
    public class UsuarioController : ControllerBase
    {
        private readonly VolunTrackDbContext _context;

        public UsuarioController(VolunTrackDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario/ListarUsuarios
        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            List<Usuario> usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/BuscarUsuario/5
        [HttpGet]
        [Route("BuscarUsuario")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuario/EditarUsuario/5
        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(int id, [FromQuery] string Nombre, [FromQuery] string NombreUsuario,
            [FromQuery] string Contrasenia, [FromQuery] DateOnly FechaNacimiento, [FromQuery] string Correo,
            [FromQuery] string Numero, [FromQuery] string Direccion)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = Nombre;
            usuario.NombreUsuario = NombreUsuario;
            usuario.Contrasenia = Contrasenia;
            usuario.FechaNacimiento = FechaNacimiento;
            usuario.Correo = Correo;
            usuario.Numero = Numero;
            usuario.Direccion = Direccion;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario actualizado",
                result = usuario
            });
        }

        // POST: api/Usuario/RegistrarUsuario
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromQuery] string Nombre, [FromQuery] string NombreUsuario,
            [FromQuery] string Contrasenia, [FromQuery] DateOnly FechaNacimiento, [FromQuery] string Correo,
            [FromQuery] string Numero, [FromQuery] string Direccion)
        {
            var usuario = new Usuario
            {
                Nombre = Nombre,
                NombreUsuario = NombreUsuario,
                Contrasenia = Contrasenia,
                FechaNacimiento = FechaNacimiento,
                Correo = Correo,
                Numero = Numero,
                Direccion = Direccion
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario registrado",
                result = usuario
            });
        }

        // DELETE: api/Usuario/EliminarUsuario/5
        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario eliminado",
                result = usuario
            });
        }
    }
}
