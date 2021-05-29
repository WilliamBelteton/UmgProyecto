using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umg.Datos;
using Umg.Entidades.Usuarios;

namespace Umg.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public UsuarioController(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuario>>> Getusuario()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET api/usuario
        [HttpGet("{idUsuario")]

        public async Task<ActionResult<usuario>> Getusuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }


        // put api/usuario
        [HttpPut("idUsuario")]
        public async Task<IActionResult> putusuario(int id, usuario usuario)
        {
            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }


            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }

            return NoContent();

        }

        //POst api/usuario
        [HttpPost]
        public async Task<ActionResult<usuario>> Postusuario(usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getusuario", new { id = usuario.idUsuario }, usuario);
        }

        //Delete Api/usuario 

        [HttpDelete("idUsuario")]
        public async Task<ActionResult<usuario>> Deleteusuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idUsuario == id);
        }
    }

}