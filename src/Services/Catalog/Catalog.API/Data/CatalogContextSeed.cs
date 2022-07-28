using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }
        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "101",
                    Name = "Iphone X",
                    Summary = "nice one",
                    ImageFile = "file.jpg",
                    Category = "Smart Phone",
                    Price = 900,
                    Description = "OK"

                },
                new Product()
                {
                    Id = "102",
                    Name = "MI X",
                    Summary = "nice one",
                    ImageFile = "file.jpg",
                    Category = "Smart Phone",
                    Price = 900,
                    Description = "OK"
                },
                new Product()
                {
                    Id = "1013",
                    Name = "Realme X",
                    Summary = "nice one",
                    ImageFile = "file.jpg",
                    Category = "Smart Phone",
                    Price = 900,
                    Description = "OK"
                }

            };
        }
    }
}
