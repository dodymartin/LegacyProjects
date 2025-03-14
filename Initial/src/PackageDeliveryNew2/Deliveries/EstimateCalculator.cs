using PackageDeliveryNew.Common;

namespace PackageDeliveryNew.Deliveries
{
    public class EstimateCalculator
    {
        private readonly DeliveryRepository _deliveryRepository;
        private readonly ProductRepository _productRepository;
        private readonly AddressResolver _addressResolver;
        public EstimateCalculator()
        {
            _deliveryRepository = new DeliveryRepository();
            _productRepository = new ProductRepository();
            _addressResolver = new AddressResolver();
        }
        public Result<decimal> Calculate(int deliveryId,
                                 int? productId1,
                                 int amount1,
                                 int? productId2,
                                 int amount2,
                                 int? productId3,
                                 int amount3,
                                 int? product_id4,
                                 int amount4)
        {
            //if (productId1 is null && productId2 is null && productId3 is null && product_id4 is null)
            //    return Result.Fail<decimal>("At least one product must be provided");

            //Delivery delivery = _deliveryRepository.GetById(deliveryId)
            //    ?? throw new Exception("Invalid delivery");

            //double? distance = _addressResolver.GetDistanceTo(delivery.Destination);
            //if (distance is null)
            //    return Result.Fail<decimal>("Invalid address");

            //var productLines = new List<(int? productId, int amount)>
            //{
            //    (productId1, amount1),
            //    (productId2, amount2),
            //    (productId3, amount3),
            //    (product_id4, amount4)
            //}
            //.Where(x => x.productId != null)
            //.Select(x => new ProductLine(_productRepository.GetById(x.productId.Value), x.amount))
            //.ToList();

            //if (productLines.Any(x => x.Product == null))
            //    throw new Exception("Invalid product");

            //return Result.Ok(delivery.GetEstimate(distance.Value, productLines));
            return Result.Ok(0m);
        }
    }

    public class AddressResolver
    {
        public double? GetDistanceTo(Address address)
        {
            return 15;
        }
    }
}
