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
    public class RolController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RolController(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/Rol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<rol>>> Getrol()
        {
            return await _context.Rols.ToListAsync();
        }

        // GET api/Rol
        [HttpGet("{idRol}")]

        public async Task<ActionResult<rol>> Getrol(int id)
        {
            var rol = await _context.Rols.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }


        // put api/Rol
        [HttpPut("idRol")]
        public async Task<IActionResult> putrol(int id, rol rol
            )
        {
            if (id != rol.idRol)
            {
                return BadRequest();
            }

           
            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!RolExists(id))
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

        //POst api/Rol
        [HttpPost]
        public async Task<ActionResult<rol>> Postrol(rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getrol", new { id = rol.idRol }, rol);
        }

        //Delete Api/Rol

        [HttpDelete("idRol")]
        public async Task<ActionResult<rol>> Deleterol(int id)
        {
            var rol = await _context.Rols.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();

            return rol;
        }

        private bool RolExists(int id)
        {
            return _context.Rols.Any(e => e.idRol == id);
        }
    }

}
