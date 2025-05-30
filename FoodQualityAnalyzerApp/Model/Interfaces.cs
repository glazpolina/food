using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ISpreadable
    {
        List<FoodProduct> Products { get; }
        void Add(FoodProduct product);
        void Add(IEnumerable<FoodProduct> products);

    }
    public interface IShrinkable
    {
        void RemoveProduct(FoodProduct product);
        void RemoveProductAt(int index);
        void RemoveLastProduct();
    }
    public interface IStatistic
    {
        double GetMaxQuality();
        double GetMinQuality();
        double GetAverageQuality();
        double GetMedianQuality();
    }
}
