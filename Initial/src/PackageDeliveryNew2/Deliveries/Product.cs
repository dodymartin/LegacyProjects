using PackageDeliveryNew.Common;
using PackageDeliveryNew2.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Product : Entity
    {
        public double WeightInPounds { get; }

        public Product(int id, double weightInPounds)
            : base(id)
        {
            Contracts.Require(id >= 0, "Invalid product id");
            Contracts.Require(weightInPounds > 0, "Weight must be greater than 0");

            WeightInPounds = weightInPounds;
        }
    }
}
