using System.ComponentModel.DataAnnotations;

namespace PrimerParcial.Models
{
    public class Recipe
    {
        // Clave Primaria (PK)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        [Required]
        public string Instructions { get; set; } // Pasos para preparar la receta

        // --- Relaciones de Entity Framework Core ---


        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
