namespace Aplicacion_de_Pedidios.Models
{
    public class OrderModel
    {
         public int Id { get; set; }
         public int Cliente { get; set; }
         public DateTime Fecha { get; set; }
         public decimal Total { get; set; }
         public string Estado { get; set; }
    }
}
