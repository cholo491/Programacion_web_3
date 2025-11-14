using Examen2doParcial.Data;
using Examen2doParcial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen2doParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProveedorController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetAll()
        {
            return Ok(await _db.Proveedores.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Proveedor>> GetById(int id)
        {
            var item = await _db.Proveedores.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> Create(Proveedor proveedor)
        {
            if (proveedor.Id == 0)
            {
                var nextId = (_db.Proveedores.Any() ? _db.Proveedores.Max(c => c.Id) : 0) + 1;
                proveedor.Id = nextId;
            }
            _db.Proveedores.Add(proveedor);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id) return BadRequest();
            var exists = await _db.Proveedores.AnyAsync(c => c.Id == id);
            if (!exists) return NotFound();
            _db.Entry(proveedor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Proveedores.FindAsync(id);
            if (item == null) return NotFound();
            _db.Proveedores.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
