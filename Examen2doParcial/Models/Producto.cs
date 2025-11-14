namespace Examen2doParcial.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public int ProveedorId { get; set; }
        public Categoria? Categoria { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}
