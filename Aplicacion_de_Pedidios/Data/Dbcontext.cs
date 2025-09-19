using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Aplicacion_de_Pedidios.Models;


namespace Aplicacion_de_Pedidios.Data
{
    public class Dbcontext
    {
        //contexto de la base de datos para la migracion
        public static List<Models.ProductModel> Products { get; set; } = new List<Models.ProductModel>();
        public static List<Models.UserModel> Users { get; set; } = new List<Models.UserModel>();
        public static List<Models.OrderModel> Orders { get; set; } = new List<Models.OrderModel>();
        public static List<Models.OrderItemModel> OrderItems { get; set; } = new List<Models.OrderItemModel>();
        
        protected override void OnCreatingModel(ModelBuilder modelBuilder)
        {
            base.OnCreatingModel(modelBuilder);
            modelBuilder.Entity<Models.ProductModel>().ToTable("Products");
            modelBuilder.Entity<Models.UserModel>().ToTable("Users");
            modelBuilder.Entity<Models.OrderModel>().ToTable("Orders")
            //un orden lo puede hacer un solo usuario pero un usuario puede hacer muchos ordenes
            .HasOne<Models.UserModel>()
            .WithMany()
            .HasForeignKey(o => o.Cliente);
            modelBuilder.Entity<Models.OrderItemModel>().ToTable("OrderItems")
            //un order item pertenece a un solo producto pero un producto puede tener muchos order items
            .HasOne<Models.OrderItemModel>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        }
    }
}
