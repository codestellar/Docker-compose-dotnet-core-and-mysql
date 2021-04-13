using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using ProductLibrary.Config;

namespace ProductLibrary
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly DbConfig _dbConfig;

        public ProductsProvider(DbConfig dbConfig) 
        {
            _dbConfig = dbConfig;
        }

        private const string QUERY = "SELECT Id, Name, Description FROM product";

        public Product[] GetAll()
        {
            using (var connection = new MySqlConnection(_dbConfig.ConnectionString))
            {
                return connection.Query<Product>(QUERY).ToArray();
            }
        }
    }
}