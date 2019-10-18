using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NonNullableLib;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmos
{
    public class Program
    {
        static async Task Main()
        {
            using (var context = new OrderContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var order = new Order
                {
                    Id = 1,
                    TrackingNumber = "123",
                    ShippingAddress = new StreetAddress { City = "London", Street = "221 B Baker St" }
                };

                context.Add(order);

                await context.SaveChangesAsync();
            }

            using (var context = new OrderContext())
            {
                await foreach (var item in context.Orders)
                {
                    Console.WriteLine($"{item.TrackingNumber}"); 
                }
            }
            
            using (var context = new OrderContext())
            {
                var cosmosClient = context.Database.GetCosmosClient();
                var database = cosmosClient.GetDatabase("Points");
                var container = database.GetContainer("Orders");

                var resultSet = container.GetItemQueryIterator<JObject>(new QueryDefinition("select * from o"));
                var order = (await resultSet.ReadNextAsync()).First();

                Console.WriteLine($"First order JSON: {order}");

                order.Remove("TrackingNumber");

                await container.ReplaceItemAsync(order, order["id"].ToString());
            }
        }
    }
}
