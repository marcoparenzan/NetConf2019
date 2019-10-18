using NonNullableLib;
using System;
using System.Linq;

namespace EFGetStarted
{
    class Program
    {
        static void Main()
        {
            using (var db = new OrdersContext())
            {
                //Create
                Console.WriteLine("Inserting a new order");
                db.Add(new Order
                {
                    TrackingNumber = "123",
                    ShippingAddress = new StreetAddress
                    {
                        City = "PORDENONE",
                        Street = "VIA KENNEDY"
                    }
                });
                db.SaveChanges();

                //var order = db.Orders.First();
            }
        }
    }
}
