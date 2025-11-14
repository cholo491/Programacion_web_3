using Examen2doParcial.Data;
using Examen2doParcial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen2doParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductoController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAll([FromQuery] string? nombre, [FromQuery] int? categoriaId, [FromQuery] int? proveedorId)
        {
            var query = _db.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre));
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(p => p.CategoriaId == categoriaId.Value);
            }

            if (proveedorId.HasValue)
            {
                query = query.Where(p => p.ProveedorId == proveedorId.Value);
            }

            return Ok(await query.ToListAsync());
        }

       
        [HttpGet("ordenar/categoria")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetOrdenadoPorCategoria()
        {
            var data = await _db.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .OrderBy(p => p.Categoria!.Nombre)
                .ThenBy(p => p.Nombre)
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Producto>>> BuscarPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return BadRequest("Debe proporcionar un nombre");
            var data = await _db.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Where(p => p.Nombre.Contains(nombre))
                .ToListAsync();
            return Ok(data);
        }
        [HttpGet("proveedor/{proveedorId:int}")]
        public async Task<ActionResult<IEnumerable<Producto>>> ListarPorProveedor([FromRoute] int proveedorId)
        {
            var data = await _db.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .Where(p => p.ProveedorId == proveedorId)
                .ToListAsync();
            return Ok(data);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var prod = await _db.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (prod == null) return NotFound();
            return Ok(prod);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            if (!_db.Categorias.Any(c => c.Id == producto.CategoriaId))
                return BadRequest("CategoriaId no existe");
            if (!_db.Proveedores.Any(p => p.Id == producto.ProveedorId))
                return BadRequest("ProveedorId no existe");

            if (producto.Id == 0)
            {
                var nextId = (_db.Productos.Any() ? _db.Productos.Max(p => p.Id) : 0) + 1;
                producto.Id = nextId;
            }

            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest("Ids no coinciden");
            if (!_db.Categorias.Any(c => c.Id == producto.CategoriaId))
                return BadRequest("CategoriaId no existe");
            if (!_db.Proveedores.Any(p => p.Id == producto.ProveedorId))
                return BadRequest("ProveedorId no existe");

            var exists = await _db.Productos.AnyAsync(p => p.Id == id);
            if (!exists) return NotFound();

            _db.Entry(producto).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _db.Productos.FindAsync(id);
            if (prod == null) return NotFound();
            _db.Productos.Remove(prod);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
