using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using PackageDeliveryNew.Common;
using PackageDeliveryNew2.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Delivery : Entity
    {
        private const double PricePerMilePerPound = 0.04;
        private const double BasePrice = 20;

        public Address Address { get; }

        public Delivery(int id, Address address)
            : base(id)
        {
            Contracts.Require(id >= 0);
            Contract.Requires(address != null);

            Address = address;
        }

        public decimal GetEstimate(double distanceInMiles, List<ProductLine> productLines)
        {
            Contracts.Require(distanceInMiles >= 0, "Invalid distance");
            Contracts.Require(productLines?.Count > 0 && productLines?.Count <= 4, "Invalid product line count");

            var totalWeightInPounds = productLines.Sum(x => x.Product.WeightInPounds * x.Amount);
            var estimate = totalWeightInPounds * distanceInMiles * PricePerMilePerPound + BasePrice;

            return decimal.Round((decimal)estimate, 2);
        }
    }
}
