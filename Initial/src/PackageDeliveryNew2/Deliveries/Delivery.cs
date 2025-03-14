using System.Collections.Generic;
using System.Linq;
using PackageDeliveryNew.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Delivery : Entity
    {
        private const double PricePerMilePerPound = 0.04;
        private const double BasePrice = 20;

        public Address Destination { get; }
        public decimal? CostEstimate { get; private set; }
        private readonly List<ProductLine> _productLines;
        public IReadOnlyList<ProductLine> ProductLines => _productLines.ToList();

        public Delivery(int id, Address destination, decimal? costEstimate, IReadOnlyList<ProductLine> productLines)
            : base(id)
        {
            Contracts.Require(id >= 0);
            Contracts.Require(destination != null);
            Contracts.Require(productLines != null);

            Destination = destination;
            CostEstimate = costEstimate;
            _productLines = productLines.ToList();
        }

        public void RecalculateCostEstimate(double distanceInMiles)
        {
            Contracts.Require(distanceInMiles >= 0, "Invalid distance");
            Contracts.Require(ProductLines?.Count > 0, "Need at least one product line");

            var totalWeightInPounds = ProductLines.Sum(x => x.Product.WeightInPounds * x.Amount);
            var estimate = totalWeightInPounds * distanceInMiles * PricePerMilePerPound + BasePrice;

            CostEstimate = decimal.Round((decimal)estimate, 2);
        }

        public void DeleteLine(ProductLine line)
        {
            _productLines.Remove(line);
        }

        public void AddProduct(Product product, int amount)
        {
            _productLines.Add(new ProductLine(product, amount));
        }
    }
}
