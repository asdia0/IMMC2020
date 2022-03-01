namespace IMMC2020
{
    using System.Linq;

    public class Product
    {
        public double Desirability
        {
            get
            {
                return 100 * this.Discount + 75 * this.RetailPrice / this.Simulation.Products.OrderByDescending(i => i.RetailPrice).FirstOrDefault().RetailPrice + 30 * this.Rating / 5 + 20 / (double)this.Quantity;
            }
        }

        public double Discount { get; set; }

        public double DiscountedPrice
        {
            get
            {
                return RetailPrice * (1 - Discount / 100);
            }
        }

        public double HumanThreshold
        {
            get
            {
                return 10;
            }
        }

        public string Name { get; set; }

        public int Quantity { get; set; }
        public double Rating { get; set; }
        public double RetailPrice { get; set; }
        public Simulation Simulation { get; set; }

        public Product(Simulation simulation, string name, double retailPrice, double discount, int quantity, double rating)
        {
            this.Simulation = simulation;
            this.Name = name;
            this.RetailPrice = retailPrice;
            this.Discount = discount;
            this.Quantity = quantity;
            this.Rating = rating;
        }
    }
}