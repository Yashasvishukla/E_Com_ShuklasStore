using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger){
            try
            {
                if(!context.ProductBrands.Any()){
                    var brandsJSON = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJSON);
                    foreach(var item in brands){
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any()){
                    var typesJSON = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesJSON);
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any()){
                    var productsJSON = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsJSON);
                    foreach(var item in products){
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var log = logger.CreateLogger<StoreContextSeed>();
                log.LogError(ex.Message, "An Error Occured during Seeding the Data");
            }
        }
    }
}