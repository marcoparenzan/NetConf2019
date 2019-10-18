using NonNullableLib;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCore3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Json();
            //SQLClient();
            Console.ReadLine();
        }

        static void Json()
        {
            Order order = default;
            string json = default;

            order = new Order
            {
                Id = 1,
                TrackingNumber = "123",
                ShippingAddress = new StreetAddress { City = "London", Street = "221 B Baker St" }
            };

            json = JsonSerializer.Serialize(order);

            //json = System.IO.File.ReadAllText("order.json");
            //order = JsonSerializer.Deserialize<Order>(json);
        }

        static async Task SQLClient()
        {
            var client = new Microsoft.Data.SqlClient.SqlConnection("");
            await client.OpenAsync();

            var cmd = client.CreateCommand();
            cmd.CommandText = "SELECT Id, TrackingNumber FROM dbo.Orders";

            var result = await cmd.ExecuteReaderAsync();
            while (await result.ReadAsync())
            {
                var value = result.GetString(1);
            }

            await client.CloseAsync();
        }
    }
}
