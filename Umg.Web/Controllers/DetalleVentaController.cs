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
    public class DetalleVentaController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public DetalleVentaController(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/detalleVenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<detalleVenta>>> GetdetalleVenta()
        {
            return await _context.DetalleVentas.ToListAsync();
        }

        // GET api/detalleVenta
        [HttpGet("{idDetalleVenta")]

        public async Task<ActionResult<detalleVenta>> GetdetalleVenta(int id)
        {
            var detalleVenta = await _context.DetalleVentas.FindAsync(id);

            if (detalleVenta == null)
            {
                return NotFound();
            }

            return detalleVenta;
        }


        // put api/detalleVenta 
        [HttpPut("idDetalleVenta")]
        public async Task<IActionResult> putdetalleVenta(int id, detalleVenta detalleVenta)
        {
            if (id != detalleVenta.idDetalleVenta)
            {
                return BadRequest();
            }

           
            _context.Entry(detalleVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!DetalleVentaExists(id))
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

        //POst api/detalleVenta
        [HttpPost]
        public async Task<ActionResult<detalleVenta>> PostdetalleVenta(detalleVenta detalleVenta)
        {
            _context.DetalleVentas.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getdetalleVenta", new { id = detalleVenta.idDetalleVenta }, detalleVenta;
        }

        //Delete Api/detalleVenta 

        [HttpDelete("idDetalleVenta")]
        public async Task<ActionResult<detalleVenta>> DeletedetalleVenta(int id)
        {
            var detalleVenta = await _context.DetalleVentas.FindAsync(id);

            if (detalleVenta == null)
            {
                return NotFound();
            }

            _context.DetalleVentas.Remove(detalleVenta);
            await _context.SaveChangesAsync();

            return detalleVenta;
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVentas.Any(e => e.idDetalleVenta == id);
        }
    }

}
