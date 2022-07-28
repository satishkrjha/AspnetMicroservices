using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration configuration;

        public CatalogContext(IConfiguration configuration)
        {
          this.configuration=  configuration;
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:Database"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            //var client = new MongoClient("mongodb://localhost:27017");
            //var database = client.GetDatabase("ProductDb");
            //Products = database.GetCollection<Product>("Products");
          
            CatalogContextSeed.SeedData(Products);
        }
      
        public IMongoCollection<Product> Products { get; }
    }
}
