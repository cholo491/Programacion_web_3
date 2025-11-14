using Examen2doParcial.Data;
using Examen2doParcial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen2doParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoriaController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            return Ok(await _db.Categorias.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var item = await _db.Categorias.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Create(Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                var nextId = (_db.Categorias.Any() ? _db.Categorias.Max(c => c.Id) : 0) + 1;
                categoria.Id = nextId;
            }
            _db.Categorias.Add(categoria);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest();
            var exists = await _db.Categorias.AnyAsync(c => c.Id == id);
            if (!exists) return NotFound();
            _db.Entry(categoria).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Categorias.FindAsync(id);
            if (item == null) return NotFound();
            _db.Categorias.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
