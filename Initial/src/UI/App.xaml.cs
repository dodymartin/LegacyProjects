using PackageDelivery.Delivery;
using PackageDeliveryNew.Utils;

namespace PackageDelivery
{
    public partial class App
    {
        public App()
        {
            var legacyDatabaseConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PackageDelivery;Trusted_Connection=true;";
            var bubbleDatabaseConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PackageDeliveryNew;Trusted_Connection=true;";

            DBHelper.Init(legacyDatabaseConnectionString);
            Settings.Init(bubbleDatabaseConnectionString);
        }
    }
}
