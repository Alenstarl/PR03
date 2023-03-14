using AspNetCoreWebApi.Data;
using AspNetCoreWebApi.Models;
using AspNetCoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi.Controllers
{
    // Set [Authorize] annotation on top of a controller class to secure all web API definitions in this controller.
    [Authorize]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductCrudService _productCrudService;

        public ProductController(ProductCrudService productCrudService)
        {
            _productCrudService = productCrudService;
        }

        [HttpGet]
        // Use ActionResult return type.
        // productName is a query string parameter. You must set the [FromQuery] annotation.
        public ActionResult GetProducts([FromQuery] string? productName,
            [FromQuery] int page = 1)
        {
            var products = _productCrudService.GetProducts(productName, page);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        // productId name declaration must be the same.
        public ActionResult GetProduct(Guid productId)
        {
            var product = _productCrudService.GetProduct(productId);

            return Ok(product);
        }

        // Alternatively, you could set [Authorize] on each web API action method instead.
        // Example, you will want to protect the Create web API to ensure that only logged in user can create a new product.
        // [Authorize]
        [HttpPost]
        // FromBody will capture the HTTP body JSON input from HTTP request.
        public ActionResult Create([FromBody] NewProduct newProduct)
        {
            // This will check whether the model (newProduct) data annotation's validations contains error or not.
            if (!ModelState.IsValid)
            {
                // Will return HTTP status code 400, which indicates user input errors due validation etc.
                return ValidationProblem(ModelState);
            }

            var newId = _productCrudService.AddNewProduct(newProduct);

            return Ok($"New Product ID: {newId}");
        }

        // To validate an user based on their user's role claims, you could set the parameter Roles with the allowed role names.
        // [Authorize(Roles = "Manager")]
        [HttpPut("{productId}")]
        public ActionResult Update(Guid productId, [FromBody]UpdateProductRequestModel updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var isSuccess = _productCrudService.UpdateProduct(productId, updatedProduct);

            if (!isSuccess)
            {
                // This will add a new model state error to the current ModelState object.
                // We need to do this because we use a custom validation that is outside of data annotation validation in the model class.
                ModelState.AddModelError("ProductID", "Product ID tidak ditemukan.");
                return ValidationProblem(ModelState);
            }

            return Ok();
        }

        [HttpDelete("{productId}")]
        public ActionResult Delete(Guid productId)
        {
            var isSuccess = _productCrudService.DeleteProduct(productId);

            if (!isSuccess)
            {
                // This will add a new model state error to the current ModelState object.
                // We need to do this because we use a custom validation that is outside of data annotation validation in the model class.
                ModelState.AddModelError("ProductID", "Product ID tidak ditemukan.");
                return ValidationProblem(ModelState);
            }

            return Ok();
        }
    }
}
