using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Models
{
    /// <summary>
    /// Model class for binding the update product request from client inputs.
    /// </summary>
    public class UpdateProductRequestModel
    {
        [Required(ErrorMessage = "Product Name tidak boleh kosong.")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Product Name harus diantara 3 - 32 karakter.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product Price tidak boleh kosong.")]
        [Range(500, 1_000_000, ErrorMessage = "Product Price harus diantara 500 - 1.000.000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Quantity tidak boleh kosong.")]
        [Range(1, 100, ErrorMessage = "Product Quantity harus diantara 1 - 100.")]
        public int Quantity { get; set; }
    }
}
