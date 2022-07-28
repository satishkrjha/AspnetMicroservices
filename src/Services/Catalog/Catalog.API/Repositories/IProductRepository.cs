using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(string id);
        public Task<IEnumerable<Product>> GetProductByName(string name);
        public Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
