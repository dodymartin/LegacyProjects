//using System;
//using System.Data.SqlClient;
//using System.Linq;
//using Dapper;
//using PackageDeliveryNew.Deliveries;
//using PackageDeliveryNew.Utils;

//namespace PackageDeliveryNew.Acl
//{
//    public class DeliveryRepository
//    {
//        public Delivery GetById(int id)
//        {
//            var legacyDelivery = GetLegacyDelivery(id);
//            var delivery = MapLegacyDelivery(legacyDelivery);

//            return delivery;
//        }

//        private Delivery MapLegacyDelivery(DeliveryLegacy legacyDelivery)
//        {
//            if (legacyDelivery.CT_ST == null || !legacyDelivery.CT_ST.Contains(" "))
//            {
//                throw new Exception("Invalide city and state");
//            }

//            var cityAndState = legacyDelivery.CT_ST.Split(' ');
//            var address = new Address((legacyDelivery.STR ?? "").Trim(),
//                                      cityAndState[0].Trim(),
//                                      cityAndState[1].Trim(),
//                                      (legacyDelivery.ZP ?? "").Trim());

//            return new Delivery(legacyDelivery.NMB_CM, address);
//        }

//        private DeliveryLegacy GetLegacyDelivery(int deliveryId)
//        {
//            using (var connection = new SqlConnection(Settings.ConnectionString))
//            {
//                var query = @"
//                SELECT d.NMB_CLM, a.*
//                FROM [dbo].[DLVR_TBL] d
//                INNER JOIN [dbo].[ADDR_TBL] a ON a.DLVR = d.NMB_CLM
//                WHERE
//                    d.NMB_CLM = @ID";

//                return connection.Query<DeliveryLegacy>(query, new { ID = deliveryId })
//                    .SingleOrDefault();
//            }
//        }

//        private class DeliveryLegacy
//        {
//            public int NMB_CM { get; set; }
//            public string STR { get; set; }
//            public string CT_ST { get; set; }
//            public string ZP { get; set; }
//        }
//    }
//}
