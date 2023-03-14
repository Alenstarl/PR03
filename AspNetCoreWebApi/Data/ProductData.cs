using AspNetCoreWebApi.Models;

namespace AspNetCoreWebApi.Data
{
    public class ProductData
    {
        public List<MasterProduct> Products { get; set; } = new List<MasterProduct>
        {
            new MasterProduct
            {
                ProductId = Guid.NewGuid(),
                Name = "Apple",
                Price = 10_000,
                Quantity = 10
            }
        };
    }
}
