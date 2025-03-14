using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PackageDeliveryNew.Utils;

namespace PackageDeliveryNew.Deliveries
{
    public class ProductRepository
    {
        public Product GetById(int id)
        {
            var productData = GetRawData(id);
            var product = MapData(productData);

            return product;
        }

        private Product MapData(ProductData productData)
        {
            return new Product(productData.ProductId, productData.WeightInPounds, productData.Name);
        }

        private ProductData GetRawData(int id)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                var query = @"
                    SELECT *
                    FROM [dbo].[Product]
                    WHERE ProductId = @id";
                return connection.QuerySingle<ProductData>(query, new { id });
            }
        }

        public IReadOnlyList<Product> GetAll()
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"SELECT * FROM [dbo].[Product]";

                return connection
                    .Query<ProductData>(query)
                    .Select(x => MapData(x))
                    .ToList();
            }
        }

        private class ProductData
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public double WeightInPounds { get; set; }
        }
    }
}
