using Grpc.Net.Client;
using ITI.GRPC_Client.Protos;
using Microsoft.AspNetCore.Mvc;

namespace ITI.GRPC_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProduct(Product product)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7140");
            var client = new InventoryService.InventoryServiceClient(channel);
            var request = new ProductRequest { Id = product.Id };
            var getProductResponse = await client.GetProductByIdAsync(request);

            if (getProductResponse == null)
            {
                return NotFound(new { message = "No product with the provided ID... Try again later!" });
            }
            if (getProductResponse.Exists)
            {
                var updateResponse = await client.UpdateProductAsync(product);
                return Ok(updateResponse.Product);
            }
            else
            {
                var addResponse = await client.AddProductAsync(product);
                return Created("", new { Status = 200, Product = addResponse.Product, Msg = "Product Added Successfully." });
            }
        }
    }
}
