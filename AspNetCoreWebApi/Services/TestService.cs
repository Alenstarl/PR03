namespace AspNetCoreWebApi.Services
{
    public class TestService
    {
        public readonly ProductCrudService _productCrudService;

        public TestService(ProductCrudService productCrudService)
        {
            _productCrudService = productCrudService;
        }
    }
}
