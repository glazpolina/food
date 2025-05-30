namespace Model
{
    public abstract partial class FoodProduct
    {
        protected string _name;
        public string Name => _name;
        public int DaysToExpire { get; set; }
        public int MaxShelfLife { get; set; }

        public FoodProduct(string name, int daysToExpire, int maxShelfLife)
        {
            _name = name;
            DaysToExpire = daysToExpire;
            MaxShelfLife = maxShelfLife;
        }
        public abstract double GetQuality();//качество продукта
    }
    public class Vegetable : FoodProduct
    {
        public bool IsOrganic { get; set; }
        public double WaterContent { get; set; } // в процентах
        public Vegetable(string name, int daysToExpire, int maxShelfLife, bool isOrganic, double waterContent)
            : base(name, daysToExpire, maxShelfLife)
        {
            IsOrganic = isOrganic;
            WaterContent = waterContent;
        }
        public override double GetQuality()
        {
            // Пример оценки качества
            double freshnessFactor = (double)DaysToExpire / MaxShelfLife * 100;
            double organicBonus = IsOrganic ? 5 : 0;
            return Math.Min(100, freshnessFactor + organicBonus);
        }
    }
    public class Fruit : FoodProduct
    {
        public int SweetnessLevel { get; set; } // 1-10
        public bool IsRipe { get; set; }
        public Fruit(string name, int daysToExpire, int maxShelfLife, int sweetnessLevel, bool isRipe)
            : base(name, daysToExpire, maxShelfLife)
        {
            SweetnessLevel = sweetnessLevel;
            IsRipe = isRipe;
        }
        public override double GetQuality()
        {
            double freshnessFactor = (double)DaysToExpire / MaxShelfLife * 100;
            double ripenessBonus = IsRipe ? 10 : -10;
            double sweetnessBonus = SweetnessLevel;
            return Math.Min(100, Math.Max(0, freshnessFactor + ripenessBonus + sweetnessBonus));
        }
    }
    public class Meat : FoodProduct
    {
        public double FatContent { get; set; } // в процентах
        public bool IsFrozen { get; set; }
        public Meat(string name, int daysToExpire, int maxShelfLife, double fatContent, bool isFrozen)
            : base(name, daysToExpire, maxShelfLife)
        {
            FatContent = fatContent;
            IsFrozen = isFrozen;
        }
        public override double GetQuality()
        {
            double freshnessFactor = (double)DaysToExpire / MaxShelfLife * 100;
            double frozenBonus = IsFrozen ? 15 : 0;
            double fatPenalty = FatContent > 20 ? -10 : 0;
            return Math.Min(100, Math.Max(0, freshnessFactor + frozenBonus + fatPenalty));
        }
    }
    public class Backery : FoodProduct
    {
        public bool IsGlutenFree { get; set; }
        public int SugarContent { get; set; } // граммы

        public Backery(string name, int daysToExpire, int maxShelfLife, bool isGlutenFree, int sugarContent)
            : base(name, daysToExpire, maxShelfLife)
        {
            IsGlutenFree = isGlutenFree;
            SugarContent = sugarContent;
        }

        public override double GetQuality()
        {
            double freshnessFactor = (double)DaysToExpire / MaxShelfLife * 100;
            double glutenFreeBonus = IsGlutenFree ? 5 : 0;
            double sugarPenalty = SugarContent > 30 ? -5 : 0;
            return Math.Min(100, Math.Max(0, freshnessFactor + glutenFreeBonus + sugarPenalty));
        }
    }
}
