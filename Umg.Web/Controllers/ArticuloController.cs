using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umg.Datos;
using Umg.Entidades.Almacen;

namespace Umg.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticuloController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: ArticuloMapp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<articulo>>> GetArticulos()
        {
            return await _context.Articulos.ToListAsync();
        }

        // GET: ArticuloMapp/Details/5
        [HttpGet("{idArticulo}")]

        public async Task<ActionResult<articulo>> Getarticulo(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);



            if (articulo == null)
            {
                return NotFound();
            }

            return articulo;
        }


        // put api/Categoria/2 
        [HttpPut("idArticulo")]
        public async Task<IActionResult> putarticulo(int id, articulo articulo)
        {
            if (id != articulo.idArticulo)
            {
                return BadRequest();
            }

            //MI ENTIDAD YA TIENE LAS PROPIEDADES QUE VOY A AGUARDAR EN MI BD
            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ArticuloExists(id))
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

        //POst api/Categorias
        [HttpPost]
        public async Task<ActionResult<articulo>> Postarticulo(articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getarticulo", new { id = articulo.idArticulo }, articulo);
        }

        //Delete Api/Categoria 2 

        [HttpDelete("idArticulo")]
        public async Task<ActionResult<articulo>> Deletearticulo(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();

            return articulo;
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.idArticulo == id);
        }
    }

}
