using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class FoodQualityAnalyzer : ISpreadable
    {
        
        private List<FoodProduct> _products = new List<FoodProduct>();
        public List<FoodProduct> Products => _products;
        public void Add(FoodProduct product)
        {
            if (!_products.Contains(product)) Products.Add(product);
        }
        public void Add(IEnumerable<FoodProduct> products)
        {
            foreach (var p in products) Add(p);
        }
    }


    public static class ProductFactory
    {
        public static List<FoodProduct> CreateSampleProducts()
        {
            return new List<FoodProduct>
            {
                new Vegetable("Морковь", 5, 10, true, 88.0),
                new Vegetable("Картофель", 15, 30, false, 79.0),
                new Fruit("Яблоко", 7, 14, 7, true),
                new Fruit("Банан", 3, 10, 9, false),
                new Meat("Курица", 2, 7, 10, true),
                new Meat("Говядина", 5, 10, 25, false),
                new Backery("Хлеб", 2, 5, false, 5),
                new Backery("Безглютеновый кекс", 3, 7, true, 15),

            };
        }
    }
}
