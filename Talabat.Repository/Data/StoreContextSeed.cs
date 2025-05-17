using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            try
            {

                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var Brand in brands)
                        await context.ProductBrands.AddAsync(Brand);

                }


                if (!context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    foreach (var Type in types)
                        await context.ProductTypes.AddAsync(Type);

                }

                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                    foreach (var Product in products)
                        await context.Products.AddAsync(Product);

                }

                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

                    var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                    foreach (var delivery in deliveryMethod)
                        await context.DeliveryMethods.AddAsync(delivery);

                }

                context.SaveChanges();


            }
            catch (Exception ex )
            {
                var Logger = loggerFactory.CreateLogger<StoreContextSeed>();
                Logger.LogError(ex,ex.Message);
            }




            


        }

    }
}
