using System.Linq;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using ProductLibrary.Config;

namespace ProductLibrary {
    public class ProductsProvider : IProductsProvider {
        private readonly DbConfig _dbConfig;
        private readonly ILogger<ProductsProvider> _logger;

        public ProductsProvider (DbConfig dbConfig, ILogger<ProductsProvider> logger) 
        {
            _dbConfig = dbConfig;
            _logger = logger;
        }

        private const string QUERY = "SELECT Id, Name, Description FROM product";

        public Product[] GetAll()
        {
            _logger.LogWarning(_dbConfig.ConnectionString);
            using (var connection = new MySqlConnection (_dbConfig.ConnectionString))
            {
                return connection.Query<Product> (QUERY).ToArray ();
            }
        }
    }
}