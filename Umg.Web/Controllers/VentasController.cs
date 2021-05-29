using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umg.Datos;
using Umg.Entidades.Ventas;

namespace Umg.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public VentasController(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ventas>>> Getventas()
        {
            return await _context.Ventass.ToListAsync();
        }

        // GET api/ventas
        [HttpGet("{idVenta")]

        public async Task<ActionResult<ventas>> Getventas(int id)
        {
            var venta = await _context.Ventass.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }


        // put api/ventas 
        [HttpPut("idVenta")]
        public async Task<IActionResult> putventas(int id, ventas ventas)

        {
            if (id != ventas.idVenta)
            {
                return BadRequest();
            }

            

            _context.Entry(ventas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!VentasExists(id))
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

        //POst api/ventas
        [HttpPost]
        public async Task<ActionResult<ventas>> Postventas(ventas ventas)
        {
            _context.Ventass.Add(ventas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getventas", new { id = ventas.idVenta }, ventas);
        }

        //Delete Api/ventas 

        [HttpDelete("idVenta")]
        public async Task<ActionResult<ventas>> Deleteventas(int id)
        {
            var ventas = await _context.Ventass.FindAsync(id);

            if (ventas == null)
            {
                return NotFound();
            }

            _context.Ventass.Remove(ventas);
            await _context.SaveChangesAsync();

            return ventas;
        }

        private bool VentasExists(int id)
        {
            return _context.Ventass.Any(e => e.idVenta == id);
        }
    }

}
