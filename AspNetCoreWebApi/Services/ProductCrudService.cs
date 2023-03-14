using AspNetCoreWebApi.Data;
using AspNetCoreWebApi.Models;

namespace AspNetCoreWebApi.Services
{
    /// <summary>
    /// Service class for providing CRUD functions for maintaining product data.
    /// </summary>
    public class ProductCrudService
    {
        private readonly ProductData _productData;

        public ProductCrudService(ProductData productData)
        {
            _productData = productData;
        }

        public List<MasterProduct> GetProducts(string? productName, int page)
        {
            var products = _productData.Products;

            if (!string.IsNullOrEmpty(productName))
            {
                // Uppercase the search string to filter case-insensitive.
                // Example: searchString = app -> APP.
                var searchString = productName.ToUpper();

                products = _productData.Products
                    // Uppercase the Name data so the filter will match without need to check its case-sensitiveness.
                    // Example: Name = Apple -> APPLE.
                    // StartsWith() will search based on the string prefix, in SQL expression, it would translate to: WHERE Name LIKE APP%.
                    // So, if your searchString value is APP, the list will return APPLE and APPLICATION product, since both names have "APP" in their words as their prefixes.
                    .Where(Q => Q.Name.ToUpper().StartsWith(searchString))
                    .ToList();
            }

            // Search first, then paginate.
            // Page = the target page index.
            // Item per page = how many products will be shown to the users per page.

            var itemPerPage = 3;

            if (page >= 1)
            {
                page -= 1;
            }

            products = products
                // Skip based on page X itemPerPage, example: 1 page X 3 itemPerPage, so you will skip 3 items and retrieve the next 3 items on the next page.
                .Skip(page * itemPerPage)
                // Take the whole items on the next page, example: 3 itemPerPage, then users will retrieve 3 itemPerPage
                .Take(itemPerPage)
                .ToList();

            return products;
        }

        /// <summary>
        /// Add new product to the static list.
        /// </summary>
        /// <param name="newProduct"></param>
        public Guid AddNewProduct(NewProduct newProduct)
        {
            var newId = Guid.NewGuid();

            _productData.Products.Add(new MasterProduct
            {
                ProductId = newId,
                Name = newProduct.Name!,
                Price = newProduct.Price,
                Quantity = newProduct.Quantity
            });

            return newId;
        }

        /// <summary>
        /// Get product based on the Product ID.
        /// Return null if not found.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public MasterProduct? GetProduct(Guid productId)
        {
            var product = _productData.Products
                .FirstOrDefault(Q => Q.ProductId == productId);

            return product;
        }

        /// <summary>
        /// Update the existing product.
        /// Return true if success, otherwise return false.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        public bool UpdateProduct(Guid productId, UpdateProductRequestModel updatedProduct)
        {
            var existingProduct = _productData.Products
                .FirstOrDefault(Q => Q.ProductId == productId);

            // Validate whether the product ID is exists or not.
            if (existingProduct == null)
            {
                return false;
            }

            // ProductName should not be null since it has been validated in controller.
            existingProduct.Name = updatedProduct.ProductName!;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;

            return true;
        }

        /// <summary>
        /// Delete product based on the product ID.
        /// Return true if success, otherwise return false.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(Guid productId)
        {
            var existingProduct = _productData.Products
               .FirstOrDefault(Q => Q.ProductId == productId);

            // Validate whether the product ID is exists or not.
            if (existingProduct == null)
            {
                return false;
            }

            _productData.Products.Remove(existingProduct);

            return true;
        }
    }
}
