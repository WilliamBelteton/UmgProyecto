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
    public class Persona_Controller : ControllerBase
    {
        private readonly DbContextSistema _context;

        public Persona_Controller(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/Persona_
        [HttpGet]
        public async Task<ActionResult<IEnumerable<persona_>>> Getpersona_()
        {
            return await _context.Personas_.ToListAsync();
        }

        // GET api/Persona_
        [HttpGet("{idPersona_}")]

        public async Task<ActionResult<persona_>> Getpersona_(int id)
        {
            var persona_ = await _context.Personas_.FindAsync(id);

            if (persona_ == null)
            {
                return NotFound();
            }

            return persona_;
        }


        // put api/Persona_
        [HttpPut("idPersona_")]
        public async Task<IActionResult> putPersona_(int id, persona_ persona_)
        {
            if (id != persona_.idPersona_)
            {
                return BadRequest();
            }

           
            _context.Entry(persona_).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!Persona_Exists(id))
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

        //POst api/Persona_
        [HttpPost]
        public async Task<ActionResult<persona_>> Postpersona_(persona_ persona_)
        {
            _context.Personas_.Add(persona_);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getpersona_", new { id = persona_.idPersona_ }, persona_);
        }

        //Delete Api/Persona_

        [HttpDelete("idPersona_")]
        public async Task<ActionResult<persona_>> Deletepersona_(int id)
        {
            var persona_ = await _context.Personas_.FindAsync(id);

            if (persona_ == null)
            {
                return NotFound();
            }

            _context.Personas_.Remove(persona_);
            await _context.SaveChangesAsync();

            return persona_;
        }

        private bool Persona_Exists(int id)
        {
            return _context.Personas_.Any(e => e.idPersona_ == id);
        }
    }

}