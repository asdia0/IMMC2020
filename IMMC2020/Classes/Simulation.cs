namespace IMMC2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Simulation
    {
        private double costIncurred = 0;

        public Square Cashier { get; set; }

        public double CostIncurred
        {
            get
            {
                SimulateFrame(this.Grid.Squares.Count);

                return this.Grid.Squares.Sum(i => i.CostIncurred);
            }
        }

        public Square Entrance { get; set; }
        public Square Exit { get; set; }
        public Grid Grid { get; set; }
        public int Population { get; set; }
        public List<Product> Products { get; set; }
        public int ProductsPerCustomer { get; set; }

        public Simulation(List<Product> products, Grid grid, int population, int productsPerCustomer)
        {
            this.Products = products;
            this.Grid = grid;
            this.Population = population;
            this.ProductsPerCustomer = productsPerCustomer;
        }

        public void SetLayout(string layout)
        {
            List<string> squares = layout.Split(" ").ToList().Select(i => i.ToLower()).ToList();

            for (int i = 0; i < this.Grid.Squares.Count; i++)
            {
                if (squares[i] == "x")
                {
                    this.Grid.Squares[i].Product = null;
                    continue;
                }
                if (squares[i] == "entrance")
                {
                    this.Grid.Squares[i].Product = null;
                    this.Entrance = this.Grid.Squares[i];
                    continue;
                }
                if (squares[i] == "exit")
                {
                    this.Grid.Squares[i].Product = null;
                    this.Exit = this.Grid.Squares[i];
                    continue;
                }
                if (squares[i] == "cashier")
                {
                    this.Grid.Squares[i].Product = null;
                    this.Cashier = this.Grid.Squares[i];
                    continue;
                }

                Product p = this.Products[int.Parse(squares[i])];

                this.Grid.Squares[i].Product = p;
            }
        }

        public List<Customer> SimulateFrame(int time)
        {
            if (time == 0)
            {
                List<Customer> customers = new();

                for (int i = 0; i < this.Population; i++)
                {
                    customers.Add(new(this, this.ProductsPerCustomer, this.Products));

                    this.Entrance.Density++;
                }

                return customers;
            }
            else
            {
                List<Customer> customers = SimulateFrame(time - 1);

                foreach (Customer c in customers)
                {
                    if (c.Path.Count <= time)
                    {
                        continue;
                    }

                    c.Path[time - 1].Density--;

                    Square s = c.Path[time];

                    // Getting an item
                    if (s.Product != null && c.DesiredProducts.Contains(s.Product) && s.Product.Quantity > 0)
                    {
                        c.Inventory.Add((s.Product, s.Product.DiscountedPrice));
                        s.Product.Quantity--;
                    }

                    // Update square info
                    if (s != this.Exit)
                    {
                        s.Density++;
                        _ = s.CostIncurred;
                    }
                }

                //System.Console.WriteLine((time, this.Grid.Squares[1].Density));

                return customers;
            }
        }
    }
}