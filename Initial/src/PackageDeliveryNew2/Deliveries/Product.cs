using PackageDeliveryNew.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class Product : Entity
    {
        public string Name { get; }
        public double WeightInPounds { get; }

        public Product(int id, double weightInPounds, string name)
            : base(id)
        {
            Contracts.Require(id >= 0, "Invalid product id");
            Contracts.Require(weightInPounds > 0, "Weight must be greater than 0");
            Contracts.Require(name != null, "Product name is required");

            WeightInPounds = weightInPounds;
            Name = name;
        }
    }
}
