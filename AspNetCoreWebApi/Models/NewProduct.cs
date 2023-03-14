using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    public class NewProduct
    {
        // Required will validate that the input from HTTP request must not be empty.
        [Required]
        // StringLength will validate the string's length.
        [StringLength(32, MinimumLength = 3)]
        public string? Name { get; set; }

        // Range will validate the numeric min and max values.
        [Range(100, 1_000_000)]
        public decimal Price { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
