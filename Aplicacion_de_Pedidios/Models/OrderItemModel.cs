namespace Aplicacion_de_Pedidios.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Cantidad { get; set; }
        public decimal subTotal { get; set; }
    }
    
}
