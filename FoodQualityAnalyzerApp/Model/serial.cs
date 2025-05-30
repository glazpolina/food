using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model
{
    public static class JsonHelper
    {
        private static readonly string DataFolder = "Data";
        private static readonly string ProductsFile = Path.Combine(DataFolder, "products.json");

        // Сохраняем список продуктов в JSON
        public static void SaveProducts(List<FoodProduct> products)
        {
            Directory.CreateDirectory(DataFolder);

            // Сериализация с сохранением типа для десериализации наследников
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(products, settings);
            File.WriteAllText(ProductsFile, json);
        }

        // Загружаем продукты из JSON
        public static List<FoodProduct> LoadProducts()
        {
            if (!File.Exists(ProductsFile))
            {
                return null;
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = File.ReadAllText(ProductsFile);
            return JsonConvert.DeserializeObject<List<FoodProduct>>(json, settings);
        }
    }
}
