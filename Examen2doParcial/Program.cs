using Examen2doParcial.Data;
using Examen2doParcial.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ExamenDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Categorias.Any())
    {
        db.Categorias.AddRange(
            new Categoria { Id = 1, Nombre = "Electrónicos" },
            new Categoria { Id = 2, Nombre = "Hogar" },
            new Categoria { Id = 3, Nombre = "Ropa" }
        );
    }

    if (!db.Proveedores.Any())
    {
        db.Proveedores.AddRange(
            new Proveedor { Id = 1, Nombre = "Proveedor A" },
            new Proveedor { Id = 2, Nombre = "Proveedor B" }
        );
    }

    if (!db.Productos.Any())
    {
        db.Productos.AddRange(
            new Producto { Id = 1, Nombre = "Televisor", Precio = 399.99m, CategoriaId = 1, ProveedorId = 1 },
            new Producto { Id = 2, Nombre = "Sofá", Precio = 549.50m, CategoriaId = 2, ProveedorId = 2 },
            new Producto { Id = 3, Nombre = "Camiseta", Precio = 19.99m, CategoriaId = 3, ProveedorId = 1 }
        );
    }

    db.SaveChanges();
}

app.Run();
