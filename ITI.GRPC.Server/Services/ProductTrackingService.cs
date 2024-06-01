using Grpc.Core;
using ITI.GRPC.Server.Protos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.GRPC.Server.Services
{
    public class ProductTrackingService : InventoryService.InventoryServiceBase
    {
        private static List<Product> products = new List<Product>();

        public override Task<ProductResponse> GetProductById(ProductRequest request, ServerCallContext context)
        {
            var product = products.FirstOrDefault(p => p.Id == request.Id);
            return Task.FromResult(new ProductResponse
            {
                Exists = product != null,
                Product = product
            });
        }

        public override Task<ProductResponse> AddProduct(Product request, ServerCallContext context)
        {
            products.Add(request);
            return Task.FromResult(new ProductResponse { Exists = true, Product = request });
        }

        public override Task<ProductResponse> UpdateProduct(Product request, ServerCallContext context)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == request.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = request.Name;
                existingProduct.Quantity = request.Quantity;
                existingProduct.Price = request.Price;
            }
            return Task.FromResult(new ProductResponse { Exists = existingProduct != null, Product = request });
        }
    }
}
