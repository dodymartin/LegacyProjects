using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PackageDeliveryNew.Utils;

namespace PackageDeliveryNew.Deliveries
{
    public class DeliveryRepository
    {
        public Delivery GetById(int id)
        {
            var data = GetRawData(id);
            var delivery = MapData(data.deliveryData, data.linesData);
            return delivery;
        }

        private Delivery MapData(DeliveryData deliveryData, List<ProductLineData> linesData)
        {
            var productLines = linesData.Select(lineData =>
            {
                var product = new Product(lineData.ProductId, lineData.ProductWeightInPounds, lineData.ProductName);
                return new ProductLine(product, lineData.Amount);
            }).ToList();

            var delivery = new Delivery(
                deliveryData.DeliveryId,
                new Address(
                    deliveryData.DestinationStreet,
                    deliveryData.DestinationCity,
                    deliveryData.DestinationState,
                    deliveryData.DestinationZipCode
                ),
                deliveryData.CostEstimate,
                productLines
            );

            return delivery;
        }

        private (DeliveryData deliveryData, List<ProductLineData> linesData) GetRawData(int id)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"
                    select *
                    from [dbo].[Delivery] d
                    where d.DeliveryId = @id 

                    select l.*, p.WeightInPounds ProductWeightInPounds, p.Name ProductName
                    from [dbo].[ProductLine] l
                    inner join [dbo].[Product] p on l.ProductId = p.ProductId
                    where l.DeliveryId = @id";

                SqlMapper.GridReader reader = connection.QueryMultiple(query, new { id });
                DeliveryData deliveryData = reader.ReadSingle<DeliveryData>();
                List<ProductLineData> linesData = reader.Read<ProductLineData>().AsList();

                return (deliveryData, linesData);
            }
        }

        public void Save(Delivery delivery)
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"
                    UPDATE [dbo].[Delivery]
                    SET CostEstimate = @CostEstimate
                    WHERE DeliveryId = @ID  

                    DELETE FROM [dbo].[ProductLine]
                    WHERE DeliveryID = @ID ";

                connection.Execute(query, new { ID = delivery.Id, delivery.CostEstimate });

                string query2 = @"
                    INSERT INTO [dbo].[ProductLine] (ProductId, Amount, DeliveryID)
                    VALUES (@ProductId, @Amount, @DeliveryId) ";

                connection.Execute(query2, delivery.ProductLines.Select(x => new
                {
                    ProductID = x.Product.Id,
                    x.Amount,
                    DeliveryId = delivery.Id
                }));
            }
        }

        private class DeliveryData
        {
            public int DeliveryId { get; set; }
            public decimal? CostEstimate { get; set; }
            public string DestinationStreet { get; set; }
            public string DestinationCity { get; set; }
            public string DestinationState { get; set; }
            public string DestinationZipCode { get; set; }
        }

        private class ProductLineData
        {
            public int ProductId { get; set; }
            public int Amount { get; set; }
            public double ProductWeightInPounds { get; set; }
            public string ProductName { get; set; }
        }
    }
}
