﻿using Microsoft.AspNetCore.Mvc;
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
    public class Usuario_Controller : ControllerBase
    {
        private readonly DbContextSistema _context;

        public Usuario_Controller(DbContextSistema context)
        {
            _context = context;
        }

        //GET api/usuario_
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuario_>>> Getusuario_()
        {
            return await _context.Usuarios_.ToListAsync();
        }

        // GET api/usuario_
        [HttpGet("{idUsuario_")]

        public async Task<ActionResult<usuario_>> Getusuario_(int id)
        {
            var usuario_ = await _context.Usuarios_.FindAsync(id);

            if (usuario_ == null)
            {
                return NotFound();
            }

            return usuario_;

        }


        // put api/usuario_
        [HttpPut("idUsuario_")]
        public async Task<IActionResult> putusuario_(int id, usuario_ usuario_)
        {
            if (id != usuario_.idUsuario)
            {
                return BadRequest();
            }

         
            _context.Entry(usuario_).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!Usuario_Exists(id))
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

        //POst api/usuario_
        [HttpPost]
        public async Task<ActionResult<usuario_>> Postusuario_(usuario_ usuario_)

        {
            _context.Usuarios_.Add(usuario_);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getusuario_", new { id = usuario_.idUsuario }, usuario_);
        }

        //Delete Api/usuario_

        [HttpDelete("idUsuario_")]
        public async Task<ActionResult<usuario_>> Deleteusuario_(int id)
        {
            var usuario_ = await _context.Usuarios_.FindAsync(id);

            if (usuario_ == null)
            {
                return NotFound();
            }

            _context.Usuarios_.Remove(usuario_);
            await _context.SaveChangesAsync();

            return usuario_;
        }

        private bool Usuario_Exists(int id)
        {
            return _context.Usuarios.Any(e => e.idUsuario == id);
        }
    }

}
