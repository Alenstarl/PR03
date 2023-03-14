namespace AspNetCoreWebApi.Models
{
    public class MasterProduct
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
